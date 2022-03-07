using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    NavMeshAgent agent;
    static NavMeshPath path;


    // Start is called before the first frame update
    void Start()
    {
       
        //agent.SetDestination(endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {

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

    public void SetEndPoint(Vector3 endPoint)
    {
        agent = GetComponent<NavMeshAgent>();        
        
        if (path == null)
        {
            path = new NavMeshPath();
            agent.CalculatePath(endPoint, path);
        }
        agent.path = path;
    }
}
