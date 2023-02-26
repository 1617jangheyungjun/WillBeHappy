using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnits;

public class ForestBug : Unit
{
    public GameObject bullet;
    Rigidbody2D myrigidbody; 
    public float nextmove;
    BoxCollider2D myboxcollider;
    [SerializeField] float EnemiesMovementSpeed = 10f;
    [SerializeField] [Range(1, 50)] float Re_Needle_Shoot = 10f;
    RaycastHit2D rayHit;
    public string Follow = "Null";
    public string status = "Idle";
    public string InNest = "In";
    bool dead = true;
    float whatmove;
    float initialReload;
    bool reload;
    float re_NeedleSoot;
    public bool isDead { get; private set; } = false;
    // Start is called before the first frame update
    void Awake() 
    {
        re_NeedleSoot = Re_Needle_Shoot;
        reload = false;
        initialReload = Re_Needle_Shoot;
        Needle_Shoot();
        myrigidbody = GetComponent<Rigidbody2D>();  
        monsterLogic();  
        Follow = "Null";
        myboxcollider = GetComponent<BoxCollider2D>();
        nextmove = 1 * EnemiesMovementSpeed;
    }

    void Needle_Shoot()
    {
        for(int rocate = -90; rocate < 91; rocate += 180 / 8)
        {
            Debug.Log("숲벌레 죽음");
            Instantiate(bullet, this.gameObject.transform.position, Quaternion.Euler(0, 0, rocate));
        }
        reload = true;

    }

    void FixedUpdate()
    {
        if(reload)
        {
            Re_Needle_Shoot -= Time.deltaTime;
            if(Re_Needle_Shoot < 0)
            {
                reload = false;
                Re_Needle_Shoot = initialReload;
                Needle_Shoot();
            }
        }
        if(isDead & dead)
        {
            deadevent();
        }
        
        if(currentHealth <= 0)
        {
            isDead = true;
        }
        myrigidbody.velocity = new Vector2(nextmove, myrigidbody.velocity.y);
        Vector2 frontVec = new Vector2(myrigidbody.position.x + Mathf.Sign(nextmove), myrigidbody.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        //떨어지지 않게
        rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));

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

    void deadevent()
    {
        dead = false;
        for(int rocate = -90; rocate < 181; rocate += 180 / 8)
        {
            Debug.Log("숲벌레 죽음");
            Instantiate(bullet, this.gameObject.transform.position, Quaternion.Euler(0, 0, rocate));
        }
        Destroy(this.gameObject, 0.5f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Froest Bug Radius Of Action" & Follow == "Null")
        {
            nextmove = Mathf.Sign(transform.parent.position.x - myrigidbody.position.x) * EnemiesMovementSpeed;
            CancelInvoke();
            Invoke("monsterLogic", Random.Range(1f, 1.5f));
        }
        else if(other.tag == "Forest Bug Nest")
        {
            Follow = "Null";
            InNest = "Out";
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

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(other);
        if(other.tag == "Player")
        {
            Follow = "Do";
        }
        if(other.tag == "Forest Bug Nest")
        {
            InNest = "In";
            Follow = "Null";
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

    void ReCrash()
    {
        Follow = "Do";
        monsterLogic();
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
