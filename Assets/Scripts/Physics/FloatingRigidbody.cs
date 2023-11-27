using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingRigidbody : MonoBehaviour
{
    [SerializeField]
    float submergenceOffset = 0.5f;

    [SerializeField]
    float submergenceRange = 1f;

    [SerializeField]
    float buoyancy = 1f;

    [SerializeField]
    Vector3 buoyancyOffset = Vector3.zero;

    [SerializeField, Range(0f, 10f)]
    float waterDrag = 1f;

    [SerializeField]
    LayerMask waterMask = 0;

    Rigidbody rb;
    float submergence;
    Vector3 gravity = Physics.gravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (submergence > 0f)
        {
            float drag = Mathf.Max(0f, 1f - waterDrag * submergence * Time.deltaTime);
            rb.velocity *= drag;
            rb.angularVelocity *= drag;
            rb.AddForceAtPosition(
                gravity * -(buoyancy * submergence),
                transform.TransformPoint(buoyancyOffset),
                ForceMode.Acceleration);
            submergence = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (
            !rb.IsSleeping() && (waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if ((waterMask & (1 << other.gameObject.layer)) != 0)
        {
            EvaluateSubmergence();
        }
    }

    void EvaluateSubmergence()
    {
        Vector3 upAxis = -gravity.normalized;
        if (Physics.Raycast(
            rb.position + upAxis * submergenceOffset,
            -upAxis, out RaycastHit hit, submergenceRange + 1f,
            waterMask, QueryTriggerInteraction.Collide
        ))
        {
            submergence = 1f - hit.distance / submergenceRange;
        }
        else
        {
            submergence = 1f;
        }
    }
}
