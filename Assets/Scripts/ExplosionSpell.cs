using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionSpell : MonoBehaviour {

    //Objects
    public GameObject projectilePrefab;

    //Variable Control
    public float radius = 3.0f;

    //private variables
    private bool castedSpell;
    private GameObject groundRune;
    private List<GameObject> targets;

    // Use this for initialization
    void Start () {
        targets = new List<GameObject>();
        groundRune = this.transform.FindChild("GroundEffect").gameObject;
        groundRune.transform.localScale = groundRune.transform.localScale * radius * 2.5f;
        FindTargetNearby(this.transform.position, radius);
        if (targets.Count == 0)
            Destroy(this.gameObject, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
	
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

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
