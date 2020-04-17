using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    private Rigidbody rb;
    private float yPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        yPosition = rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Player").GetComponent<CubeMovement>().

        //Current y position subtracted by original y position when the player starts
        if (rb.position.y - yPosition <= -10)
        {
            deathFall();
        }
    }

    private void deathFall()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
