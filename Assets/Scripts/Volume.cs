using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Reference to the AudioMixer
    [SerializeField] private Slider musicSlider; // Reference to the UI Slider for volume control
    [SerializeField] private Slider SoundeffectSlider; // Reference to the UI Slider for sound effects volume control
    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadingVolume(); // Load the saved volume setting if it exists
        }
        else
        {
            SetVolume(); // Apply the default volume
            SetEffectVolume(); // Apply the default sound effects volume
        }
    }
    public void SetVolume()
    {
        float volumeDb = musicSlider.value; // Convert volume to decibels
        // Set the volume in the AudioMixer
        audioMixer.SetFloat("MyExposedParam", Mathf.Log10(volumeDb)*20);
        PlayerPrefs.SetFloat("MusicVolume", volumeDb); // Save the volume setting
    }
    public void SetEffectVolume()
    {
        float volume = SoundeffectSlider.value; // Convert volume to decibels
        // Set the volume in the AudioMixer
        audioMixer.SetFloat("MyExposedParam 1", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SoundVolume", volume); // Save the volume setting
    }
    public void LoadingVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume"); // Load the saved volume setting
        SoundeffectSlider.value = PlayerPrefs.GetFloat("SoundVolume"); // Load the saved sound effects volume setting
        SetVolume(); // Apply the loaded volume
        SetEffectVolume(); // Apply the loaded sound effects volume

    }
}
