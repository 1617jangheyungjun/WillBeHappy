using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlower : MonoBehaviour
{
    [SerializeField] float MaxJumpingSpeed = 30f;
    Rigidbody2D myrigidbody;
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D other)
    {   
        if(other.gameObject.tag == "Jump")
        {
            if(myrigidbody.velocity.y > MaxJumpingSpeed)
            {
                myrigidbody.velocity = new Vector2 (0, MaxJumpingSpeed);
            }
        }
    }

    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }
    
}
