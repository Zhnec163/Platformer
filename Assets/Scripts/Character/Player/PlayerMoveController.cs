using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveController : CharacterMover
{
    [SerializeField] private float _jumpForce = 7F;
    [SerializeField] private float _attackRadius = 0.7F;

    private Rigidbody2D _rigidbody;
    private bool _playerOnGround;
    
    [HideInInspector] public bool IsIdle => Condition.IsAboutZero(_rigidbody.velocity.magnitude) && _playerOnGround;

    private void Awake()
    {
        base.Init();

        if (TryGetComponent(out Rigidbody2D rigidbody2D))
            _rigidbody = rigidbody2D;
    }
    
    private void FixedUpdate()
    {
        DetectGround();
    }

    public void Move(float direction)
    {
        transform.position = new Vector3(transform.position.x + direction * MoveSpeed * Time.deltaTime, transform.position.y);

        if (_spriteRenderer.flipX && Condition.IsLessZero(direction) ||
            _spriteRenderer.flipX == false && Condition.IsAboveZero(direction))
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    public void TryJump()
    {
        if (_playerOnGround)
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