using UnityEngine;

namespace Volorf.VolumeUI
{
    [RequireComponent(typeof(AudioSource))]
    public class UISound : MonoBehaviour
    {
        [SerializeField] AudioClip _onPressSound;
        [SerializeField] AudioClip _onReleaseSound;
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

        void PlayOnReleaseSound()
        {
            if (_audioSource.isPlaying) return;
            _audioSource.PlayOneShot(_onReleaseSound, _volume);
        }

        void PlayOnPressSound()
        {
            if (_audioSource.isPlaying) return;
            _audioSource.PlayOneShot(_onPressSound, _volume);
        }
    }
}

