using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    //config
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce = 5f;
    Rigidbody2D qRigidBody;
    Animator qAnimator;
    Collider2D qCollider;
    

    //state
    bool isAlive = true;
    

    //Cached Component Reference
	void Start () {
        qRigidBody = GetComponent<Rigidbody2D>();
        qAnimator = GetComponent<Animator>();
        qCollider = GetComponent<Collider2D>();
		
	}
	
	
	void Update () {
        Run();
        FlilpSprite();
        Jump();
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
            return;
        }

        bool isJumping = Input.GetButtonDown("Jump");


        if (isJumping)
        {
            Vector2 qJumpVelocity = new Vector2(0f, jumpForce);
            qRigidBody.velocity += qJumpVelocity;
            //qAnimator.SetBool("IsJumping", isJumping);
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
