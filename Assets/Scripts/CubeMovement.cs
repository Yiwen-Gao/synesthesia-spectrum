using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rb;

    //Boost variables
    private float boostTime;

    private bool hasBoost;
    
    public float yPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 8f;
        rb = GetComponent<Rigidbody>();
        yPosition = rb.position.y;

        boostTime = 0;
        hasBoost = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed*Input.GetAxis("Horizontal")*Time.deltaTime, 0f, moveSpeed*Input.GetAxis("Vertical")*Time.deltaTime);

        
        //When player gets a speed boost
        if (hasBoost)
        {
            //Setting the time for the boost
            boostTime += Time.deltaTime;
            //Ends the boost after x amount of time
            if(boostTime >= 3)
            {
                //Resets to regular movement
                moveSpeed = 8f;
                boostTime = 0;
                hasBoost = false;
            }
        }
        

    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Tag for object to give the boost
        if(other.tag == "Boost")
        {
            hasBoost = true;
            moveSpeed = 13f;
            //Destroys the object giving the boost if we want that option
            //Destroy(other.gameObject);

        }
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Physics.gravity = new Vector3(-9.8f, 0, 0);
        }
    }
}
