using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoverController : CharacterMover
{
    [SerializeField] private float _jumpForce = 7F;
    [SerializeField] private float _attackRadius = 0.7F;
    [SerializeField] private Transform _attackPoint;

    private Rigidbody2D _rigidbody;
    private bool _playerOnGround;
    private Action<List<Enemy>> _onAttackOfEnemies;

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        DetectGround();
    }

    private void OnDestroy()
    {
        _onAttackOfEnemies = null;
    }

    public void Init(Action<List<Enemy>> onAttackOfEnemies)
    {
        base.Init();

        if (TryGetComponent(out Rigidbody2D rigidbody2D))
            _rigidbody = rigidbody2D;

        _onAttackOfEnemies = onAttackOfEnemies;
    }

    private void HandleInput()
    {
        float horizontalInput = Input.GetAxis(Constant.HorizontalAxis);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerOnGround)
                Jump();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (_canAttacking)
                Attack();
        }
        else if (Condition.IsNotAboutZero(horizontalInput))
        {
            Move(horizontalInput);
        }
        else
        {
            _animationController.PlayingIdleAnimation();
        }
    }
    
    private void Attack()
    {
        _canAttacking = false;
        _animationController.PlayingAttackingAnimation();

        List<Collider2D> colliders = Physics2D.OverlapCircleAll(_attackPoint.transform.position, _attackRadius).ToList();

        if (Condition.IsAboveZero(colliders.Count))
        {
            List<Enemy> enemies = new List<Enemy>();

            colliders.ForEach(collider =>
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemies.Add(enemy);
                }
            });

            if (Condition.IsAboveZero(enemies.Count))
                _onAttackOfEnemies.Invoke(enemies);
        }

        StartCoroutine(ResetTimeAttack());
    }

    private void Move(float direction)
    {
        _animationController.PlayingMovingAnimation(); 
        transform.position = new Vector3(transform.position.x + direction * MoveSpeed * Time.deltaTime, transform.position.y);

        if (_spriteRenderer.flipX && Condition.IsLessZero(direction) || _spriteRenderer.flipX == false && Condition.IsAboveZero(direction))
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            _attackPoint.transform.position = new Vector3(transform.position.x + Vector3.Distance(transform.position, _attackPoint.transform.position) * (_spriteRenderer.flipX ? 1 : -1), transform.position.y);
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void DetectGround()
    {
        float offsetGroundRay = 0.1F;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _spriteRenderer.bounds.extents.y + offsetGroundRay);

        if (hit)
        {
            if (hit.transform.TryGetComponent(out Ground ground) && _playerOnGround == false)
                _playerOnGround = true;
        }
        else
        {
            if (_playerOnGround)
                _playerOnGround = false;
        }
    }
}