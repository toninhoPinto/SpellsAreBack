using UnityEngine;
using System.Collections;

public class SummonSpell : MonoBehaviour {

    //Objects
    public SpellFXManager spellFXManager;
    public GameObject minion;
    public int spawnNumber = 1;

    void Update()
    {
        if (spellFXManager.ready)
        {
            CastSpell();
        }
    }

    private void CastSpell()
    {
        for(int i = 0; i < spawnNumber; i++)
            Instantiate(minion, this.transform.position, Quaternion.identity);
    }

    public void UpgradeSpell()
    {
        spawnNumber++;
    }

}
