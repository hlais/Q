using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour {

    [SerializeField] float leftBoundary;
    [SerializeField] float rightBoundary;
    

    Player player;
	void Start () {

        player = FindObjectOfType<Player>();
		
	}
	
	// Update is called once per frame
	void Update () {

        float playerBounded = Mathf.Clamp(player.transform.position.x, leftBoundary, rightBoundary);
        Vector3 playerPos = new Vector3(playerBounded, -0.2f,transform.position.z);
        transform.position = playerPos;
		

    }
}
