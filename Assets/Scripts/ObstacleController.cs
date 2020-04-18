using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public float speed;
    public Vector3 dir;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position += speed * dir;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall")) {
            dir = -dir; //new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
        }
    }
}
