using UnityEngine;
using System.Collections;

public class MyCharController : MonoBehaviour {

    Rigidbody rb;
    public ThrowScrolls throwScrolls;

    public float speed = 0.23f;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            throwScrolls.ThrowScroll();
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        Vector3 movDir = transform.forward * Input.GetAxis("Vertical")+ transform.right * Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + movDir.normalized * speed);
    }
}
