using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public float speed;
    public Vector3 dir;
	private float offset;

    // Start is called before the first frame update
    void Start() {
        offset = .1f;
    }

    // Update is called once per frame
    void Update() {
        transform.position += offset * dir;
    }

    void OnCollisionEnter(Collision other) {
    	if (other.gameObject.CompareTag("Wall")){
    		dir = -dir;
    	}
    }
}
