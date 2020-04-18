using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour {

	public GameObject[] walls;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        	transform.Rotate(90, 0, 0);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.Rotate(-90, 0, 0);
    }

}
