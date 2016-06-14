using UnityEngine;
using System.Collections;

public class RayTransparencyMatching : MonoBehaviour {

    Renderer thisRenderer;

	// Use this for initialization
	void Start () {
        thisRenderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        Color currColor = thisRenderer.sharedMaterial.GetColor("_TintColor");
        thisRenderer.material.SetColor("_TintColor" , new Color(currColor.r, currColor.g, currColor.b, currColor.a - .5f*Time.deltaTime));
	}
}
