using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y - GameObject.Find("Player").GetComponent<CubeMovement>().yPosition <= -15)
        {
            deathFall();
        }
    }

    private void deathFall()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
