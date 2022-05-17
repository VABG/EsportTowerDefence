using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float health = 100;
    List<GameObject> targets;

    [SerializeField] GameObject shot;
    [SerializeField] Transform shootFromHere;
    [SerializeField] ParticleSystem pfx;

    float shotDelay = .2f;
    float shotTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = targets.Count-1; i >= 0; i--)
        {
            if (targets[i] == null) targets.RemoveAt(i);
        }

        if (shotTimer > 0) shotTimer -= Time.deltaTime;

        if (targets.Count > 0)
        {
            shootFromHere.LookAt(targets[0].transform);

            if (shotTimer <= 0)
            {
                Shoot();
                shotTimer = shotDelay;
            }
        }
    }

    void Shoot()
    {
        shootFromHere.LookAt(targets[0].transform);
        Instantiate(shot, shootFromHere.position, shootFromHere.rotation);
        pfx.Play();
    }

    private void OnTriggerEnter(Collider other)
    {      
        if (!targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }
}
