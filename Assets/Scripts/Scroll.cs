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
            GameObject castedSpell = (GameObject)Instantiate(spell, collision.contacts[0].point + antiClippingDir*0.3f, spell.transform.rotation);
            castedSpell.transform.rotation = Quaternion.FromToRotation(castedSpell.transform.up, collision.contacts[0].normal);
            Destroy(this.gameObject);
        }
    }

}
