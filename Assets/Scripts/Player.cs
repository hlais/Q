using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    //config
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 5f;
    Rigidbody2D qRigidBody;
    Animator qAnimator;
    Collider2D qCollider;
    float gravityScaleAtStart;
    

    //state
    bool isAlive = true;
    

    //Cached Component Reference
	void Start () {
        qRigidBody = GetComponent<Rigidbody2D>();
        qAnimator = GetComponent<Animator>();
        qCollider = GetComponent<Collider2D>();
        gravityScaleAtStart = qRigidBody.gravityScale;
		
	}
	
	
	void Update () {
        Run();
        FlilpSprite();
        Jump();
        ClimbLadder();

    }

    void Run()
    {
        float qHorizontalInput = Input.GetAxis("Horizontal");
        bool isFacingRight = Mathf.Abs(qRigidBody.velocity.x) > Mathf.Epsilon;
        qAnimator.SetBool("IsRunning", isFacingRight);
       


        Vector2 horizontalVector =  new Vector2( qHorizontalInput * runSpeed, qRigidBody.velocity.y);
        qRigidBody.velocity = horizontalVector;

    }

    void Jump()
    {
        

        if (!qCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            qAnimator.SetBool("IsJumping", false);
            return;
        }
        if (qCollider.IsTouchingLayers(LayerMask.GetMask("Wall")))
        {
            qAnimator.SetBool("IsJumping", false);
            return; 
        }

        bool isJumping = Input.GetButtonDown("Jump");
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
        
        
        

        float qVerticalInput = Input.GetAxis("Vertical");

        Vector2 verticalVector = new Vector2(qRigidBody.velocity.x, qVerticalInput * climbSpeed);
        qRigidBody.velocity = verticalVector;
        qRigidBody.gravityScale = 0;

        bool isClimbing = Mathf.Abs(qVerticalInput) > Mathf.Epsilon;
            qAnimator.SetBool("IsClimbingLatter", isClimbing);

     

       


      
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
