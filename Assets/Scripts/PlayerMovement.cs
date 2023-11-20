using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    //Serialized Variables
    [Header("General Movement Controls")]
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxClimbSpeed = 2f, maxSwimSpeed = 5f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f, maxAirAcceleration = 1f, maxClimbAcceleration = 20f, maxSwimAcceleration = 5f;

    [Header("Jumping Controls")]
    [SerializeField, Range(0f, 10f)]
    float jumpForce = 5f;

    [SerializeField, Range(0, 5)]
    int maxAirJumps = 0;

    [SerializeField, Range(0f, 90f)]
    float maxGroundAngle = 25f, maxStairsAngle = 50f;

    [SerializeField]
    public Vector3 standardGravity = new Vector3(0f, -9.8f, 0f), fallingGravity = new Vector3(0f, -12f, 0f);

    [SerializeField]
    public float FallingThreshold = 0.1f;

    [Header("Camera Controls")]
    [SerializeField, Range(0f, 100f)]
    float maxSnapSpeed = 100f;

    [SerializeField, Min(0f)]
    float probeDistance = 1f;

    [Header("Climbing Controls")]
    [SerializeField, Range(90, 180)]
    float maxClimbAngle = 140f;

    [Header("Swimming Controls")]
    [SerializeField, Range(0f, 5f)]
    float submergenceOffset = 1f;

    [SerializeField, Range(0f, 5f)]
    float submergenceRange = 2f;

    [SerializeField, Range(0f, 10f)]
    float waterDrag = 1f;

    [SerializeField, Range(0f, 10f)]
    float buoyancy = 2f;

    [SerializeField, Range(0.01f, 2f)]
    float swimThreshold = 1f;

    [Header("Other")]
    [SerializeField] LayerMask probeMask = -1, stairsMask = -1, climbMask = -1, waterMask = 0;

    [SerializeField] Transform playerInputSpace = default, playerModel = default;

    [SerializeField] Material normalMaterial = default, climbingMaterial = default, swimmingMaterial = default;

    [SerializeField] Animator playerAnimator = default;

    public bool disabledPlayerMovement = false;

    SkinnedMeshRenderer skinnedMeshRenderer;

    //Movement Variables
    [HideInInspector] public Vector3 playerInput;
    [HideInInspector] public Vector3 velocity, connectionVelocity;
    private Vector3 contactNormal, steepNormal, climbNormal, lastClimbNormal;
    private Vector3 upAxis, rightAxis, forwardAxis;
    private Rigidbody rb, connectedRb, previousConnectedRb;
    private Vector3 connectionWorldPosition, connectionLocalPosition;

    //Jump Variables
    private bool desiredJump, desiresClimbing;
    private int groundContactCount, steepContactCount, climbContactCount;
    private int stepsSinceLastGrounded, stepsSinceLastJump;
    private KeyCode jumpKey = KeyCode.Space;
    private KeyCode climbKey = KeyCode.LeftShift;
    private int jumpPhase;
    private float minGroundDotProduct, minStairsDotProduct, minClimbDotProduct;

    //Swimming Variables
    float submergence;

    void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
        minClimbDotProduct = Mathf.Cos(maxClimbAngle * Mathf.Deg2Rad);
    }

    private void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        OnValidate();
        playerAnimator.Play("Idle");
    }

    private void Update()
    {
        if (disabledPlayerMovement)
            return;

        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput.z = Swimming ? Input.GetAxis("UpDown") : 0f;
        playerInput = Vector3.ClampMagnitude(playerInput, 1f);

        if (playerInputSpace)
        {
            rightAxis = ProjectDirectionOnPlane(playerInputSpace.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(playerInputSpace.forward, upAxis);
        }
        else
        {
            rightAxis = ProjectDirectionOnPlane(Vector3.right, upAxis);
            forwardAxis = ProjectDirectionOnPlane(Vector3.forward, upAxis);
        }

        if (Swimming)
        {
            desiresClimbing = false;
        }
        else
        {
            desiredJump |= Input.GetKeyDown(jumpKey);
            desiresClimbing = Input.GetKey(climbKey);
        }

        //skinnedMeshRenderer.material = Climbing ? climbingMaterial : Swimming ? swimmingMaterial : normalMaterial;
    }

    private void FixedUpdate()
    {
        if (!onGround && rb.velocity.y < FallingThreshold)
        {
            Physics.gravity = fallingGravity;
        }
        else if (onGround)
            Physics.gravity = standardGravity;

        upAxis = -Physics.gravity.normalized;

        UpdateState();

        if (InWater)
        {
            velocity *= 1f - waterDrag * submergence * Time.deltaTime;
        }

        AdjustVelocity();

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        if (Climbing)
        {
            rb.useGravity = false;
            velocity -= contactNormal * (maxClimbAcceleration * 0.9f * Time.deltaTime);
        }
        else if (InWater)
        {
            rb.useGravity = true;
            velocity += Physics.gravity * ((1f - buoyancy * submergence) * Time.deltaTime);
        }
        else if (!Climbing)
        {
            rb.useGravity = true;
        }
        else if (desiresClimbing && onGround)
        {
            velocity +=
                (Physics.gravity - contactNormal * (maxClimbAcceleration * 0.9f)) *
                Time.deltaTime;
        }
        else if (onGround && velocity.sqrMagnitude < 0.01f)
        {
            velocity +=
                contactNormal *
                (Vector3.Dot(Physics.gravity, contactNormal) * Time.deltaTime);
        }

        if (!disabledPlayerMovement)
            rb.velocity = velocity;
        else if (disabledPlayerMovement)
            rb.velocity = Vector3.zero;

        if (velocity.magnitude > .01)
        {
            playerModel.rotation = Quaternion.LookRotation(velocity, Vector3.up);
            playerModel.eulerAngles = new Vector3(0f, playerModel.eulerAngles.y, 0f);
        }

        ClearState();
    }

    private void UpdateState()
    {
        stepsSinceLastGrounded += 1;
        stepsSinceLastJump += 1;
        velocity = rb.velocity;

        if (CheckClimbing() || CheckSwimming() || onGround || SnapToGround() || CheckSteepContacts())
        {
            stepsSinceLastGrounded = 0;
            if (stepsSinceLastJump > 1)
            {
                jumpPhase = 0;
            }
            if (groundContactCount > 1)
            {
                contactNormal.Normalize();
            }
        }
        else
            contactNormal = upAxis;

        if (connectedRb)
        {
            if (connectedRb.isKinematic || connectedRb.mass >= rb.mass)
            {
                UpdateConnectionState();
            }
        }
    }

    void UpdateConnectionState()
    {
        if (connectedRb == previousConnectedRb)
        {
            Vector3 connectionMovement =
                connectedRb.transform.TransformPoint(connectionLocalPosition) -
                connectionWorldPosition;
            connectionVelocity = connectionMovement / Time.deltaTime;
        }
        connectionWorldPosition = rb.position;
        connectionLocalPosition = connectedRb.transform.InverseTransformPoint(connectionWorldPosition);
    }

    void ClearState()
    {
        groundContactCount = steepContactCount = climbContactCount = 0;
        contactNormal = steepNormal = climbNormal = Vector3.zero;
        connectionVelocity = Vector3.zero;
        previousConnectedRb = connectedRb;
        connectedRb = null;
        submergence = 0f;
    }

    void Jump()
    {
        Vector3 jumpDirection;
        if (onGround)
        {
            jumpDirection = contactNormal;
        }
        else if (OnSteep)
        {
            jumpDirection = steepNormal;
            jumpPhase = 0;
        }
        else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
        {
            if (jumpPhase == 0)
            {
                jumpPhase = 1;
            }
            jumpDirection = contactNormal;
        }
        else
        {
            return;
        }
        stepsSinceLastJump = 0;
        jumpPhase += 1;
        float jumpSpeed = Mathf.Sqrt(2f * Physics.gravity.magnitude * jumpForce);
        if (InWater)
        {
            jumpSpeed *= Mathf.Max(0f, 1f - submergence / swimThreshold);
        }
        jumpDirection = (jumpDirection + upAxis).normalized;
        float alignedSpeed = Vector3.Dot(velocity, jumpDirection);

        if (alignedSpeed > 0f)
        {
            jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
        }
        velocity += jumpDirection * jumpSpeed;
    }

    public bool Climbing => climbContactCount > 0 && stepsSinceLastJump > 2;
    public bool onGround => groundContactCount > 0;
    bool OnSteep => steepContactCount > 0;
    public bool InWater => submergence > 0f;
    public bool Swimming => submergence >= swimThreshold;

    void OnTriggerEnter(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence(other);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence(other);
        }
    }

    void EvaluateSubmergence(Collider collider)
    {
        if (Physics.Raycast(rb.position + upAxis * submergenceOffset, -upAxis, out RaycastHit hit, submergenceRange + 1f, waterMask, QueryTriggerInteraction.Collide))
        {
            submergence = 1f - hit.distance / submergenceRange;
        }
        else
        {
            submergence = 1f;
        }

        if (Swimming)
        {
            connectedRb = collider.attachedRigidbody;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    private void EvaluateCollision(Collision collision)
    {
        if (Swimming)
        {
            return;
        }

        int layer = collision.gameObject.layer;
        float minDot = GetMinDot(layer);

        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            float upDot = Vector3.Dot(upAxis, normal);
            if (upDot >= minDot)
            {
                groundContactCount += 1;
                contactNormal += normal;
                connectedRb = collision.rigidbody;
            }
            else
            {
                if (upDot > -0.01f)
                {
                    steepContactCount += 1;
                    steepNormal += normal;
                    if (groundContactCount == 0)
                    {
                        connectedRb = collision.rigidbody;
                    }
                }
                if (desiresClimbing && upDot >= minClimbDotProduct && (climbMask & (1 << layer)) != 0)
                {
                    climbContactCount += 1;
                    climbNormal += normal;
                    lastClimbNormal = normal;
                    connectedRb = collision.rigidbody;
                }
            }
        }
    }

    private Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
    {
        return (direction - normal * Vector3.Dot(direction, normal)).normalized;
    }

    private void AdjustVelocity()
    {
        float acceleration, speed;
        Vector3 xAxis, zAxis;

        if (Climbing)
        {
            acceleration = maxClimbAcceleration;
            speed = maxClimbSpeed;
            xAxis = Vector3.Cross(contactNormal, upAxis);
            zAxis = upAxis;
        }
        else if (InWater)
        {
            float swimFactor = Mathf.Min(1f, submergence / swimThreshold);
            acceleration = Mathf.LerpUnclamped(onGround ? maxAcceleration : maxAirAcceleration, maxSwimAcceleration, swimFactor);
            speed = Mathf.LerpUnclamped(maxSpeed, maxSwimSpeed, swimFactor);
            xAxis = rightAxis;
            zAxis = forwardAxis;
        }
        else
        {
            acceleration = onGround ? maxAcceleration : maxAirAcceleration;
            speed = onGround && desiresClimbing ? maxClimbSpeed : maxSpeed;
            xAxis = rightAxis;
            zAxis = forwardAxis;
        }

        xAxis = ProjectDirectionOnPlane(xAxis, contactNormal);
        zAxis = ProjectDirectionOnPlane(zAxis, contactNormal);

        Vector3 relativeVelocity = velocity - connectionVelocity;
        float currentX = Vector3.Dot(relativeVelocity, xAxis);
        float currentZ = Vector3.Dot(relativeVelocity, zAxis);

        float maxSpeedChange = acceleration * Time.deltaTime;

        float newX =
            Mathf.MoveTowards(currentX, playerInput.x * speed, maxSpeedChange);
        float newZ =
            Mathf.MoveTowards(currentZ, playerInput.y * speed, maxSpeedChange);

        velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);

        if (Swimming)
        {
            float currentY = Vector3.Dot(relativeVelocity, upAxis);
            float newY = Mathf.MoveTowards(currentY, playerInput.z * speed, maxSpeedChange);
            velocity += upAxis * (newY - currentY);
        }
    }

    private bool SnapToGround()
    {
        if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
        {
            return false;
        }
        float speed = velocity.magnitude;
        if (speed > maxSnapSpeed)
        {
            return false;
        }
        if (!Physics.Raycast(rb.position, -upAxis, out RaycastHit hit, probeDistance, probeMask, QueryTriggerInteraction.Ignore))
        {
            return false;
        }
        float upDot = Vector3.Dot(upAxis, hit.normal);
        if (upDot < GetMinDot(hit.collider.gameObject.layer))
        {
            return false;
        }

        groundContactCount = 1;
        contactNormal = hit.normal;
        float dot = Vector3.Dot(velocity, hit.normal);
        if (dot > 0f)
        {
            velocity = (velocity - hit.normal * dot).normalized * speed;
        }
        connectedRb = hit.rigidbody;
        return true;
    }

    float GetMinDot(int layer)
    {
        return (stairsMask & (1 << layer)) == 0 ? minGroundDotProduct : minStairsDotProduct;
    }

    bool CheckSteepContacts()
    {
        if (steepContactCount > 1)
        {
            steepNormal.Normalize();
            float upDot = Vector3.Dot(upAxis, steepNormal);
            if (upDot >= minGroundDotProduct)
            {
                groundContactCount = 1;
                contactNormal = steepNormal;
                return true;
            }
        }
        return false;
    }

    bool CheckClimbing()
    {
        if (Climbing)
        {
            if (climbContactCount > 1)
            {
                climbNormal.Normalize();
                float upDot = Vector3.Dot(upAxis, climbNormal);
                if (upDot >= minGroundDotProduct)
                {
                    climbNormal = lastClimbNormal;
                }
            }

            groundContactCount = 1;
            contactNormal = climbNormal;
            return true;
        }
        return false;
    }

    bool CheckSwimming()
    {
        if (Swimming)
        {
            groundContactCount = 0;
            contactNormal = upAxis;
            return true;
        }
        return false;
    }

    public void PreventSnapToGround()
    {
        stepsSinceLastJump = -1;
    }
}
