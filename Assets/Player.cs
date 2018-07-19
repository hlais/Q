using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    [SerializeField] float runSpeed;
    Rigidbody2D qRigidBody;
    Transform qTransform;
    
    


	void Start () {
        qRigidBody = GetComponent<Rigidbody2D>();
        qTransform = GetComponent<Transform>();
		
	}
	
	
	void Update () {
        Run();
        FlilpSprite();
	}

    void Run()
    {
        float qHorizontalInput = Input.GetAxis("Horizontal");
       

        Vector2 horizontalVector =  new Vector2( qHorizontalInput * runSpeed, qRigidBody.velocity.y);
        qRigidBody.velocity = horizontalVector;

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
