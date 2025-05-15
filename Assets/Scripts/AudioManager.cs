using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource; // Reference to the AudioSource component

    public AudioClip background; // Reference to the background music

    void Start()
    {
        musicSource.clip = background; // Set the background music clip
        musicSource.Play(); // Play the background music
    }
}
