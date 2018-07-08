using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {


    [SerializeField]
    float walkSpeed = 2.5f;
    Rigidbody2D myRigidBody;
    Animator playerAnimator;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
            
		
	}
	
	// Update is called once per frame
	void Update () {
        Run();
        FlipSprite();
		
	}
    private void Run()
    {
        float movementInput = Input.GetAxis("Horizontal");
        Vector2 direction = new Vector2(movementInput * walkSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = direction;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            playerAnimator.SetBool("isRunning", true);
        }
  
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale  = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
