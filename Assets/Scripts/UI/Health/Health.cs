using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHealthPont { get; private set; }
    [field: SerializeField] public float Current { get; private set; }

    public event Action ChangeAction;

    private void OnValidate()
    {
        if (Current > MaxHealthPont)
            Current = MaxHealthPont;
    }
    
    public void AddHealth(float value)
    {
        if (value <= 0) 
            return;
        
        Current = Mathf.Clamp(Current + value, 0, MaxHealthPont);
        ChangeAction?.Invoke();
    }
    
    public void TakeHealth(float value)
    {
        if (value <= 0) 
            return;
        
        Current = Mathf.Clamp(Current - value, 0, Current);
        ChangeAction?.Invoke();
    }
}
