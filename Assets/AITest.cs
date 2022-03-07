using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{
    [SerializeField] Transform endPoint;
    NavMeshAgent agent;
    static NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (path == null)
        {
            path = new NavMeshPath();
            agent.CalculatePath(endPoint.position, path);
        }

        agent.SetPath(path);
        //agent.SetDestination(endPoint.position);
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
