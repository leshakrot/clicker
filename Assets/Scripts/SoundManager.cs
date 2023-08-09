using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject _soundToggleImage;
    private static bool isSoundEnabled = true;

    private void Start()
    {
        isSoundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        if (!isSoundEnabled) _soundToggleImage.SetActive(true);
        else _soundToggleImage.SetActive(false);
        UpdateSoundState();
    }

    public void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        _soundToggleImage.SetActive(!isSoundEnabled);

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
