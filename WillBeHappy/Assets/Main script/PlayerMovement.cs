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

    [SerializeField] float BoubleJumpSpeed = 10f;
    
    [SerializeField] [Range(1, 100)] float Shiver = 10f;

    public bool Shake;


    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    GameObject Jump;
    BoxCollider2D myboxcollider;
    SpriteRenderer SR;
    int other_range;
    int doubleJump = 1;
    public bool isDead { get; private set; } = false;
    void Awake()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myboxcollider = GetComponent<BoxCollider2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        Run();
        HitMove();
    }
    


    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        Debug.Log("스페이스바 눌렀어요");
        if (!myboxcollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myboxcollider.IsTouchingLayers(LayerMask.GetMask("Box")) && !myboxcollider.IsTouchingLayers(LayerMask.GetMask("Object"))) 
        {
            if(doubleJump == 2)
            {
                myrigidbody.velocity = new Vector2 (myrigidbody.velocity.x,BoubleJumpSpeed);
            }

            else{return;}
        }

        if (myboxcollider.IsTouchingLayers(LayerMask.GetMask("Ground")) | myboxcollider.IsTouchingLayers(LayerMask.GetMask("Box")) | myboxcollider.IsTouchingLayers(LayerMask.GetMask("Object"))) 
        {
            doubleJump = 1;
        }
        
        if(value.isPressed)
        {
            myrigidbody.velocity += new Vector2 (0f,JumpSpeed);
            doubleJump += 1;
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
            myrigidbody.velocity = new Vector2(Mathf.Sign(other.transform.position.x - myrigidbody.position.x) * 20, 5f);
            SR.color = Color.red;
            Shake = true;
        }
        if(other.gameObject.tag == "Enemy Bullet" & !isDamage)
        {
            isDamage = true;
            float enemyAttack = other.gameObject.GetComponent<Bullit>().damage;
            currentHealth -= enemyAttack;
            myrigidbody.velocity = new Vector2(Mathf.Sign(other.transform.position.x - myrigidbody.position.x) * 20, 5f);
            SR.color = Color.red;

        }
        if (currentHealth <= 0)
            {
                isDead = true;
                gameObject.SetActive(false);
            }    
    }

    void HitMove()
    {
        if (damageDelay > 0.4)
        {
            transform.localEulerAngles = new Vector3(0, 0,  initialDamageDelay * Shiver - damageDelay * Shiver);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            SR.color = Color.white;
            Shake = false;
        }
    }
}
