using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PatrolingEnemyAI : CharacterMover
{
    [SerializeField] private float _patrolRadius = 3F;
    [SerializeField] private float _attackDistance = 1F;

    private AnimationController _animationController;
    private bool _isMovingLeft;
    private float _leftPositionX;
    private float _rightPositionX;
    private Attack _attack;

    private void Awake()
    {
        base.Init();

        if (TryGetComponent(out Attack attack))
            _attack = attack;
        
        if (TryGetComponent(out AnimationController animationController))
            _animationController = animationController;
        
        _leftPositionX = transform.position.x - _patrolRadius;
        _rightPositionX = transform.position.x + _patrolRadius;
        _isMovingLeft = _spriteRenderer.flipX;
    }
    
    private void FixedUpdate()
    {
        GuardTerritory();
    }

    private void GuardTerritory()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _isMovingLeft ? Vector2.left : Vector2.right, _spriteRenderer.bounds.extents.x + _attackDistance);

        if (hit)
        {
            if (hit.transform.TryGetComponent(out Player player))
            {
                if (Vector3.Distance(player.transform.position, transform.position) < _attackDistance)
                {
                    Attack();
                }
                else
                {
                    Chase(player);
                }

                return;
            }
        }

        Patrol();
    }

    private void Attack()
    {
        if (_attack.TryUse())
            _animationController.PlayingAttackingAnimation();
    }

    private void Chase(Player player)
    {
        _animationController.PlayingMovingAnimation();
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
    }

    private void Patrol()
    {
        _animationController.PlayingMovingAnimation();
        
        if (transform.position.x < _leftPositionX)
        {
            _isMovingLeft = false;
            _spriteRenderer.flipX = false;
        }
        else if (transform.position.x > _rightPositionX)
        {
            _isMovingLeft = true;
            _spriteRenderer.flipX = true;
        }

        transform.position = new Vector3(transform.position.x + (_isMovingLeft ? -1 : 1) * MoveSpeed * Time.deltaTime, transform.position.y);
    }
}