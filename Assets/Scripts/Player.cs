using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour {


    //config
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 5f;
    Rigidbody2D qRigidBody;
    Animator qAnimator;
    Collider2D qCollider;
    float gravityScaleAtStart;

    float qVerticalInput;
    float qHorizontalInput;

    public bool isKeyBoardControlsOn = true;
    //state

    //Cached Component Reference
    void Start()
    {
        qRigidBody = GetComponent<Rigidbody2D>();
        qAnimator = GetComponent<Animator>();
        qCollider = GetComponent<Collider2D>();
        gravityScaleAtStart = qRigidBody.gravityScale;

    }


    void Update()
    {
        Run();
        FlilpSprite();
        Jump();
        ClimbLadder();
        JumpAttack();
        Attacking();

    }

    void Run()
    {

        if (qCollider.IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
           
            qAnimator.SetBool("IsRunning", false);
        }

        if (!this.qAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {

            qHorizontalInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            if (isKeyBoardControlsOn)
            {
                qHorizontalInput = Input.GetAxis("Horizontal");
            }
            bool isFacingRight = Mathf.Abs(qRigidBody.velocity.x) > Mathf.Epsilon;
            qAnimator.SetBool("IsRunning", isFacingRight);
            //print(qHorizontalInput);

            Vector2 horizontalVector = new Vector2(qHorizontalInput * runSpeed * Time.deltaTime, qRigidBody.velocity.y);
            qRigidBody.velocity = horizontalVector;
        }
    }

    public void Jump()
    {
        bool isJumping;
        if (!qCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            qAnimator.SetBool("IsJumping", false);

            qAnimator.ResetTrigger("IsAttacking");
            return;
        }

        if (qCollider.IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            qAnimator.SetBool("IsJumping", false);
            return;
        }

        
        if (!isKeyBoardControlsOn)
        {
            //bool isJumping = Input.GetButtonDown("Jump");
            isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        else
        {
            isJumping = Input.GetButton("Jump");
        }
        

        if (isJumping)
        {

            Vector2 qJumpVelocity = new Vector2(0f, jumpForce);
            qRigidBody.velocity += qJumpVelocity;
            qAnimator.SetBool("IsJumping", true);
        }
    }

    void ClimbLadder()
    {
        if (!qCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {

            qAnimator.SetBool("IsClimbingLatter", false);

            qRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        qVerticalInput = CrossPlatformInputManager.GetAxisRaw("Vertical");

        if (isKeyBoardControlsOn)
        {
            qVerticalInput = Input.GetAxis("Vertical");
        }

        Vector2 verticalVector = new Vector2(qRigidBody.velocity.x, qVerticalInput * climbSpeed);
        qRigidBody.velocity = verticalVector;
        qRigidBody.gravityScale = 0;

        bool isClimbing = Mathf.Abs(qVerticalInput) > Mathf.Epsilon;
        qAnimator.SetBool("IsClimbingLatter", isClimbing);


    }

    void Attacking()
    {

        if (Input.GetKeyDown(KeyCode.F) || CrossPlatformInputManager.GetButtonDown("Fire1"))

        {
             qAnimator.SetTrigger("IsAttacking");
             qRigidBody.velocity = Vector2.zero;
        }
    }
    void JumpAttack()
    {


        if (qAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jumping") && Input.GetKeyDown(KeyCode.F))
        {
            qAnimator.SetTrigger("jumpAttack");
        }
    }


    private void FlilpSprite()
    {
        bool isFacingRight = Mathf.Abs(qRigidBody.velocity.x) > Mathf.Epsilon;

        if (isFacingRight)
        {
            transform.localScale = new Vector2(Mathf.Sign( qRigidBody.velocity.x), 1f);
           
        }

    }
}
