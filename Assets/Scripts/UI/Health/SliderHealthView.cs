using UnityEngine;
using UnityEngine.UI;

public class SliderHealthView : HealthView
{
    [field: SerializeField] protected Slider Slider;

    protected override void Start()
    {
        Slider.minValue = 0;
        Slider.maxValue = Health.MaxHealthPont;
        Slider.value = Health.Current;
        base.Start();
    }
    
    protected override void Change()
    {
        if (Slider != null)
            Slider.value = Health.Current;
    }
}
