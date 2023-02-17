using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBot : MonoBehaviour
{
    public string Follow = "Null";
    public string status = "Idle";
    public string InNest = "In";

    [SerializeField] GameObject bullet;

    [SerializeField] Transform Gun;

    [SerializeField][Range(0f, 10f)] float speed = 1f;
    [SerializeField][Range(0f, 10f)] float length = 1f;

    [SerializeField] int EnemiesMovementSpeed = 10;

    [SerializeField] Transform target;

    Rigidbody2D rd;
    public int nextmove;
    float runningTime = 0f;
    float yPos = 0f;
    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        rd.velocity = new Vector2(0, 0);
    }


    void FixedUpdate()
    {
        swing();
        if (Follow == "Null")
        {
            if (status == "Idle")
            {
                Invoke("errorThink", Random.Range(1f, 1.5f));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player" & Follow == "Do")
        {
            Debug.Log("전투모드 해제");
            Follow = "Null";
            errorThink();
        }
        if(other.tag == "Error Nest")
        {
            nextmove *= -1;
            CancelInvoke();
            Invoke("errorThink", Random.Range(1f, 1.5f));
        }    
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" & Follow == "Null")
        {
            Follow = "Do";
            nextmove = 0;
            CancelInvoke();
            Invoke("ShootGun", 0.5f);
            Debug.Log("전투모드");  
        }
            
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" & Follow == "Null")
        {
            Follow = "Do";
            nextmove = 0;
            CancelInvoke();
            Invoke("ShootGun", 0.5f);
            Debug.Log("전투모드");  
        }
            
    }
    void ShootGun()
    {
        Vector2 dir = target.position - transform.position;
        Instantiate(bullet, Gun.position, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90));  
        Invoke("ShootGun", 0.5f);    
    }

    void errorThink()
    {
        Debug.Log("에러봇이 생각을 시작함");
        if(Follow == "Null")
        {
            nextmove = Random.Range(-1,2) * EnemiesMovementSpeed;
        }
        CancelInvoke();
        Invoke("errorThink", Random.Range(1f, 1.5f));
    }
    void swing()
    {
        runningTime += Time.deltaTime * speed;
        yPos = Mathf.Sin(runningTime) * length;
        rd.velocity = new Vector2(nextmove, yPos);
    }
}
