using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullit : MonoBehaviour
{
    Rigidbody2D rd;
    Vector2 Pv;
    Vector2 dir;
    [SerializeField] float BullitSpeed = 10f;
    int i = 1;
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        Pv = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position;
    }

    // Update is called once per frame
    void Update() 
    {   
        dir = new Vector2(Pv.x - rd.position.x, Pv.y - rd.position.y);
        if(i == 1)
        {
            shoot();
            i += 1;
        }
        
        
    }

    void shoot()
    {
        rd.velocity = new Vector2 (dir.x * BullitSpeed, dir.y * BullitSpeed);
    }
    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(this.gameObject,0);   
    }
}
