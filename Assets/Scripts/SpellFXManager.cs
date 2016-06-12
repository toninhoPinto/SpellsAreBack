using UnityEngine;
using System.Collections;

public class SpellFXManager : MonoBehaviour {

    public Animator runeAnimation;
    public GameObject runeScaler;
    public ParticleSystem particleSystem;
    public ParticleSystem collisionParticles;
    public bool ready = false;
    public float particleTimeSpellStart = 0.1f;
    public float radius;

	// Use this for initialization
	void Start () {
        runeScaler.transform.localScale *= radius;
	}
	
    public void InstantiateCollisionParticles(Vector3 position)
    {
        Instantiate(collisionParticles, position, Quaternion.identity);
    }

	// Update is called once per frame
	void Update () {
        ready = particleSystem.time > particleTimeSpellStart &&
            runeAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= particleTimeSpellStart;
    }
}
