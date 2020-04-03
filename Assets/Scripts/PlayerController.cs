using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 20;
	public Rigidbody rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void FixedUpdate() {
    	float horizontalInput = Input.GetAxis("Horizontal");
    	float verticalInput = Input.GetAxis("Vertical");
    	Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
    	rb.AddForce(movement * speed);

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Physics.gravity = new Vector3(-9.8f, 0, 0);
        }
    }
}
