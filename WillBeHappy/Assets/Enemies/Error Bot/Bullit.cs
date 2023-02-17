using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullit : MonoBehaviour
{
    Rigidbody2D rd;
    
    int rotateSpeed = 10000;
    public Transform target;
    [SerializeField] float BullitSpeed = 10f;
    int i = 1;
    // dir = (target.pos - pos).normalize
    void Start()
    {
        
        rd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() 
    {   
        if(i < 3)
        {
            Invoke("shoot", 0.01f);
            i += 1;
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
