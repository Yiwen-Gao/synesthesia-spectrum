using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostforPlayer : MonoBehaviour
{
    private float originalSpeed;
    private Rigidbody rb;

    private float boostTime;

    private bool hasBoost;
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = GameObject.Find("Player").GetComponent<PlayerController>().speed;
        rb = GetComponent<Rigidbody>();


        boostTime = 0;
        hasBoost = false;
    }

    // Update is called once per frame
    void Update()
    {
        //When player gets a speed boost
        if (hasBoost)
        {
            //Setting the time for the boost
            boostTime += Time.deltaTime;
            //Ends the boost after x amount of time
            if (boostTime >= 2)
            {
                //Resets to regular movement
                GameObject.Find("Player").GetComponent<PlayerController>().speed = originalSpeed;
                boostTime = 0;
                hasBoost = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Tag for object to give the boost
        if (collision.gameObject.CompareTag("Boost"))
        {
            hasBoost = true;
            GameObject.Find("Player").GetComponent<PlayerController>().speed = originalSpeed + 5;
            //Destroys the object giving the boost if we want that option
            //Destroy(other.gameObject);

        }
    }
}
