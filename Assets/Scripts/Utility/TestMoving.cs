using UnityEngine;
using System.Collections;

public class TestMoving : MonoBehaviour {
    public float movementSpeed = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(0,0, movementSpeed * Time.deltaTime);
	}
}
