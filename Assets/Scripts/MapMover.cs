using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour {
    // Start is called before the first frame update
    public float angle = 180;
    public Vector3[] rotationStates = { Vector3.zero, 
        Vector3.forward * 90, 
        Vector3.forward * 180,
        Vector3.forward * 270 };
    int currentPosition;
    public float rotationSpeed = 1.5f;
    public KeyCode rotateLeftButton;
    public KeyCode rotateRightButton;
    private Vector3 startRotation;
    bool isRotating;

    void Start() {

        NetworkClient client = FindObjectOfType<NetworkClient>();
        if (client != null)
        {
            FindObjectOfType<NetworkClient>().RegisterCallback("rotate", (string rotationState) =>
            {
                currentPosition = int.Parse(rotationState);
                Vector3 toAngle = rotationStates[currentPosition];
                StartCoroutine(RotateMe(toAngle, rotationSpeed));
            });
        }
        startRotation = this.transform.rotation.eulerAngles;
        currentPosition = 0;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(rotateRightButton) && CanRotate())
        {
            currentPosition = currentPosition - 1;
            if (currentPosition < 0)
            {
                currentPosition = currentPosition + 4;
            }
            SendMessageAndRotate(currentPosition, rotationSpeed);
        }
        else if (Input.GetKeyDown(rotateLeftButton) && CanRotate())
        {
            currentPosition = (currentPosition + 1) % 4;
            SendMessageAndRotate(currentPosition, rotationSpeed);
        }
    }

    bool CanRotate()
    {
        return !isRotating;
    }

    void SendMessageAndRotate(int rotationState, float inTime)
    {
        NetworkClient client = FindObjectOfType<NetworkClient>();
        if (client != null)
        {
            FindObjectOfType<NetworkClient>().SendMessageNetwork("rotate", rotationState.ToString());
        }
        StartCoroutine(RotateMe(rotationStates[currentPosition], inTime));
    }

    IEnumerator RotateMe(Vector3 targetAngle, float inTime)
    {
        isRotating = true;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(startRotation + targetAngle);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        transform.rotation = Quaternion.Slerp(fromAngle, toAngle, 1);
        isRotating = false;
    }
}
