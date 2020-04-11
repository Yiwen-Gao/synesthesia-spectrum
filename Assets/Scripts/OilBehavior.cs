using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ApplyForce(Rigidbody body)
    {
        Debug.Log("here");
        Vector3 direction = body.transform.position - transform.position;
        body.AddForceAtPosition(direction.normalized, transform.position);
    }
}
