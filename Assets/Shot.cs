using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject deathFX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy e = collision.gameObject.GetComponent<Enemy>();

        if (e != null)
        {
            e.Damage(4);
            GameObject eSpawn = Instantiate(effect, e.transform.position, e.transform.rotation,  e.transform);
            IEffect ifx = eSpawn.GetComponent<IEffect>();
            ifx.StartEffect(e);
            if (!e.AddEffect(ifx))
            {
                Destroy(eSpawn);
            }

        }
        Instantiate(deathFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
