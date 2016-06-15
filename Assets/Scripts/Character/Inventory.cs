using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public ThrowScrolls throwScroll;
    public GameObject spell;

    GameObject[] equipedSpells;
    int nextSpellPos = 0;
    

	// Use this for initialization
	void Start () {
        equipedSpells = new GameObject[4];
    }
	
    public void addNewSpell(GameObject spell)
    {
        Debug.Log(nextSpellPos);
        throwScroll.setSpellToThrow(spell);
        equipedSpells[nextSpellPos] = spell;
        nextSpellPos++;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
