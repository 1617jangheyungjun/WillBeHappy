using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private PlayerMovement playerScript;
    [SerializeField] float waitToLoad = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ReloadScene();
    }

    void ReloadScene()
    {
        if (playerScript.isDead)
        {
            Debug.Log("플레이어 사망" + waitToLoad);
            waitToLoad -= Time.deltaTime;
            if(waitToLoad <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
