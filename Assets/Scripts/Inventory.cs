using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public ThrowScrolls throwScroll;
    public GameObject spell;

	// Use this for initialization
	void Start () {
        throwScroll.setSpellToThrow(spell);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
