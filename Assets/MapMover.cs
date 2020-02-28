using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour
{
    // Start is called before the first frame update
    public float angle = 180;
    int currentPosition;
    public GameObject Map;
    public float rotationSpeed = 1.5f;

    void Start()
    {
        Map = GameObject.Find("Map");
        currentPosition = 0;
    }

    //T corresponds to turquoise wall (currentPosition = 1)
    //O corresponds to orange wall (currentPosition = 2)
    //G correspond to gray which is starting ground (currentPosition = 0)
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.G)) && (currentPosition == 1))
        {
            //Map.transform.Rotate(0, 0, 270);
            currentPosition = 0;
            StartCoroutine(RotateMe(Vector3.forward * 270, rotationSpeed));

        }

        if ((Input.GetKeyDown(KeyCode.O)) && (currentPosition == 1))
        {
            //Map.transform.Rotate(0, 0, 180);
            currentPosition = 2;
            StartCoroutine(RotateMe(Vector3.forward * -180, rotationSpeed));

        }


        if (Input.GetKeyDown(KeyCode.O) && (currentPosition == 0))
        {
            //Map.transform.Rotate(0, 0, 270);
            currentPosition = 2;
            StartCoroutine(RotateMe(Vector3.forward * 270, rotationSpeed));

        }

        if (Input.GetKeyDown(KeyCode.T) && (currentPosition == 0))
        {
            //Map.transform.Rotate(0, 0, 90);
            currentPosition = 1;
            StartCoroutine(RotateMe(Vector3.forward * 90, rotationSpeed));
        }

        if (Input.GetKeyDown(KeyCode.T) && (currentPosition == 2))
        {
            //Map.transform.Rotate(0, 0, 180);
            currentPosition = 1;
            StartCoroutine(RotateMe(Vector3.forward * -180, rotationSpeed));
        }

        if (Input.GetKeyDown(KeyCode.G) && (currentPosition == 2))
        {
            //Map.transform.Rotate(0, 0, 90);
            currentPosition = 0;
            StartCoroutine(RotateMe(Vector3.forward * 90, rotationSpeed));
        }
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = Quaternion.Slerp(fromAngle, toAngle, 1);
    }
}
