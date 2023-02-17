using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float RunSpeed = 10f;
    [SerializeField] float JumpSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    GameObject Jump;
    BoxCollider2D myboxcollider;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myboxcollider = GetComponent<BoxCollider2D>();
        
    }

    void Update() 
    {
        
    }
    // Update is called once per frame
    void FixedUpdate() 
    {
        Run();
    }
    


    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        Debug.Log("스페이스바 눌렀어요");
        if (!myboxcollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myboxcollider.IsTouchingLayers(LayerMask.GetMask("Box")) && !myboxcollider.IsTouchingLayers(LayerMask.GetMask("Object"))) {return;}

        if(value.isPressed)
        {
            myrigidbody.velocity += new Vector2 (0f,JumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * RunSpeed, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        Debug.Log(moveInput.x * RunSpeed);
        Debug.Log("velocity = " + myrigidbody.velocity);
    }
}
