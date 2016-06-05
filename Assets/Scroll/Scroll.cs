using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    public GameObject spell;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if(!collision.transform.tag.Equals("Untagged") && !collision.transform.tag.Equals("Player")){
            Vector3 antiClippingDir = collision.contacts[0].normal.normalized;
            Instantiate(spell, collision.contacts[0].point + antiClippingDir, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
