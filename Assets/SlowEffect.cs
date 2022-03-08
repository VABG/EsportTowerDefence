using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    abstract void StartEffect(Enemy e);
    abstract void EffectEnd();
    abstract void OnDeath();
    abstract string GetType();
}


public class SlowEffect : MonoBehaviour, IEffect
{
    [SerializeField] float slowTime = 1.0f;
    float timer = 0;
    Enemy target;

    public void EffectEnd()
    {
        target.SlowEffectStop();
        target.RemoveEffect(this);
        Destroy(gameObject);
    }

    public void OnDeath()
    {

    }

    public void StartEffect(Enemy e)
    {
        target = e;
        target.SlowEffectStart(.5f);
    }

    string IEffect.GetType()
    {
        return "slow";
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= slowTime)
        {
            EffectEnd();
        }
    }
}
