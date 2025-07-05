using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Volorf.VolumeUI
{
    [RequireComponent(typeof(AudioSource))]
    public class UISound : MonoBehaviour
    {
        [SerializeField] List<AudioClip> _onPressSounds;
        [SerializeField] List<AudioClip> _onReleaseSounds;
        [SerializeField] float _volume = 1f;
        
        IInteractable _interactable;
        AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            
            _interactable = GetComponent<IInteractable>();
            if (_interactable == null)
            {
                Debug.LogError("UISound requires an IInteractable component.");
                return;
            }

            _interactable.OnPressed += PlayOnPressSound;
            _interactable.OnReleased += PlayOnReleaseSound;
        }
        
        void PlayOnPressSound()
        {
            StartCoroutine(PlaySound(
                () => _audioSource.PlayOneShot(GetRandomClip(_onPressSounds), _volume))
            );
        }

        void PlayOnReleaseSound()
        {
            StartCoroutine(PlaySound(
                () => _audioSource.PlayOneShot(GetRandomClip(_onReleaseSounds), _volume))
            );
        }
        
        static AudioClip GetRandomClip(List<AudioClip> clips)
        {
            if (clips == null || clips.Count == 0) return null;
            return clips[Random.Range(0, clips.Count)];
        }

        IEnumerator PlaySound(Action action)
        {
            while (_audioSource.isPlaying)
            {
                yield return null;
            }
            
            action?.Invoke();
        }
    }
}

