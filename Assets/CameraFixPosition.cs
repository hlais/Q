using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixPosition : MonoBehaviour {

    Transform currentPosition;
	// Use this for initialization
	void Start () {

        currentPosition = GetComponent<Transform>();
       
        
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {


        currentPosition.transform.position = new Vector3(transform.position.x, -0.2669f, transform.position.z);
        Debug.Log(currentPosition.transform.position);


    }
}
