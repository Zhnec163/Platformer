using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _audioSourceButton;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private Slider _sliderOfCommonSound;
    [SerializeField] private Slider _sliderOfButtonSound;
    [SerializeField] private Slider _sliderOfBackgroundSound;

    private int _logarithmFactor = 20;
    private int _minLogValue = -80;
    private float _savedValue;
    private bool _isEnableSound = true;

    private void Awake()
    {
        _audioMixer.SetFloat(Constant.ParameterMasterChannel, Mathf.Log10(_sliderOfCommonSound.value) * _logarithmFactor);
        _audioMixer.SetFloat(Constant.ParameterButtonChannel, Mathf.Log10(_sliderOfButtonSound.value) * _logarithmFactor);
        _audioMixer.SetFloat(Constant.ParameterBackgroundChannel, Mathf.Log10(_sliderOfBackgroundSound.value) * _logarithmFactor);
        _audioMixer.GetFloat(Constant.ParameterMasterChannel, out _savedValue);
    }

    private void OnEnable()
    {
        _sliderOfCommonSound.onValueChanged.AddListener(SetMasterParameter);
        _sliderOfButtonSound.onValueChanged.AddListener(SetButtonParameter);
        _sliderOfBackgroundSound.onValueChanged.AddListener(SetBackgroundParameter);
    }

    private void OnDestroy()
    {
        _sliderOfCommonSound.onValueChanged = null;
        _sliderOfButtonSound.onValueChanged = null;
        _sliderOfBackgroundSound.onValueChanged = null;
    }

    public void PlaySound(int clipNumber)
    {
        _audioSourceButton.clip = _audioClips[clipNumber];
        _audioSourceButton.Play();
    }

    public void SwitchMute()
    {
        if (_isEnableSound)
        {
            _isEnableSound = false;
            _audioMixer.GetFloat(Constant.ParameterMasterChannel, out _savedValue);
            _audioMixer.SetFloat(Constant.ParameterMasterChannel, _minLogValue);
        }
        else
        {
            _isEnableSound = true;
            _audioMixer.SetFloat(Constant.ParameterMasterChannel, _savedValue);
        }
    }

    private void SetMasterParameter(float volume)
    {
        if (volume > 0)
            _audioMixer.SetFloat(Constant.ParameterMasterChannel, Mathf.Log10(volume) * _logarithmFactor);
    }

    private void SetBackgroundParameter(float volume)
    {
        if (volume > 0)
            _audioMixer.SetFloat(Constant.ParameterBackgroundChannel, Mathf.Log10(volume) * _logarithmFactor);
    }

    private void SetButtonParameter(float volume)
    {
        if (volume > 0)
            _audioMixer.SetFloat(Constant.ParameterButtonChannel, Mathf.Log10(volume) * _logarithmFactor);
    }
}