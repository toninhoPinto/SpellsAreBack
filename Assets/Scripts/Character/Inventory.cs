using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public ThrowScrolls throwScroll;
    public GameObject spell;

    GameObject[] equipedSpells;
    int nextSpellPos = 0;

    int equipedSpellID = -1;
    float timeOfLastScroll = 0;
    float spellScrollSpeed = 2f;
    int spellNumberMax = 4;

	// Use this for initialization
	void Start () {
        equipedSpells = new GameObject[spellNumberMax];
    }
	
    public void addNewSpell(GameObject spell)
    {
        spell.transform.parent = spell.transform;
        throwScroll.setSpellToThrow(spell);
        equipedSpellID = nextSpellPos;
        equipedSpells[nextSpellPos] = spell;
        nextSpellPos = (nextSpellPos + 1) % spellNumberMax;
        Debug.Log(nextSpellPos);
        Debug.Log(equipedSpellID);
    }

	// Update is called once per frame
	void Update () {
	    
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && Time.time - timeOfLastScroll > spellScrollSpeed)
        {
            timeOfLastScroll = Time.time;
            equipedSpellID = (equipedSpellID + 1) % nextSpellPos;
            throwScroll.setSpellToThrow(equipedSpells[equipedSpellID]);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Time.time - timeOfLastScroll > spellScrollSpeed)
        {
            timeOfLastScroll = Time.time;
            equipedSpellID = (equipedSpellID - 1) % nextSpellPos;
            throwScroll.setSpellToThrow(equipedSpells[equipedSpellID]);
        }
    }
}
