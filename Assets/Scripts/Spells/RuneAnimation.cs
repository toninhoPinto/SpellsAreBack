using UnityEngine;
using System.Collections;

public class RuneAnimation : MonoBehaviour {

    public float time = 0f;
    public float radius = 10f;
    public float rotationSpeed = 3f;

    // Use this for initialization
    void Start () {
        transform.localScale = transform.localScale * radius * 2.5f;
    }
	
	// Update is called once per frame
	void Update () {
        time = Mathf.Min(time + 2f * Time.deltaTime, 1f);
        //transform.localScale = transform.localScale * radius * 2.5f * Time.deltaTime;
        transform.RotateAround(transform.position, transform.forward, rotationSpeed * Time.deltaTime * 90f);
    }
}
