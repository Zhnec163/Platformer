using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] protected int Damage;
    
    protected Health Health;

    private WaitForSeconds _delayToDie = new (0.3F);

    protected void Init()
    {
        if (TryGetComponent(out Health health))
            Health = health;
    }

    public void Attack(Character character)
    {
        character.TakeDamage(Damage);
    }

    public void TakeDamage(float damage)
    {
        if (Mathf.Clamp(Health.Current - damage, 0, Health.MaxHealthPont) == 0)
        {
            ToDie();
        }
        else
        {
            Health.Subtract(damage);
        }
    }
    
    private void ToDie()
    {
        Health.Subtract(Health.Current);
        StartCoroutine(DeactivateDelay());
    }

    private IEnumerator DeactivateDelay()
    {
        yield return _delayToDie;
        gameObject.SetActive(false);
    }
}