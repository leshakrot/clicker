using UnityEngine;

public class CharacterStorage : MonoBehaviour
{
    public static CharacterStorage instance;

    [SerializeField] private Character[] _characters;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateCharacters();
    }

    public void UpdateCharacters()
    {
        foreach(Character character in _characters)
        {
            character.gameObject.SetActive(true);
            if (PlayerPrefs.GetInt(gameObject.name) == 1) character.isPurchased = true;
            else character.gameObject.SetActive(false);
        }
    }
}
