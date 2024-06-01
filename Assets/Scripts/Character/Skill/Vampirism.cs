using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Health))]
public class Vampirism : Skill
{
    [SerializeField] private float _timeUsing;
    [SerializeField] private float _timeStep;

    private Health _health;
    private WaitForSeconds _delay;
    private WaitForSeconds _castTime;
    private bool _isCast;

    private void Awake()
    {
        if (TryGetComponent(out Health health))
            _health = health;
        
        _delay = new WaitForSeconds(_timeStep);
        _castTime = new WaitForSeconds(_timeUsing);
    }

    protected override void Cast()
    {
        if (_isCast == false)
            StartCoroutine(DrainLives());
    }

    private IEnumerator DrainLives()
    {
        if (TryGetNearCharacter(out Character character))
        {
            _isCast = true;
            StartCoroutine(StopCast());
            
            while (_isCast)
            {
                if (Vector3.Distance(transform.position, character.transform.position) < DistanceUsing)
                {
                    character.TakeDamage(Damage);
                    _health.Add(Damage);
                }

                yield return _delay;
            }
        }
    }
    
    private IEnumerator StopCast()
    {
        yield return _castTime;
        _isCast = false;
    }
}