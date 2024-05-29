using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _mixerParameterName;

    private Slider _slider;
    private int _logarithmFactor = 20;
    private int _minLogValue = -80;
    private float _savedValue;

    public void Awake()
    {
        if (TryGetComponent(out Slider slider))
        {
            _slider = slider;
            _slider.onValueChanged.AddListener(Change);
        }
        
        _mixer.SetFloat(_mixerParameterName, Mathf.Log10(_slider.value) * _logarithmFactor);
        _mixer.GetFloat(_mixerParameterName, out _savedValue);
    }

    public void Mute()
    {
        _mixer.GetFloat(_mixerParameterName, out _savedValue);
        _mixer.SetFloat(_mixerParameterName, _minLogValue);
    }
    
    public void Unmute()
    {
        _mixer.SetFloat(_mixerParameterName, _savedValue);
    }

    private void Change(float value)
    {
        _mixer.SetFloat(_mixerParameterName, Mathf.Log10(_slider.value) * _logarithmFactor);
    }
}