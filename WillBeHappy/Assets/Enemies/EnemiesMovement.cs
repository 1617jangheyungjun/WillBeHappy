using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    Rigidbody2D myrigidbody; 
    public float nextmove;
    [SerializeField] float EnemiesMovementSpeed = 10f;
    [SerializeField] float EnemiesMovementJumpSpeed = 10f;
    RaycastHit2D JumprayHit;
    RaycastHit2D rayHit;
    RaycastHit2D CheckDojump1;
    RaycastHit2D CheckDojump2;
    public string status = "Idle";
    float whatmove;
    // Start is called before the first frame update
    void Awake() 
    {
        myrigidbody = GetComponent<Rigidbody2D>();  
        monsterLogic();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myrigidbody.velocity = new Vector2(nextmove, myrigidbody.velocity.y);

        Vector2 JumpVec = new Vector3(myrigidbody.position.x + Mathf.Sign(nextmove) * 2, Mathf.Round(myrigidbody.position.y) + 3);
        Vector2 frontVec = new Vector2(myrigidbody.position.x + Mathf.Sign(nextmove), myrigidbody.position.y);
        Vector2 CheckDoJump1 = new Vector2(myrigidbody.position.x, myrigidbody.position.y + 2);
        Vector2 CheckDoJump2 = new Vector2(myrigidbody.position.x + Mathf.Sign(nextmove) * 1.8f, myrigidbody.position.y + 2);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        Debug.DrawRay(JumpVec, Vector3.down);
        Debug.DrawRay(CheckDoJump1, new Vector2(0, -2), new Color(0, 0, 1));
        Debug.DrawRay(CheckDoJump2, new Vector2(0, -2), new Color(0, 0, 1));
        //떨어지지 않게
        rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        //점프할 곳이 있는지
        JumprayHit = Physics2D.Raycast(JumpVec, Vector3.down, -1, LayerMask.GetMask("Ground"));
        CheckDojump1 = Physics2D.Raycast(CheckDoJump1, new Vector2(0, -2), -2, LayerMask.GetMask("Ground"));
        CheckDojump2 = Physics2D.Raycast(CheckDoJump2, new Vector2(0, -2), -2, LayerMask.GetMask("Ground"));
    }

    void ChangeIdle()
    {
        status = "Idle";
        nextmove *= 2;
    }



    void Update() 
    {
        if(status == "Idle")
        {
            if (rayHit.collider == null)
            {
                nextmove *= -1;
                CancelInvoke();
                Invoke("monsterLogic", Random.Range(0.3f, 4f));    
            }
        }
        if (JumprayHit.collider != null & status == "Idle" & CheckDojump1.collider == null & CheckDojump2.collider == null)
        {
            status = "Jump";
            whatmove = nextmove;
            nextmove *= 0.5f;
            Debug.Log("닿음");
            int JumpOrNot = Random.Range(0,2);
            if (JumpOrNot == 1)
            {
                myrigidbody.velocity = new Vector2(myrigidbody.velocity.x, EnemiesMovementJumpSpeed);
            }
            Invoke("ChangeIdle", 1.4f);
            Invoke("monsterLogic", Random.Range(1f, 4f));
        }
    }
    void nextJumpMove()
    {
        Debug.Log("출발");
        myrigidbody.velocity = new Vector2(whatmove, myrigidbody.velocity.y);
    }
    void monsterLogic()
    {
        if (status == "Idle")
        {
            nextmove = Random.Range(-1, 2) * EnemiesMovementSpeed;

            Invoke("monsterLogic", Random.Range(0.3f, 4f));
        }
    }
}
