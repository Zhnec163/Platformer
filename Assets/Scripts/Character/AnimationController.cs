using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);
    private const string IsAttacking = nameof(IsAttacking);
    
    private Animator _animator;
    
    private void Awake()
    {
        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }
    }
    
    public void PlayingIdleAnimation()
    {
        _animator.SetBool(IsMoving, false);
    }
    
    public void PlayingMovingAnimation()
    {
        _animator.SetBool(IsMoving, true);
    }

    public void PlayingAttackingAnimation()
    {
        _animator.SetTrigger(IsAttacking);
    }
}