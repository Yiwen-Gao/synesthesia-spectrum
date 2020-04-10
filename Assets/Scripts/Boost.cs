using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    
    //private Rigidbody rb;
    private float boostTime;

    private bool hasBoost;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player").GetComponent<CubeMovement>().moveSpeed = 8f;
        //rb = GetComponent<Rigidbody>();

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
            if (boostTime >= 3)
            {
                //Resets to regular movement
                GameObject.Find("Player").GetComponent<CubeMovement>().moveSpeed = 8f;
                boostTime = 0;
                hasBoost = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Tag for object to give the boost
        if (other.tag == "Boost")
        {
            hasBoost = true;
            GameObject.Find("Player").GetComponent<CubeMovement>().moveSpeed = 13f;
            //Destroys the object giving the boost if we want that option
            //Destroy(other.gameObject);

        }
    }
}
