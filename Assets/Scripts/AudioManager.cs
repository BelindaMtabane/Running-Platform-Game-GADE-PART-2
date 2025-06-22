using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource; // Reference to the AudioSource component
    [SerializeField] AudioSource SoundEffectSource;
    public AudioClip background; // Reference to the background music
    public AudioClip timeOrb;
    public AudioClip health;
    public AudioClip point;
    public AudioClip coal;
    public AudioClip death;
    public AudioClip portal;
    public AudioClip shooting;
    public AudioClip buttonMenu;
    public AudioClip enemy;

    public void Start()
    {
        musicSource.clip = background; // Set the background music clip
        musicSource.Play(); // Play the background music
    }
    public void PlaySoundEffects(AudioClip clip)
    {
        SoundEffectSource.PlayOneShot(clip); // Play the sound effect once
    }
}
