using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private AudioClip[] _audioClips;

    public void SwitchSoundState()
    {
        for(int i = 0; i < _audioSources.Length; i++)
        {
            if (_audioSources[i].volume == 0)
            {
                _audioSources[i].volume = 1;
            }
            else
            {
                _audioSources[i].volume = 0;
            }
        }
    }

    public void TapSound(int index)
    {
        _audioSources[0].clip = _audioClips[index];
        _audioSources[0].Play();
    }

    public void EffectSound(int index)
    {
        _audioSources[1].clip = _audioClips[index];
        _audioSources[1].Play();
    }


}
