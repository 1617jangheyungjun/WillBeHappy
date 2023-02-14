using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : MonoBehaviour
{
    [SerializeField] float AddScale = 10f;
    void OnTriggerEnter2D(Collider2D other) 
    {
        other.transform.localScale *= new Vector2(AddScale, AddScale);
        Destroy(gameObject, 0f);
        Invoke("size_back", 3f);
    }
    
}
