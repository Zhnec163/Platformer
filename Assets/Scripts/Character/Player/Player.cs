using System;
using System.Collections.Generic;
using Platformer;
using UnityEngine;

[RequireComponent(typeof(PlayerMoverController), typeof(AnimationController), typeof(Health))]
public class Player : Character
{
    private int _coinCount;

    private void Awake()
    {
        Init();
        PlayerMoverController playerMoverController = GetComponent<PlayerMoverController>();
        playerMoverController.Init(HandleAttacking);
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

    private void HandleAttacking(List<Enemy> enemies)
    {
        enemies.ForEach(enemy => Attack(enemy));
    }
}