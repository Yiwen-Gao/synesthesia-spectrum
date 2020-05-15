using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public float loopTime = 5;
    public GameObject center;
    private float radius;
    private float currTime;

    private void Start()
    {
        radius = (transform.position - center.transform.position).magnitude;
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
        float theta = 2 * Mathf.PI * currTime / loopTime;
        Vector3 offset = radius * new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
        offset = center.transform.rotation * offset;
        transform.position = center.transform.position + offset;
        transform.LookAt(center.transform.position);
        Vector3 rotate = transform.rotation.eulerAngles;
        if (rotate.x > 1 && rotate.y < 181 && rotate.y > 179)
        {
            rotate.y = 180 - rotate.y;
            transform.Rotate(new Vector3(180, 180, 0));
        }
        transform.rotation = transform.rotation * center.transform.rotation;
    }
}
