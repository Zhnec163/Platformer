using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class CharacterMover : MonoBehaviour
{
    [field: SerializeField] protected float MoveSpeed = 4F;
    
    protected SpriteRenderer _spriteRenderer;
    protected WaitForSeconds _delayOfAttacking;
    
    protected void Init()
    {
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
            _spriteRenderer = spriteRenderer;
    }
}