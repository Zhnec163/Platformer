using System;
using System.Collections.Generic;
using Platformer;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMoveController), typeof(AnimationController))]
public class Player : Character
{
    private int _coinCount;
    private PlayerInput _input;
    private PlayerMoveController _playerMoveController;
    private AnimationController _animationController;
    private Attack _attack;
    private Vampirism _vampirism;

    private void Awake()
    {
        Init();

        if (TryGetComponent(out PlayerInput input))
            _input = input;
        
        if (TryGetComponent(out AnimationController animationController))
            _animationController = animationController;

        if (TryGetComponent(out Attack attack))
            _attack = attack;

        if (TryGetComponent(out Vampirism vampirism))
            _vampirism = vampirism;

        _input.Init(HandleMove, HandleJump, HandleAttack, HandleVampirism);
        _playerMoveController = GetComponent<PlayerMoveController>();
    }

    private void FixedUpdate()
    {
        if (_playerMoveController.IsIdle())
            _animationController.PlayingIdleAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Loot loot))
        {
            if (loot is Coin)
                _coinCount++;
            else if (loot is MedicineChest medicineChest)
                Health.Add(medicineChest.HealingPoints);

            loot.Tacked();
        }
    }

    private void HandleMove(float direction)
    {
        _animationController.PlayingMovingAnimation();
        _playerMoveController.Move(direction);
    }

    private void HandleJump() => _playerMoveController.TryJump();

    private void HandleAttack()
    {
        _animationController.PlayingAttackingAnimation();
        _attack.TryUse();
    }

    private void HandleVampirism()
    {
        _animationController.PlayingAttackingAnimation();
        _vampirism.TryUse();
    }
}