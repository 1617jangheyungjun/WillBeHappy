using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlower : MonoBehaviour
{
    [SerializeField] float MaxJumpingSpeed = 30f;

    [SerializeField] float  SmallestSpeed = 30f;
    BoxCollider2D myboxcollider;

    public GameObject material;
    float InitialBounce = 1.2f;

    float jumpDelay = 5f;
    float InitialJumpDelay;

    bool dojump = false;
    // Start is called before the first frame update
    void Start() 
    {
        InitialJumpDelay = jumpDelay;
        Debug.Log("실행됨");   
    }

    void Update() 
    {
        //여기 전체
        Debug.Log(InitialBounce);
        if(dojump)
        {
            jumpDelay -= Time.deltaTime;
            Debug.Log(jumpDelay);
            if(jumpDelay < 0)
            {
                GameObject temp = Instantiate(material, this.transform.position, Quaternion.identity);
                temp.transform.parent = this.transform;
                jumpDelay = InitialJumpDelay;
                dojump = false;
            }
        }    
    }
    void OnCollisionEnter2D(Collision2D other)
    {   
        Debug.Log("점프력"+other.gameObject.GetComponent<Rigidbody2D>().velocity.y);
        Rigidbody2D otherrigid = other.gameObject.GetComponent<Rigidbody2D>();
        
        if(other.gameObject.tag == "Player" & !dojump)
        {
            dojump = true; // 여기
            if(otherrigid.velocity.y > MaxJumpingSpeed)
            {
                otherrigid.velocity = new Vector2 (0, MaxJumpingSpeed);
            }

            else if(otherrigid.velocity.y < SmallestSpeed)
            {
                otherrigid.velocity = new Vector2 (0, SmallestSpeed);
            }
            Debug.Log(transform.GetChild(0));
            Destroy(transform.GetChild(0).gameObject, 0); //여기
        }
        
    }    
}
