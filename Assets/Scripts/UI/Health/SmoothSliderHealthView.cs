using System.Collections;
using UnityEngine;

public class SmoothSliderHealthView : SliderHealthView
{
    [SerializeField] private float _speed;
    
    private Coroutine _healthChangingCoroutine;
    
    protected override void Change()
    {
        if (_healthChangingCoroutine != null)
            StopCoroutine(_healthChangingCoroutine);
        
        _healthChangingCoroutine = StartCoroutine(HealthChanging());
    }

    private IEnumerator HealthChanging()
    {
        while (Slider.value != Health.Current)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, Health.Current, Time.deltaTime * _speed);
            yield return null;
        }
    }
}
