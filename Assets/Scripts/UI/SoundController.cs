using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _audioSourceButton;
    [SerializeField] private AudioClip[] _audioClips;
    [SerializeField] private MixerChannelChanger _masterChannelChanger;

    private bool _isEnableSound = true;

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
            _masterChannelChanger.Mute();
        }
        else
        {
            _isEnableSound = true;
            _masterChannelChanger.Unmute();
        }
    }
}