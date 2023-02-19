using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    ErrorBot errorbot;
    void Awake()
    {
        errorbot = transform.GetChild(1).GetComponent<ErrorBot>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {   
            Debug.Log("들어옴");
            errorbot.Follow = "Do";
        }    
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("들어옴, 나감");
            errorbot.Follow = "Null";
        }       
    }
}
