using System;
using System.Collections;
using UnityEngine;

public class Vampirism : Skill
{
    [SerializeField] private Health _health;
    [SerializeField] private float _timeDelayDammage;
    [SerializeField] protected float _timeUse;
    [SerializeField] protected float _timeCooldown;

    private Coroutine _coroutine = null;
    
    public override bool TryUse()
    {
        bool isUsing = base.TryUse();

        if (isUsing)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(Using());
        }

        return isUsing;
    }

    private Collider2D GetClosestCollider(Collider2D[] colliders)
    {
        float maxDistance = Single.MaxValue;
        int indexСlosestCollider = 0;

        if (colliders.Length == 0)
            return null;

        for (int i = 0; i < colliders.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, colliders[i].transform.position);

            if (maxDistance > distance)
            {
                maxDistance = distance;
                indexСlosestCollider = i;
            }
        }

        return colliders[indexСlosestCollider];
    }

    private IEnumerator Using()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeDelayDammage);

        while (CanUseSkill)
        {
            Collider2D collider = GetClosestCollider(Physics2D.OverlapCircleAll(transform.position, DistanceUsing, LayerMaskAttacked));
            
            if (collider != null && collider.TryGetComponent(out Character character))
            {
                character.TakeDamage(Damage);
                _health.Add(Damage);
            }

            yield return wait;
        }
    }
}