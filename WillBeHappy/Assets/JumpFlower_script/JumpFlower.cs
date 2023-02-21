using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlower : MonoBehaviour
{
    [SerializeField] float MaxJumpingSpeed = 30f;

    [SerializeField] float  SmallestSpeed = 30f;
    // Start is called before the first frame update
    void Start() 
    {
        Debug.Log("실행됨");    
    }
    void OnCollisionEnter2D(Collision2D other)
    {   
        Debug.Log("점프력"+other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        Rigidbody2D otherrigid = other.gameObject.GetComponent<Rigidbody2D>();
        if(other.gameObject.tag == "Player")
        {
            if(otherrigid.velocity.y > MaxJumpingSpeed)
            {
                otherrigid.velocity = new Vector2 (0, MaxJumpingSpeed);
            }

            if(otherrigid.velocity.y < SmallestSpeed)
            {
                otherrigid.velocity = new Vector2 (0, SmallestSpeed);
            }
        }
    }    
}
