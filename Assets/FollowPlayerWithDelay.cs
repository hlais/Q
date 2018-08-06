using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerWithDelay : MonoBehaviour {

    [SerializeField] float leftBoundary;
    [SerializeField] float rightBoundary;
    Player player;
    public float smoothing = 2.5f;

    Vector3 offset;
	
	void Start () {
        player = FindObjectOfType<Player>();
        offset = transform.position - player.transform.position;
		
	}
	
	void FixedUpdate () {
        float playerBounded = Mathf.Clamp(player.transform.position.x, leftBoundary, rightBoundary);
        Vector3 playerPos = new Vector3(playerBounded, -0.2f, transform.position.z);
        Vector3 targetCamPos = player.transform.position + offset;
        transform.position = Vector3.Lerp(playerPos, targetCamPos, smoothing * Time.deltaTime);
		
	}
}
