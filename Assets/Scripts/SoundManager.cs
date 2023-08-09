using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static bool isSoundEnabled = true;

    private void Start()
    {
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        UpdateSoundState();
    }

    public static void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;

        PlayerPrefs.SetInt("SoundEnabled", isSoundEnabled ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSoundState();
    }

    private static void UpdateSoundState()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.mute = !isSoundEnabled;
        }
    }
}
