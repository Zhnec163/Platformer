using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SliderHealthView : HealthView
{
    protected Slider Slider;

    protected override void Start()
    {
        if (TryGetComponent(out Slider slider))
        {
            Slider = slider;
        }
        
        Slider.value = Health.Current / Health.MaxHealthPont;
        base.Start();
    }
    
    protected override void HandleValueChange()
    {
        if (Slider != null)
            Slider.value = Health.Current / Health.MaxHealthPont;
    }
}
