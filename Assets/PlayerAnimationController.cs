using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private string idleAnim = "Idle", jumpAnim = "Jumping", jogAnim = "Jogging", swimAnim = "Swimming", climbAnim = "Climbing", fallAnim = "Falling";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Swimming
        if (PlayerMovement.Instance.InWater || PlayerMovement.Instance.Swimming)
            animator.Play(swimAnim);

        //Climbing
        if (PlayerMovement.Instance.Climbing)
            animator.Play(climbAnim);

        if (PlayerMovement.Instance.Climbing && PlayerMovement.Instance .playerInput == Vector3.zero)
            animator.speed = 0f;
        else
            animator.speed = 1f;

        //Falling
        if (!PlayerMovement.Instance.onGround && PlayerMovement.Instance.velocity.y < PlayerMovement.Instance.FallingThreshold)
        {
            animator.Play(fallAnim);
        }

        //Jumping
        if (!PlayerMovement.Instance.Climbing && !PlayerMovement.Instance.InWater && !PlayerMovement.Instance.onGround && Physics.gravity == PlayerMovement.Instance.standardGravity)
            animator.Play(jumpAnim);

        //Jogging
        if (PlayerMovement.Instance.velocity.magnitude > 0.1f)
        {
            if (!PlayerMovement.Instance.Climbing && !PlayerMovement.Instance.InWater && !PlayerMovement.Instance.Swimming && PlayerMovement.Instance.onGround)
                animator.Play(jogAnim);
        }
        //Idling
        else if (PlayerMovement.Instance.velocity.magnitude <= 0.1)
        {
            if (!PlayerMovement.Instance.Climbing && !PlayerMovement.Instance.InWater && PlayerMovement.Instance.onGround)
                animator.Play(idleAnim);
        }
    }
}
