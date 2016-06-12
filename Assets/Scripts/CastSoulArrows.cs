using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastSoulArrows : MonoBehaviour {

    //Objects
    public GameObject projectilePrefab;
    public SpellFXManager spellFXManager;

    //Variable Control
    public int nProjectiles = 1;
    public float speed = 1.0f;
    public float pathAngling = 2.0f;
    public float radius = 3.0f;
    public float trackingValue = 0.8f;

    //private variables
    private Transform target;
    private List<GameObject> livingProjectiles;
    private bool castedSpell;
    private float progressionTime;
    private float currSpeed;
    private Vector3 startPos;
    private Vector3 originalTargetPos;
    private Vector3 currTargetPos;
    private GameObject groundRune;

    void Start() {
        spellFXManager.radius = radius;
        livingProjectiles = new List<GameObject>();
        progressionTime = 0;
        castedSpell = false;
        FindTargetNearby(transform.position, radius);
        if (target == null)
            Destroy(this.gameObject, 0.5f);
        startPos = transform.position;
        castedSpell = true;
    }

    void Update()
    {
        if (target != null)
        {
            if (castedSpell && spellFXManager.ready)
            {
                TrackingCheck();
                ManageProjectiles();
            }
        }
    }

    public void ManageProjectiles()
    {
        if (progressionTime >= 1.0f)
        {
            //Debug.Log(Vector3.Distance(livingProjectiles[0].transform.position, target.position));
            if (Vector3.Distance(livingProjectiles[0].transform.position , target.position) <= 0.5f)
                spellFXManager.InstantiateCollisionParticles(target.position);
            castedSpell = false;
            KillProjectile();
            progressionTime = 0.0f;
            currSpeed = speed;
            Destroy(this.gameObject);
        }
        else
        if (progressionTime <= 0.0f)
        {
            GetProjectileReady();
            currSpeed = speed;
            progressionTime += 0.001f * currSpeed;
        }
        else
        {
            currSpeed = speed * Mathf.Pow(5, progressionTime);
            progressionTime += 0.001f * currSpeed;

            NewProjectilePosition();
        }
    }

    private void NewProjectilePosition()
    {
        
        for (int i = 0; i < livingProjectiles.Count; i++)
        {
            Vector3 BezierPointVector = RotateVector(this.transform.right * pathAngling, (180 / (livingProjectiles.Count-1)) * i);

            Debug.DrawRay(startPos, BezierPointVector, Color.yellow);

            Vector3 newPosition = Mathf.Pow((1 - progressionTime), 2) * startPos + 2 * (1 - progressionTime) * progressionTime *
            (startPos + BezierPointVector) +
                Mathf.Pow(progressionTime, 2) * currTargetPos;

            Vector3 projectileDirection = newPosition - livingProjectiles[i].transform.position;

            Debug.DrawLine(startPos, currTargetPos);
            Debug.DrawRay(livingProjectiles[i].transform.position, projectileDirection*5 ,Color.red);

            livingProjectiles[i].transform.rotation = Quaternion.LookRotation(projectileDirection);
            livingProjectiles[i].transform.position = newPosition;
        }
    }

    private void GetProjectileReady() {
        if (livingProjectiles.Count == 0) {
            for (int i = 0; i < nProjectiles; i++) {
                livingProjectiles.Add((GameObject)Instantiate(projectilePrefab, this.transform.position, Quaternion.identity));
            }
        }
        else
        {
            for (int i = 0; i < livingProjectiles.Count; i++)
            {
                livingProjectiles[i].SetActive(true);
                livingProjectiles[i].transform.position = startPos;
            }
        }
    }

    private void KillProjectile()
    {
        for (int i = 0; i < livingProjectiles.Count; i++)
        {
            //livingProjectiles[i].SetActive(false);
            Destroy(livingProjectiles[i]);
        }
    }

    private void TrackingCheck()
    {
        if(Vector3.Distance(target.position, originalTargetPos) < trackingValue)
        {
            currTargetPos = target.position;
        }
    }

    private void FindTargetNearby(Vector3 originPos, float radius)
    {
        RaycastHit[] sphereHit = Physics.SphereCastAll(originPos, radius, transform.up, 10);

        float closestDist = radius * 2;
  
        for (int i = 0; i < sphereHit.Length; i++)
        {
            Debug.Log(sphereHit[i].collider.transform.tag);
            if (sphereHit[i].collider.transform.tag.Equals("Target"))
            {
                if(sphereHit[i].distance < closestDist)
                {
                    closestDist = sphereHit[i].distance;
                    target = sphereHit[i].collider.transform;
                    currTargetPos = target.position;
                    originalTargetPos = target.position;
                }
            }
        }

        Vector3 dirToTarget = currTargetPos - this.transform.position;
        dirToTarget = new Vector3(dirToTarget.x, 0, dirToTarget.z);
        this.transform.rotation = Quaternion.FromToRotation(this.transform.forward, dirToTarget);

        
        
        //this.transform.LookAt(target);

    }

    private Vector3 RotateVector(Vector3 vector, float angle)
    {
        Vector3 rotatedVector = Quaternion.AngleAxis(angle, this.transform.forward) * vector;
        return rotatedVector;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(startPos, radius);
    }
}
