using UnityEngine;

public class PlayerAnimationController : Singleton<PlayerAnimationController>
{
    private Animator animator;

    private string idleAnim = "Idle",
        jumpAnim = "Jumping",
        jogAnim = "Jogging",
        swimAnim = "Swimming",
        climbAnim = "Climbing",
        fallAnim = "Falling",
        crouchDownAnim = "Crouch Down Anim",
        crouchUpAnim = "Crouch Up Anim";

    private bool inDialogue;
    [SerializeField]  bool startProne = false;

    [SerializeField]
    private float waitTimer;

    new void Awake()
    {
        base.Awake();

        animator = GetComponent<Animator>();
        waitTimer = 0;
        inDialogue = false;
    }

    private void Start()
    {
        if (startProne)
            BogsGetup();
    }

    void Update()
    {
        //Swimming
        //if (PlayerMovement.Instance.InWater || PlayerMovement.Instance.Swimming)
        //    animator.Play(swimAnim);

        //Climbing
        //if (PlayerMovement.Instance.Climbing)
        //    animator.Play(climbAnim);

        //if (PlayerMovement.Instance.Climbing && PlayerMovement.Instance.playerInput == Vector3.zero)
        //    animator.speed = 0f;
        //else
        //    animator.speed = 1f;

        //Falling and Jumping
        //if (!PlayerMovement.Instance.onGround && !PlayerMovement.Instance.Climbing && !PlayerMovement.Instance.Swimming)
        //{
        //    if (Physics.gravity == PlayerMovement.Instance.fallingGravity)
        //        WaitUntilAnim(fallAnim);
        //    else if (Physics.gravity == PlayerMovement.Instance.standardGravity)
        //        WaitUntilAnim(jumpAnim);
        //}
        //else
        //{
        //    waitTimerCurrent = waitTimerMax;
        //}

        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        //Jogging
        if (PlayerMovement.Instance.velocity.magnitude > 0.1f && !inDialogue)
        {
            if (!PlayerMovement.Instance.Climbing && !PlayerMovement.Instance.InWater && !PlayerMovement.Instance.Swimming && PlayerMovement.Instance.onGround)
                animator.Play(jogAnim);
        }
        //Idling
        else if (PlayerMovement.Instance.velocity.magnitude <= 0.1 && !inDialogue)
        {
            if (!PlayerMovement.Instance.Climbing && !PlayerMovement.Instance.InWater && PlayerMovement.Instance.onGround)
                animator.Play(idleAnim);
        }
    }

    public void ToggleDialogueAnim(bool isCrouching)
    {
        if (isCrouching)
        {
            if (inDialogue)
            {
                animator.Play(crouchUpAnim);
                waitTimer = 1f;
            }
            else if (!inDialogue)
                animator.Play(crouchDownAnim);
        }
        else if (!isCrouching)
            animator.Play(idleAnim);

        inDialogue = !inDialogue;
    }

    public void BogsGetup()
    {
        waitTimer = 11f;
        animator.Play("Prone Getup");
    }
}
