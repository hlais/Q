using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    float walkSpeed = 1f;
    Rigidbody2D miniSpider;

	void Start () {

       
        miniSpider = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (IsFacingLeft())
        {
            miniSpider.velocity = new Vector2(-walkSpeed, 0f);
        }
        else
        {
            miniSpider.velocity = new Vector2(walkSpeed, 0f);
        }
	}

    bool IsFacingLeft()
    {
        return transform.localScale.x < 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // the minus symbol flips the sprites. -- == + / -+ == -
        transform.localScale = new Vector2(-(Mathf.Sign(miniSpider.velocity.x)), 1f);
    }
}
