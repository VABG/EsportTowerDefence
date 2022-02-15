using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;

    float moveTime = 5;

    float time = 0;
    bool right = true;

    Vector3 startposition;
    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (right) time += Time.deltaTime;
        else time -= Time.deltaTime;

        if (time >= moveTime) right = false;
        if (time <= 0) right = true;
        float lerp = time / moveTime;

        transform.position = Vector3.Lerp(startposition, startposition + Vector3.right * 30, lerp);
    }

    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Destroy(gameObject);
        }
    }
}
