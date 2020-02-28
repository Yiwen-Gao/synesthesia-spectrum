using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public float speed;
	private float dir;
	public float offsetZ;

    // Start is called before the first frame update
    void Start() {
        dir = 1;
    }

    // Update is called once per frame
    void Update() {
    	Vector3 offset = new Vector3(0f, 0f, dir * offsetZ);
        transform.position += offset;
    }

    void OnCollisionEnter(Collision other) {
    	if (other.gameObject.CompareTag("Wall")){
    		dir = -dir;
    	}
    }
}
