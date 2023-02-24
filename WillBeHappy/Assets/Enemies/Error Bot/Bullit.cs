using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AllUnits;

public class Bullit : Unit
{
    [SerializeField] float bullit_destroy = 10f;
    Rigidbody2D rd;
    [SerializeField] float BullitSpeed = 10f;
    int i = 1;
    // dir = (target.pos - pos).normalize
    void Awake()
    {
        
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {   
        if(i < 3)
        {
            shoot();
            i += 1;
        }
        bullit_destroy -= Time.deltaTime;
        if(bullit_destroy < 0)
        {
            Destroy(this.gameObject);
        }
        
    }

    void shoot()
    {
        rd.AddRelativeForce(new Vector2(0, BullitSpeed), ForceMode2D.Impulse);
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(this.gameObject,0);   
    }
}
