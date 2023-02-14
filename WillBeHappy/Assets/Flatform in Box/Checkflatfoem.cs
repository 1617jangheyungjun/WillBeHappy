using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkflatfoem : MonoBehaviour
{
    public string recall;

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Pickup Box")
        {
            Debug.Log("recall Box");
            recall = "recall";
        }    
    }
}
