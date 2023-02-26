using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    Rigidbody2D rd;
    [SerializeField] int number_of_bullets = 6;
    [SerializeField] [Range(1, 5)] float ReloadTime = 1.5f;
    [SerializeField] [Range(0.2f, 5f)] float Shoot_Delay = 0.3f;
    public bool isShoot = true;
    private float initial_Shoot_Delay;
    private float initial_ReloadTime;
    private int initial_NOB;
    private PlayerMovement PlayerScript;
    private Vector2 mouse_position, target;
    private float angle;
    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        PlayerScript = GetComponent<PlayerMovement>();
        initial_NOB = number_of_bullets;
        initial_Shoot_Delay = Shoot_Delay;
        initial_ReloadTime = ReloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        ReloadTime -= Time.deltaTime;
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = rd.position;
        angle = Mathf.Atan2(mouse_position.y - target.y, mouse_position.x - target.x) * Mathf.Rad2Deg;
        shoot();
    }

    void shoot()
    {
        if(ReloadTime < 0)
        {
            number_of_bullets = initial_NOB;
        }
        if(!isShoot)
        {
            Shoot_Delay -= Time.deltaTime;
            if(Shoot_Delay < 0 & number_of_bullets > 0)
            {
                Shoot_Delay = initial_Shoot_Delay;
                isShoot = true;
            }
        }
        if(!PlayerScript.iscat)
        {
            if(number_of_bullets < initial_NOB)
            {
                ReloadTime -= Time.deltaTime;
            }
        }
    }

    void OnFire(InputValue value)
    {
        if(value.isPressed & isShoot)
        {
            Debug.Log(Shoot_Delay);
            if(number_of_bullets > 0)
            {
                number_of_bullets -= 1;
                ReloadTime = initial_ReloadTime;
                if(number_of_bullets > 0 & isShoot)
                {
                    Debug.Log("퓨슝");
                    Instantiate(bullet, rd.position, Quaternion.AngleAxis(angle-90, Vector3.forward));
                }
            }
            isShoot = false;
        }
    }
}
