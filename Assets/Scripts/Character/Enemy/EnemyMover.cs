using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : CharacterMover
{
    [SerializeField] private float _patrolRadius = 3F;
    [SerializeField] private float _attackDistance = 1.5F;

    private bool _isMovingLeft;
    private float _leftPositionX;
    private float _rightPositionX;
    private Action<Player> _onPlayerAttack;

    private void FixedUpdate()
    {
        GuardTerritory();
    }

    private void OnDestroy()
    {
        _onPlayerAttack = null;
    }

    public void Init(Action<Player> onPlayerAttack)
    {
        base.Init();
        _onPlayerAttack = onPlayerAttack;
        _leftPositionX = transform.position.x - _patrolRadius;
        _rightPositionX = transform.position.x + _patrolRadius;
        _isMovingLeft = _spriteRenderer.flipX;
    }

    private void GuardTerritory()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _isMovingLeft ? Vector2.left : Vector2.right, _spriteRenderer.bounds.extents.x + _attackDistance);

        if (hit)
        {
            if (hit.transform.TryGetComponent(out Player player))
            {
                if (_canAttacking && Vector3.Distance(player.transform.position, transform.position) < _attackDistance)
                    Attack(player);
                else
                    Chase(player);
                
                return;
            }
        }

        Patrol();
    }

    private void Chase(Player player)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
        _animationController.PlayingMovingAnimation();
    }

    private void Attack(Player player)
    {
        _canAttacking = false;
        _animationController.PlayingAttackingAnimation();
        _onPlayerAttack.Invoke(player);
        StartCoroutine(ResetTimeAttack());
    }

    private void Patrol()
    {
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