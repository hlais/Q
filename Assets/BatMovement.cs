using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MonoBehaviour {

    Rigidbody2D bat;
    [SerializeField] float speed = 45f;
    [SerializeField] float flapHeight =15f;
  
 
	// Use this for initialization
	void Start () {

        bat = GetComponent<Rigidbody2D>();
        
        
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovingLeft();
		
	}
    void MovingLeft()
    {
        bat.velocity = new Vector2(- speed * Time.deltaTime, 0f);
    }
    void Flap()
    {
        bat.velocity = new Vector2(0f, flapHeight) ;
      
    }
    void FlapOrignal()
    {
        bat.velocity = new Vector2(0f, -flapHeight);
       
        
    }


}
