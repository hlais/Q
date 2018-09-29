using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour {


    //config
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float airbouneAttack = 0.5f;
    [SerializeField] GameObject swordSlash;
    public Transform pointOfSlash;
    Rigidbody2D qRigidBody;
    Animator qAnimator;
    Collider2D qCollider;
    Vector2 startGravity;

    


    float gravityScaleAtStart;
    float qVerticalInput;
    float qHorizontalInput;

    public bool isKeyBoardControlsOn = true;

    //state
    bool isJumping;
  

    //Cached Component Reference
    void Start()
    {
        qRigidBody = GetComponent<Rigidbody2D>();
        qAnimator = GetComponent<Animator>();
        qCollider = GetComponent<Collider2D>();
        gravityScaleAtStart = qRigidBody.gravityScale;
        startGravity = Physics2D.gravity;

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
    void JumpAttack()
    {

       

      


        if ((Mathf.Abs(qRigidBody.velocity.y) > Mathf.Epsilon) && (Input.GetKeyDown(KeyCode.F) || CrossPlatformInputManager.GetButtonDown("Fire1")))
        {
        
            Physics2D.gravity = new Vector2(0,0);

             qAnimator.SetTrigger("jumpAttack");
    
          
            StartCoroutine("Landing");
          
        }
        
       

    }
    IEnumerator Landing()
    {
        
        yield return new WaitForSeconds(airbouneAttack);
        Physics2D.gravity = startGravity;
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
            SwordSlash();
             qRigidBody.velocity = Vector2.zero;
        }
    }
    void SwordSlash()
    {
        GameObject slash;
        slash =    Instantiate(swordSlash, new Vector2(pointOfSlash.position.x, pointOfSlash.position.y), Quaternion.identity) ;
        Object.Destroy(slash, 0.3f);
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
