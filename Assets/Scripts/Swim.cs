using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public float loopTime = 5;
    public Vector3 center;
    private float radius;
    private float currTime;

    private void Start()
    {
        center.y = transform.position.y;
        radius = (transform.position - center).magnitude;
        currTime = loopTime;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= loopTime)
        {
            currTime -= loopTime;
        }
        transform.position = center + radius * new Vector3(Mathf.Cos(2 * Mathf.PI * currTime / loopTime), 0, Mathf.Sin(2*Mathf.PI * currTime / loopTime));
        transform.LookAt(center);
    }
}
