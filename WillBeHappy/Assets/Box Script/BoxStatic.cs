using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStatic : MonoBehaviour
{
    Rigidbody2D myrigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Ground")
        {
            myrigidbody.bodyType = RigidbodyType2D.Static;
        }    
    }

    
}
