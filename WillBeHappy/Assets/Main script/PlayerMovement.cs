using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AllUnits;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : Unit
{
    [SerializeField] float RunSpeed = 10f;
    [SerializeField] float JumpSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    GameObject Jump;
    BoxCollider2D myboxcollider;
    public bool isDead { get; private set; } = false;
    void Awake()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myboxcollider = GetComponent<BoxCollider2D>();
        
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

    void OnCollisionEnter2D(Collision2D other) 
    {
        if((other.gameObject.tag == "Leafy") & !isDamage)
        {
            isDamage = true;
            float enemyAttack = other.gameObject.GetComponent<EnemiesMovement>().damage;
            currentHealth -= enemyAttack;
        }
        if(other.gameObject.tag == "Enemy Bullet" & !isDamage)
        {
            isDamage = true;
            float enemyAttack = other.gameObject.GetComponent<Bullit>().damage;
            currentHealth -= enemyAttack;
        }
        if (currentHealth <= 0)
            {
                isDead = true;
                gameObject.SetActive(false);
            }    
    }
}
