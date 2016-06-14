using UnityEngine;
using System.Collections;

public class ThrowScrolls : MonoBehaviour {

    public GameObject scroll;
    public float throwStrength = 5000f;
    private Rigidbody scrollRB;
    private GameObject spell;

    // Use this for initialization
    void Start () {
    }
	
    public void setSpellToThrow(GameObject spell)
    {
        this.spell = spell;
    }

    public void ThrowScroll()
    {
        GameObject instScroll = (GameObject)Instantiate(scroll, this.transform.position, Quaternion.identity);
        instScroll.GetComponent<Scroll>().spell = this.spell;
        scrollRB = instScroll.GetComponent<Rigidbody>();
        scrollRB.AddForce((transform.forward + transform.up) * throwStrength);
        scrollRB.AddTorque(transform.right * 10000);

    }
}
