using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Character : MonoBehaviour
{
    protected Health Health;

    protected void Init()
    {
        if (TryGetComponent(out Health health))
            Health = health;
    }

    public void Attack(Character character, Skill skill) => character.TakeDamage(skill.Damage); 

    public void TakeDamage(float damage)
    {
        float newHealthPoint = Mathf.Clamp(Health.Current - damage, 0, Health.MaxHealthPont);

        if (newHealthPoint > 0)
        {
            Health.Subtract(damage);
        }
        else
        {
            Health.ResetToZero();
            gameObject.SetActive(false);
        }
    }
}