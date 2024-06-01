using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action OnValueChanged;
    
    [field: SerializeField] public float MaxHealthPont { get; private set; }
    [field: SerializeField] public float Current { get; private set; }

    private void OnValidate()
    {
        if (Current > MaxHealthPont)
            Current = MaxHealthPont;
    }
    
    public void Add(float value)
    {
        if (value <= 0) 
            return;
        
        Current = Mathf.Clamp(Current + value, 0, MaxHealthPont);
        OnValueChanged?.Invoke();
    }
    
    public void Subtract(float value)
    {
        if (value <= 0) 
            return;
        
        Current = Mathf.Clamp(Current - value, 0, Current);
        OnValueChanged?.Invoke();
    }
        
    public void ResetToZero()
    {
        Current = 0;
        OnValueChanged?.Invoke();
    }
}
