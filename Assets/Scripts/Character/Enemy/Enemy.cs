using UnityEngine;

[RequireComponent(typeof(PatrolingEnemyAI), typeof(AnimationController), typeof(Health))]
public class Enemy : Character
{
    private void Awake()
    {
        Init();
    }
}
