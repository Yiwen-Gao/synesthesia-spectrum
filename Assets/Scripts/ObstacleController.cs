using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public float speed;
    public Vector3 dir;
    private Random rand;

    // Start is called before the first frame update
    void Start() {
        rand = new Random();
    }

    // Update is called once per frame
    void Update() {
        transform.position += speed * dir;
    }

    void OnCollisionEnter(Collision other) {
    	// if ((dir.x != 0 && other.gameObject.CompareTag("Vertical Wall")) || 
     //        (dir.y != 0 && other.gameObject.CompareTag("Horizontal Wall"))) {
    	// 	dir = -dir;
    	// }

        if (other.gameObject.CompareTag("Vertical Wall") || other.gameObject.CompareTag("Horizontal Wall")) {
            dir = -dir;
        }
    }
}
