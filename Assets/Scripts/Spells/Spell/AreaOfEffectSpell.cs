using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaOfEffectSpell : Spell {

    //Objects
    public SpellFXManager spellFXManager;

    //Variable Control
    public float radius = 3.0f;

    //private variables
    private List<GameObject> targets;
    private bool finishedSpell = false;

    // Use this for initialization
    void Start () {
        targets = new List<GameObject>();
        FindTargetNearby(this.transform.position, radius);
        if (targets.Count == 0)
            Destroy(this.gameObject, 0.5f);
    }

    void Update()
    {
        if (targets.Count > 0)
        {
            if (spellFXManager.ready && !finishedSpell)
            {
                CastSpell();
                finishedSpell = true;
            }
        }
    }

    private void FindTargetNearby(Vector3 originPos, float radius)
    {
        RaycastHit[] sphereHit = Physics.SphereCastAll(originPos, radius, transform.up, 10);

        for (int i = 0; i < sphereHit.Length; i++)
        {
            if (sphereHit[i].collider.transform.tag.Equals("Target"))
            {
                targets.Add(sphereHit[i].collider.gameObject);
            }
        }
    }

    private void CastSpell()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            //targets[i].transform;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
