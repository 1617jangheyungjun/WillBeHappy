using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlower : MonoBehaviour
{
    [SerializeField] float MaxJumpingSpeed = 30f;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other)
    {   
        Rigidbody2D otherrigid = other.gameObject.GetComponent<Rigidbody2D>();
        if(other.gameObject.tag == "Player")
        {
            if(otherrigid.velocity.y > MaxJumpingSpeed)
            {
                otherrigid.velocity = new Vector2 (0, MaxJumpingSpeed);
            }
        }
    }    
}
