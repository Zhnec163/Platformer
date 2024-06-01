using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private const string IsMoved = nameof(IsMoved);
    private const string IsAttacked = nameof(IsAttacked);
    
    private Animator _animator;
    
    private void Awake()
    {
        if (TryGetComponent(out Animator animator))
            _animator = animator;
    }
    
    public void PlayingIdleAnimation() => _animator.SetBool(IsMoved, false);
    
    public void PlayingMovingAnimation() => _animator.SetBool(IsMoved, true);

    public void PlayingAttackingAnimation() => _animator.SetTrigger(IsAttacked);
}