using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAndSize : MonoBehaviour
{
    [SerializeField] float AddSize = 5f;
    [SerializeField] float delay = 3f;
    string charactersize;
    Rigidbody2D myrigidbody;
    string stop;
    Vector2 lastFlip;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        charactersize = "Normal";
        stop = "now";
    }

    void Update()
    {
        Debug.Log(charactersize);
        if(charactersize == "Normal")
        {
            FlipSprite();
        }
        else
        {
            FlipSpriteBig();
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Milk")
        {
            Debug.Log("milk");
            Destroy(other.gameObject, 0);
            charactersize = "big";
            Invoke("backsize", delay);
        }
    }

    void backsize()
    {
        charactersize = "Normal";
    }
    void FlipSprite()
    {
        if(Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2 (-1, 1f);
        }
        else if(Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2 (1, 1f);
        }
        if (stop == "no")
        {
            transform.localScale = new Vector2 (Mathf.Sign(lastFlip.x), 1f);
            stop = "now";
        }
    }

    void FlipSpriteBig()
    {
        if(Input.GetKey(KeyCode.A) & !Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector2 ( 1 * AddSize, 1f * AddSize);
        }
        else if(Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector2 ( -1 * AddSize, 1f * AddSize);
        }
        if(stop == "now")
        {
            transform.localScale *= new Vector2 (AddSize, AddSize);
        }
        stop = "no";
        
        lastFlip = transform.localScale;
    }
}
