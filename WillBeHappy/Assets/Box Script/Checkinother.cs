using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkinother : MonoBehaviour
{
    public string recall = "none";
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(this.gameObject.tag == "Pickup Box" & !(other.gameObject.tag == "Player"))
        {
            Debug.Log("Check");
            recall = "recall";
            Debug.Log(recall);
            Invoke("vh", 0.3f);
        }
            
    }

    void vh()
    {
        recall = "none";
    }

    private void Update() {
        Debug.Log(recall);
    }
}
