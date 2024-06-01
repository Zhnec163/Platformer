﻿using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Skill : MonoBehaviour
{
    [field: SerializeField] private float _timeCooldown;
    [field: SerializeField] private LayerMask LayerMaskAttacked;

    private bool _canUsingSkill = true;

    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float DistanceUsing { get; private set; }

    private void OnValidate()
    {
        if (DistanceUsing < 0f)
            DistanceUsing = 0f;
    }

    public virtual bool TryUse()
    {
        if (_canUsingSkill)
        {
            _canUsingSkill = false;
            Cast();
            StartCoroutine(ResetAttack());
            return true;
        }

        return false;
    }
    
    protected abstract void Cast();

    protected bool TryGetNearCharacter(out Character character)
    {
        character = null;
        float diameter = DistanceUsing * 2;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, diameter, LayerMaskAttacked);

        if (colliders.Length == 0)
            return false;

        float bestDistance = Single.MaxValue;
            
        for (int i = 0; i < colliders.Length; i++)
        {
            float currentDistance = Vector2.Distance(transform.position, colliders[i].transform.position);
            
            if (currentDistance < bestDistance)
            {
                if (colliders[i].TryGetComponent(out Character currentCharacter))
                {
                    character = currentCharacter;
                    bestDistance = currentDistance;
                }
            }
        }

        if (character == null)
            return false;
        else
            return true;
    }
    
    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(_timeCooldown);
        _canUsingSkill = true;
    }
}