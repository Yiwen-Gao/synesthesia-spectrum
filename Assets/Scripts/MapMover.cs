using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour {
    // Start is called before the first frame update
    public float angle = 180;
    int currentPosition;
    public GameObject Map;
    public float rotationSpeed = 1.5f;

    void Start() {
        FindObjectOfType<NetworkClient>().RegisterCallback("rotate", (string rotation) =>
        {
            string[] anglesList = rotation.Split(',');
            Vector3 byAngles = new Vector3(float.Parse(anglesList[0]), float.Parse(anglesList[1]), float.Parse(anglesList[2]));
            StartCoroutine(RotateMe(byAngles, rotationSpeed));
        });
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
            SendMessageAndRotate(Vector3.forward * 270, rotationSpeed);

        }

        if ((Input.GetKeyDown(KeyCode.O)) && (currentPosition == 1))
        {
            //Map.transform.Rotate(0, 0, 180);
            currentPosition = 2;
            SendMessageAndRotate(Vector3.forward * -180, rotationSpeed);

        }


        if (Input.GetKeyDown(KeyCode.O) && (currentPosition == 0))
        {
            //Map.transform.Rotate(0, 0, 270);
            currentPosition = 2;
            SendMessageAndRotate(Vector3.forward * 270, rotationSpeed);

        }

        if (Input.GetKeyDown(KeyCode.T) && (currentPosition == 0))
        {
            //Map.transform.Rotate(0, 0, 90);
            currentPosition = 1;
            SendMessageAndRotate(Vector3.forward * 90, rotationSpeed);
        }

        if (Input.GetKeyDown(KeyCode.T) && (currentPosition == 2))
        {
            //Map.transform.Rotate(0, 0, 180);
            currentPosition = 1;
            SendMessageAndRotate(Vector3.forward * -180, rotationSpeed);
        }

        if (Input.GetKeyDown(KeyCode.G) && (currentPosition == 2))
        {
            //Map.transform.Rotate(0, 0, 90);
            currentPosition = 0;
            SendMessageAndRotate(Vector3.forward * 90, rotationSpeed);
        }
    }

    void SendMessageAndRotate(Vector3 byAngles, float inTime)
    {
        FindObjectOfType<NetworkClient>().SendMessageNetwork("rotate", byAngles[0] + "," + byAngles[1] + "," + byAngles[2]);
        StartCoroutine(RotateMe(byAngles, inTime));
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
