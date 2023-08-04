using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class Customer : MonoBehaviour
{
    private int currentEarnings = 5;

    private void Start()
    {
        currentEarnings = PlayerPrefs.GetInt("Earnings", 5);
    }

    private void Update()
    {
        EarnCurrency();
    }

    public void BuyCharacter(Character character)
    {
        if(character.GetPrice() <= Bank.instance.GetAmountCurrency())
        {
            Bank.instance.SpendCurrency(character.GetPrice());
            BankUI.instance.UpdateCurrencyUI();
            character.isPurchased = true;
            string key = character.name;
            PlayerPrefs.SetInt(key, 1);
            CharacterStorage.instance.UpdateCharacters();
            currentEarnings += 5;
            PlayerPrefs.SetInt("Earnings", currentEarnings);
            YandexGame.Instance._FullscreenShow();
        }
    }

    public void EarnCurrency()
    {
        bool hasTouchInput = Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();
        bool hasKeyboardInput = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);

        if (hasTouchInput || hasKeyboardInput)
        {
            Bank.instance.AddCurrency(currentEarnings);
            BankUI.instance.UpdateCurrencyUI();
        }
    }
}
