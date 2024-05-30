using System;
using System.Collections;
using UnityEngine;

public class SmoothSliderHealthView : SliderHealthView
{
    [SerializeField] private float _speed;

    private Coroutine _healthChangingCoroutine;

    protected override void HandleValueChange()
    {
        if (_healthChangingCoroutine != null)
            StopCoroutine(_healthChangingCoroutine);

        _healthChangingCoroutine = StartCoroutine(HealthChanging());
    }

    private IEnumerator HealthChanging()
    {
        while (Slider.value != Health.Current / Health.MaxHealthPont)
        {
            Slider.value = Mathf.Lerp(Slider.value, Health.Current / Health.MaxHealthPont, Time.deltaTime * _speed);
            yield return null;
        }
    }
}