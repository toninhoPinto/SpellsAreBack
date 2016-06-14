using UnityEngine;
using System.Collections;

public class SummonSpell : MonoBehaviour {

    //Objects
    public SpellFXManager spellFXManager;
    public GameObject minion;

    void Update()
    {
        if (spellFXManager.ready)
        {
            CastSpell();
        }
    }

    private void CastSpell()
    {
        Instantiate(minion, this.transform.position, Quaternion.identity);
    }
}
