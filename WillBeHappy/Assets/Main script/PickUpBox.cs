using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBox : MonoBehaviour
{
    Checkinother check;
    public string Status = "None Box";
    CapsuleCollider2D mycapsulecollider;
    Vector2 Pos;
    Vector2 BoxPos;
    Vector3 Scale;
    Rigidbody2D myrigidbody;
    public Transform prefab;
    string pick = "not pick";
    // Start is called before the first frame update
    void Start() 
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        mycapsulecollider = GetComponent<CapsuleCollider2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Box")
        {
            pick = "pick";
            other.gameObject.transform.parent.tag = "Player Box";
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag == "Box")
        {
            pick = "pick";
            other.gameObject.tag = "Player Box";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player Box")
        {
            pick = "not pick";
            Debug.Log("ddddd");
            other.gameObject.tag = "Box";
        }
    }
    void status()
    {
        Status = "Pick";
    }

    void FixedUpdate() 
    {
        check = GameObject.Find("Box 1(Clone)").GetComponent<Checkinother>();
    }
    void Update() 
    {
        Debug.Log(pick);
        if (pick == "pick" & Status != "Pick")
        {
            Debug.Log("Enter the Box Collider");
            if(Input.GetKeyDown(KeyCode.E))
            {
                GameObject.FindWithTag("Player Box").gameObject.tag = "Pickup Box";
                Destroy(GameObject.FindWithTag("Pickup Box"), 0);
                Invoke("status", 0.5f);
            }
        }
        Debug.Log(Status);
        Pos = this.gameObject.transform.position;
        Scale = this.gameObject.transform.localScale;
        if(Input.GetKeyDown(KeyCode.E) & Status == "Pick")
        {
            Debug.Log("Box down");
            Instantiate(prefab, new Vector3(0,0,0), Quaternion.identity);
            GameObject.FindWithTag("Pickup Box").transform.position = new Vector2(Pos.x - Scale.x * 1.3f, Pos.y + Scale.y / 2f);  
            Invoke("changetag", 0.1f);
            Status = "None Box";
        }
        if(check.recall == "recall")
        {
            check.recall = "none";
            Debug.Log("Succes");
            Destroy(GameObject.FindWithTag("Pickup Box"), 0);
            Status = "Pick";
        } 
    }

    void changetag()
    {
        GameObject.FindWithTag("Pickup Box").tag = "Box";
    }

}
