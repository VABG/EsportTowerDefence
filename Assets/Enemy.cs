using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health;
    NavMeshAgent agent;
    static NavMeshPath path;
    float defaultSpeed;

    List<IEffect> effects = new List<IEffect>();    

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = agent.speed;  
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

    public void SlowEffectStart(float multiplier)
    {
        agent.speed = defaultSpeed * multiplier;
    }

    public void SlowEffectStop()
    {
        agent.speed = defaultSpeed;
    }

    public void RemoveEffect(IEffect effect)
    {
        effects.Remove(effect);
    }

    public bool AddEffect(IEffect effect)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            string t = effects[i].GetType();
            if (effect.GetType() == t) return false;
        }

        //if (effects.Contains(effect)) return;

        effects.Add(effect);
        return true;
    }
}
