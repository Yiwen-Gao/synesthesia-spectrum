using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{

    public Material color;
    // Start is called before the first frame update
    void Start()
    {
        NetworkClient nc = FindObjectOfType<NetworkClient>();
        nc.RegisterCallback("died", (string name) => {
            gameObject.GetComponent<MeshRenderer>().material = color;
            Debug.Log(name);
        });
    }
}
