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
    [SerializeField] Animator anim;
    [SerializeField] SkinnedMeshRenderer skin;
    [SerializeField] MeshRenderer[] mesh;
    [SerializeField] GameObject deathPFX;
    [SerializeField] Transform deathPFXSpawnPosition;
    Material[] m;
    int fadeID;
    float fadeTimer = 0;
    List<IEffect> effects = new List<IEffect>();
    int speedID;
    bool dying = false;

    [SerializeField] Transform headBone;
    [SerializeField] Transform jaw;
    [SerializeField] Transform[] tail;
    [SerializeField] Transform upperBody;

    void RandomizeBoneSizes()
    {
        float headScale = Random.value + .5f;
        float tailLength = Random.value + .5f;
        float tailThickness = Random.value + .5f;
        float jawLength = Random.value + .5f;
        float upperbodyScale = Random.value + .5f;

        upperBody.localScale = new Vector3(upperbodyScale, upperbodyScale, upperbodyScale);
        headBone.localScale = new Vector3(headScale, headScale, headScale);
        jaw.localScale = new Vector3(1, jawLength, 1);

        for (int i = 0; i < tail.Length; i++)
        {
            tail[i].localScale = new Vector3(tailThickness, tailLength, tailThickness);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        RandomizeBoneSizes();
        speedID = Animator.StringToHash("Speed");
        agent = GetComponent<NavMeshAgent>();
        defaultSpeed = agent.speed;
        m = new Material[mesh.Length+1];
        m[0] = skin.materials[0];
        for (int i = 0; i < mesh.Length; i++)
        {
            m[i+1] = mesh[i].materials[0];
        }

        fadeID = Shader.PropertyToID("Vector1_924b215b91f745428a2ccfd9c7708541");
        //agent.SetDestination(endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = agent.velocity.magnitude;
        anim.SetFloat(speedID, speed);
        if (dying)
        {
            fadeTimer += Time.deltaTime;            
            for (int i = 0; i < m.Length; i++)
            {
                m[i].SetFloat(fadeID, fadeTimer);
            }            
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    void Die()
    {
        //agent.isStopped = true;
        dying = true;
        anim.SetTrigger("OnDeath");
        Destroy(gameObject, 1.0f);
        GetComponent<CapsuleCollider>().enabled = false;
        agent.enabled = false;
        Instantiate(deathPFX, deathPFXSpawnPosition.position, Quaternion.identity);
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
