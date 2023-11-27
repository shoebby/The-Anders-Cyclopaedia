using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private string idleAnim = "Idle", jumpAnim = "Jumping", jogAnim = "Jogging", swimAnim = "Swimming", climbAnim = "Climbing", fallAnim = "Falling";

    [SerializeField]
    private float waitTimerCurrent, waitTimerMax;

    void Awake()
    {
        animator = GetComponent<Animator>();
        waitTimerCurrent = waitTimerMax;
    }

    void Update()
    {
        //Swimming
        if (PlayerMovement.Instance.InWater || PlayerMovement.Instance.Swimming)
            animator.Play(swimAnim);

        //Climbing
        if (PlayerMovement.Instance.Climbing)
            animator.Play(climbAnim);

        if (PlayerMovement.Instance.Climbing && PlayerMovement.Instance.playerInput == Vector3.zero)
            animator.speed = 0f;
        else
            animator.speed = 1f;

        //Falling and Jumping
        if (!PlayerMovement.Instance.onGround && !PlayerMovement.Instance.Climbing && !PlayerMovement.Instance.Swimming)
        {
            if (Physics.gravity == PlayerMovement.Instance.fallingGravity)
                WaitUntilAnim(fallAnim);
            else if (Physics.gravity == PlayerMovement.Instance.standardGravity)
                WaitUntilAnim(jumpAnim);
        }
        else
        {
            waitTimerCurrent = waitTimerMax;
        }

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

    void WaitUntilAnim(string anim)
    {
        if (waitTimerCurrent <= 0f)
        {
            animator.Play(anim);
        } else
        {
            waitTimerCurrent -= Time.deltaTime;
        }
    }
}