using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PDollarGestureRecognizer;

public class SpellRecognizer : MonoBehaviour {

    public GameObject[] Spells;

	// Use this for initialization
	void Start () {
    }
	
	public void RecognizeSpell(List<Gesture> gestures, Inventory inv)
    {
        GameObject newSpell = null;
        for (int i = 0; i < gestures.Count; i++) {

            if (gestures[i].Name.Equals("square"))
            {
                if(newSpell == null)
                    newSpell = (GameObject)Instantiate(Spells[1], transform.position, Quaternion.identity);
                else if (newSpell.GetComponent<SummonSpell>() != null)
                     newSpell.GetComponent<ProjectileSpell>().UpgradeSpell();
            }
            else if (gestures[i].Name.Equals("triangle"))
            {
                if (newSpell == null)
                    newSpell = (GameObject)Instantiate(Spells[1], transform.position, Quaternion.identity);
                else if (newSpell.GetComponent<ProjectileSpell>() != null)
                    newSpell.GetComponent<ProjectileSpell>().UpgradeSpell();
            }
            else if(gestures[i].Name.Equals("circle"))
            {
                if (newSpell == null)
                    newSpell = (GameObject)Instantiate(Spells[0], transform.position, Quaternion.identity);
                else if(newSpell.GetComponent<AreaOfEffectSpell>() != null)
                    newSpell.GetComponent<AreaOfEffectSpell>().UpgradeSpell();
            }
            else if(gestures[i].Name.Equals("semicircle"))
            {
            }
            else if(gestures[i].Name.Equals("fire"))
            {
            }
            else if(gestures[i].Name.Equals("rock"))
            {
            }
            else if(gestures[i].Name.Equals("water"))
            {
            }
            else if(gestures[i].Name.Equals("wind"))
            {
            }
            else if(gestures[i].Name.Equals("lightning"))
            {
            }
            else if(gestures[i].Name.Equals("poison"))
            {
            }
            else if(gestures[i].Name.Equals("arcane"))
            {
            }
            else if(gestures[i].Name.Equals("death"))
            {
            }
        }
        newSpell.SetActive(false);
        inv.addNewSpell(newSpell);
    } 



}
