using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class CharacterMover : MonoBehaviour
{
    [field: SerializeField] protected float MoveSpeed = 4F;
    [field: SerializeField] protected float TimeAttacking = 0.7F;
    
    protected bool _canAttacking;
    protected SpriteRenderer _spriteRenderer;
    protected AnimationController _animationController;
    protected WaitForSeconds _delayOfAttacking;
    
    protected void Init()
    {
        _canAttacking = true;
        _delayOfAttacking = new WaitForSeconds(TimeAttacking);
        
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            _spriteRenderer = spriteRenderer;
        }

        if (TryGetComponent(out AnimationController animationController))
        {
            _animationController = animationController;
        }
    }
    
    protected IEnumerator ResetTimeAttack()
    {
        yield return _delayOfAttacking;
        _canAttacking = true;
    }
}