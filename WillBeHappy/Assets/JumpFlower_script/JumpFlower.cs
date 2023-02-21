using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlower : MonoBehaviour
{
    [SerializeField] float MaxJumpingSpeed = 30f;

    [SerializeField] float  SmallestSpeed = 30f;
    BoxCollider2D myboxcollider;

    float InitialBounce = 1.2f;

    float jumpDelay = 5f;
    float InitialJumpDelay;

    bool dojump = false;
    float SmallestSpeed2;
    // Start is called before the first frame update
    void Start() 
    {
        InitialJumpDelay = jumpDelay;
        Debug.Log("실행됨");   
    }

    void Update() 
    {
        Debug.Log(InitialBounce);
        if(dojump)
        {
            jumpDelay -= Time.deltaTime;
            Debug.Log(jumpDelay);
            if(jumpDelay < 0)
            {
                jumpDelay = InitialJumpDelay;
                dojump = false;
            }
        }    
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        SmallestSpeed2 = other.GetComponent<Rigidbody2D>().velocity.y;
    }
    void OnCollisionEnter2D(Collision2D other)
    {   
        Debug.Log("점프력"+other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        Rigidbody2D otherrigid = other.gameObject.GetComponent<Rigidbody2D>();
        
        if(other.gameObject.tag == "Player" & !dojump)
        {
            dojump = true;
            if(SmallestSpeed2 > MaxJumpingSpeed)
            {
                otherrigid.velocity = new Vector2 (0, MaxJumpingSpeed);
            }

            else if(SmallestSpeed2 < SmallestSpeed)
            {
                otherrigid.velocity = new Vector2 (0, SmallestSpeed);
            }

            else
            {   
                otherrigid.velocity = new Vector2 (0, -SmallestSpeed2 * 1.2f);
            }
            Debug.Log(SmallestSpeed2 + "꿝");
        }
        
    }    
}
