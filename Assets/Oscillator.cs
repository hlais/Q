using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField]
    float startingPosition;

    [SerializeField]
    float endingPosition;
    
    public float speed = 1.0f;

    void Update()
    {

        MovementOfObject();
    }
    void MovementOfObject()
    {
        Vector3 pos1 = new Vector3(startingPosition, transform.position.y, 0);
        Vector3 pos2 = new Vector3(endingPosition, transform.position.y, 0);
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
    //Player can move with platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "PlayerQ")
        {
            collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.name == "PlayerQ")
        {
            collision.collider.transform.SetParent(null);
        }

    }

}
