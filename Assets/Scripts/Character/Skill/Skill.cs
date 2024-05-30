using System.Collections;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private float _timeDelayUsing;
    [field: SerializeField, Range(0, 300f)] public float Damage {get; private set;}
    [field: SerializeField] public LayerMask LayerMaskAttacked {get; private set;}
    [field: SerializeField] public float DistanceUsing { get; private set; }
    
    protected bool CanUseSkill = true;
    
    private Coroutine _delayAttack;

    private void Awake()
    {
        _delayAttack = null;
    }

    private void OnValidate()
    {
        if (DistanceUsing < 0f)
            DistanceUsing = 0f;
    }
    
    public bool IsDistanceReached(Character target)
    {
        float distanceTarget = Vector2.Distance(transform.position, target.transform.position);

        return distanceTarget - DistanceUsing < 0;
    }
    
    public virtual bool TryUse()
    {
        if (_delayAttack == null)
        {
            DelayUse();
            return true;
        }
        
        return false;
    }
    
    private void DelayUse()
    {
        if (_delayAttack == null)
            _delayAttack = StartCoroutine(Delay());
    }
    
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_timeDelayUsing);
        _delayAttack = null;
    }
}