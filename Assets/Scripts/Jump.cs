using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public bool onGround;
    private Rigidbody rb;
    public float jumpSpeed;

    //Use this for initialization
    void Start()
    {
        onGround = true;
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(0f, jumpSpeed, 0f);
                onGround = false;
            }
        }
    }

    void OnCollisionEnter(Collision any)
    {
        any.gameObject.CompareTag("Ground");
        onGround = true;
    }


}
