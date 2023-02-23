using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnits;

public class EnemiesMovement : Unit
{
    Rigidbody2D myrigidbody; 
    public float nextmove;
    BoxCollider2D myboxcollider;
    [SerializeField] float EnemiesMovementSpeed = 10f;
    [SerializeField] float EnemiesMovementJumpSpeed = 10f;
    RaycastHit2D rayHit;
    public string Follow = "Null";
    public string status = "Idle";
    public string InNest = "In";
    public bool isDead { get; private set; } = false;
    float whatmove;
    // Start is called before the first frame update
    void Awake() 
    {
        myrigidbody = GetComponent<Rigidbody2D>();  
        monsterLogic();  
        Follow = "Null";
        myboxcollider = GetComponent<BoxCollider2D>();
        nextmove = 1 * EnemiesMovementSpeed;
    }


    void FixedUpdate()
    {
        if(isDead)
        {
            Destroy(this.gameObject, 0);
        }
        if(currentHealth < 0)
        {
            isDead = true;
        }
        myrigidbody.velocity = new Vector2(nextmove, myrigidbody.velocity.y);
        Vector2 frontVec = new Vector2(myrigidbody.position.x + Mathf.Sign(nextmove), myrigidbody.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        //떨어지지 않게
        rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (myboxcollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            status = "Idle";
        }
        if(Follow == "Null")
        {
            if(status == "Idle")
            {
                if (rayHit.collider == null)
                {
                    Debug.Log("바닥이 없음");
                    nextmove *= -1;
                    CancelInvoke();
                    Invoke("monsterLogic", Random.Range(1f, 4f));   
                }

                int jump = Random.Range(1, 1000);
                if (jump == 1)
                {
                    myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, EnemiesMovementJumpSpeed);
                    status = "Jump";
                }
                if (InNest == "Out")
                {
                    nextmove *= -1;
                }


            }
        }
        if(Follow == "Do")
        {
            monsterLogic();
            
            status = "Idle";
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Leafy Radius Of Action" & Follow == "Null")
        {
            nextmove = Mathf.Sign(transform.parent.position.x - myrigidbody.position.x) * EnemiesMovementSpeed;
            CancelInvoke();
            Invoke("monsterLogic", Random.Range(1f, 1.5f));
        }
        if(other.tag == "Leafy Nest")
        {
            Follow = "Null";
            nextmove = Mathf.Sign(transform.parent.position.x - myrigidbody.position.x) * EnemiesMovementSpeed;
            CancelInvoke();
            Invoke("monsterLogic", Random.Range(1f, 1.5f));
        }     
        if(other.tag == "Player")
        {
            Follow = "Null";
            CancelInvoke();
            Invoke("monsterLogic", Random.Range(1f, 1.5f));
        }
    }

    

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Follow = "Null";
            CancelInvoke();
            nextmove = -Mathf.Sign(GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().position.x - myrigidbody.position.x) * EnemiesMovementSpeed;
            Invoke("monsterLogic", Random.Range(1f, 1.5f));
        }    
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(other);
        if(other.tag == "Player")
        {
            Follow = "Do";
        }
        if(other.tag == "Leafy Nest")
        {
            InNest = "In";
            Follow = "Null";
        }  
    }
    void monsterLogic()
    {
        if(Follow == "Null")
        {
            if (status == "Idle")
            {
                nextmove = Random.Range(-1, 2) * EnemiesMovementSpeed;
                Debug.Log(transform.parent.gameObject + "객체의 Leafy가 생각을 바꿈");
                CancelInvoke();
                Invoke("monsterLogic", Random.Range(1f, 5f));
            }
        }

        if(Follow == "Do")
        {
            if (status == "Idle")
            {
                nextmove = Mathf.Sign(GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().position.x - myrigidbody.position.x) * EnemiesMovementSpeed * 2;
            }
        }



        
    }
}
