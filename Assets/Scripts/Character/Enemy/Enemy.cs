using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(AnimationController), typeof(Health))]
public class Enemy : Character
{
    private void Awake()
    {
        Init();
        EnemyMover enemyMover = GetComponent<EnemyMover>();
        enemyMover.Init(HandleAttacking);
    }
    
    private void HandleAttacking(Player player)
    {
        Attack(player);
    }
}
