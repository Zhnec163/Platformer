using System;
using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
    [field: SerializeField] protected Health Health;

    private float _localScaleX;

    protected virtual void Start()
    {
        _localScaleX = Health.transform.localScale.x;
        Change();
    }
    
    private void LateUpdate()
    {
        if (_localScaleX != Health.transform.localScale.x)
        {
            Vector3 localScale = new Vector3(Health.transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = localScale;
            _localScaleX = transform.localScale.x;
        }
    }
    
    private void OnEnable()
    {
        Health.ChangeAction += Change;
    }

    private void OnDisable()
    {
        Health.ChangeAction -= Change;
    }

    protected abstract void Change();
}
