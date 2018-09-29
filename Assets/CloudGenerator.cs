using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CloudGenerator : MonoBehaviour {

    [SerializeField] GameObject[] cloud;
    

    private void Start()
    {
            
    }
    private void Update()
    {
        int randomCloud = Random.Range(0, cloud.Length - 1);
        GameObject clone;
       clone = Instantiate(cloud[randomCloud], transform.parent);
        clone.transform.TransformDirection(-1 * 10,transform.position.y,transform.position.z);

    }

}
 

    
        
        


    //timeBetweenClouds -= Time.time;
    //    float randomHeight = Random.Range(minHeight, maxHeight);
        
    //    location = new Vector3(transform.position.x * -2 * Time.fixedDeltaTime , randomHeight, transform.position.z);
        
    //    int cloudSelector = Random.Range(0, cloud.Length-1);

    //    while (timeBetweenClouds > 0)
    //    {
    //        Instantiate(cloud[cloudSelector], location, Quaternion.Euler(0, 0, 0));
    //    }


      
    
