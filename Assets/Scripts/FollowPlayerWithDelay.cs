using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerWithDelay : MonoBehaviour {

    [SerializeField] float leftBoundary;
    [SerializeField] float rightBoundary;
    [SerializeField] float topBoundary;
    [SerializeField] float bottomBoundary;
    Player player;

    public float smoothing = 2.5f;

    Vector3 offset;
	
	void Start () {
        player = FindObjectOfType<Player>();
        offset = transform.position - player.transform.position;
		
	}
	
	void FixedUpdate () {
        float playerXBounded = Mathf.Clamp(player.transform.position.x, leftBoundary, rightBoundary);
        float playerYBounded = Mathf.Clamp(player.transform.position.y, topBoundary, bottomBoundary);


        Vector3 playerPos = new Vector3(playerXBounded, playerYBounded, transform.position.z);


        Vector3 targetCamPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(playerPos, targetCamPos, smoothing * Time.fixedDeltaTime);
		
	}
}
