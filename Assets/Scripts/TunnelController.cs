using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelController : MonoBehaviour {

	public GameObject[] walls;
	private int currIdx;

    // Start is called before the first frame update
    void Start() {
        currIdx = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        	transform.Rotate(90, 0, 0);
    }

    private void onTriggerEnter(Collider other) {
    	
    	transform.Rotate(0, 0, -90);
    	// Debug.Log("collision");
    	// if (other.gameObject.CompareTag("Player")) {
    	// 	GameObject wall = other.contacts[0].thisCollider.gameObject;
    	// 	int prevIdx = (currIdx - 1) % walls.Length;
	    // 	int nextIdx = (currIdx + 1) % walls.Length;
	    	
	    // 	if (wall == walls[prevIdx]) {
	    // 		transform.Rotate(0, 0, -90);
	    // 		currIdx = prevIdx;
	    // 	}
	    // 	else if (wall == walls[nextIdx]) {
	    // 		transform.Rotate(0, 0, 90);
	    // 		currIdx = nextIdx;
	    // 	}
	    // }
    }
}
