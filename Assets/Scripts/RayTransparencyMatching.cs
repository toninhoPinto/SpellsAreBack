using UnityEngine;
using System.Collections;

public class RayTransparencyMatching : MonoBehaviour {

    Renderer thisRenderer;
    Mesh quad;

	// Use this for initialization
	void Start () {
        thisRenderer = GetComponent<Renderer>();
        quad = GetComponent<MeshFilter>().mesh;
        /*
        transform.localScale = transform.localScale * Random.value;
        transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z - quad.bounds.extents.z);
        */
    }
	
	// Update is called once per frame
	void Update () {
        Color currColor = thisRenderer.sharedMaterial.GetColor("_TintColor");
        thisRenderer.material.SetColor("_TintColor" , new Color(currColor.r, currColor.g, currColor.b, currColor.a - .5f*Time.deltaTime));
	}
}
