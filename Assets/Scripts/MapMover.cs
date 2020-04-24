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
    public float controlTime = 5f;
    //public MeshRenderer player;
    public Material canRotateColor;
    public Material opponentControlColor;
    private Vector3 startRotation;
    private float timer;
    private bool isRotating;
    private bool isInControl = false;

    void Start() {
        NetworkClient client = FindObjectOfType<NetworkClient>();
        if (client != null)
        {
            client.RegisterCallback("rotate", (string rotationState) =>
            {
                currentPosition = int.Parse(rotationState);
                Vector3 toAngle = rotationStates[currentPosition];
                StartCoroutine(RotateMe(toAngle, rotationSpeed));
            });
            client.RegisterCallback("finishedTimer", (string name) =>
            {
                isInControl = false;
                timer = controlTime;
            });
            if (client.PlayerNum == 2)
            {
                isInControl = false;
                timer = controlTime;
            }
        }
        startRotation = this.transform.rotation.eulerAngles;
        currentPosition = 0;
    }
    
    void Update()
    {
        if (!isInControl)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                NetworkClient client = FindObjectOfType<NetworkClient>();
                if (client != null)
                {
                    client.SendMessageNetwork("finishedTimer");
                }
                isInControl = true;
            }
            //player.material = opponentControlColor;
        }
        else
        {
            //player.material = canRotateColor;
        }
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
        return !isRotating && isInControl;
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
