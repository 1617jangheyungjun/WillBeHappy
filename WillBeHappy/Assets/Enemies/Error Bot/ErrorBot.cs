using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorBot : MonoBehaviour
{
    public string Follow = "Null";
    public string status = "Idle";
    public string InNest = "In";

    [SerializeField][Range(0.1f, 1f)] float ReloadTime = 0.5f;

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

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "UpNest")
        {
            status = "Warn";
        }    
    }
    void FixedUpdate()
    {
        flip();
        if (Follow == "Null")
        {
            if (status == "Idle")
            {
                errorThink();
                CancelInvoke();
            }
        }

        if (Follow == "Do")
        {
            if (status == "Idle")
            {
                swing();
                errorThink();
            }

            else if (status == "Warn")
            {
                swing();
                errorThink();
                CancelInvoke();
                Invoke("ShootGun", ReloadTime);
                status = "Idle";
            }
        }

        if (this.transform.parent.transform.GetChild(0).GetComponent<Rigidbody2D>().position - rd.position == new Vector2(0, 0))
        {
            Follow = "Shoot";
        }
    }

    void flip()
    {
        transform.localScale = new Vector2(Mathf.Sign(target.position.x - transform.position.x) ,1f);
    }
    
    void ShootGun()
    {
        Vector2 dir = target.position - transform.position;
        Instantiate(bullet, Gun.position, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90));  
        Invoke("ShootGun", ReloadTime);    
    }

    void errorThink()
    {
        if(Follow == "Null")
        {
            Debug.Log("에러봇이 생각을 시작함");
            rd.velocity = (this.transform.parent.GetComponent<Rigidbody2D>().position - rd.position) * 2;
            Debug.Log(gameObject.GetComponentInParent<Rigidbody2D>().position);
        }

        else if(Follow == "Do")
        {
            rd.velocity = (this.transform.parent.transform.GetChild(0).GetComponent<Rigidbody2D>().position - rd.position) * 2;
        }
    }
    void swing()
    {
        runningTime += Time.deltaTime * speed;
        yPos = Mathf.Sin(runningTime) * length;
        rd.velocity = new Vector2(nextmove, yPos);
    }
}
