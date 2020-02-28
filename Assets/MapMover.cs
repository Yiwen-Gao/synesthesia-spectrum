using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMover : MonoBehaviour {
    // Start is called before the first frame update
    public float angle = 180;
    public GameObject Map;

    void Start() {
        Map = GameObject.Find("Map");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Map.transform.RotateAround(0, 0, angle);
        }
    }
}
