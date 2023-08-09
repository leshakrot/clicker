using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class Customer : MonoBehaviour
{
    private int upgradeCost = 2500;
    private int rewardedCurrencyAmount = 1000;
    private int currentEarnings = 5;
    private bool isAutoEarningActive = false;
    private float autoEarnInterval = 1f;
    private float autoEarnTimer = 0f;

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private void Start()
    {
        currentEarnings = PlayerPrefs.GetInt("Earnings", 5);
        isAutoEarningActive = PlayerPrefs.GetInt("AutoEarningActive", 0) == 1;

        if (isAutoEarningActive)
        {
            InvokeRepeating("AutoEarnCurrency", autoEarnInterval, autoEarnInterval);
        }
    }

    private void Update()
    {
        EarnCurrency();

        if (isAutoEarningActive)
        {
            autoEarnTimer += Time.deltaTime;

            if (autoEarnTimer >= autoEarnInterval)
            {
                autoEarnTimer = 0f;
                AutoEarnCurrency();
            }
        }
    }

    public void BuyCharacter(Character character)
    {
        if (character.GetPrice() <= Bank.instance.GetAmountCurrency())
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

    public void BuyUpgrade()
    {
        if (upgradeCost <= Bank.instance.GetAmountCurrency())
        {
            Bank.instance.SpendCurrency(upgradeCost);
            BankUI.instance.UpdateCurrencyUI();
            ToggleAutoEarning();
            YandexGame.Instance._FullscreenShow();
        }
    }

    public void GetCurrencyForReward()
    {
        Bank.instance.AddCurrency(currentEarnings);
        BankUI.instance.UpdateCurrencyUI();
    }

    public void ToggleAutoEarning()
    {
        isAutoEarningActive = !isAutoEarningActive;

        PlayerPrefs.SetInt("AutoEarningActive", isAutoEarningActive ? 1 : 0);
        PlayerPrefs.Save();

        if (isAutoEarningActive)
        {
            AutoEarnCurrency();
        }
    }

    private void AutoEarnCurrency()
    {
        Bank.instance.AddCurrency(currentEarnings);
        BankUI.instance.UpdateCurrencyUI();
    }

    private void EarnCurrency()
    {
        bool hasTouchInput = Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();
        bool hasKeyboardInput = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return);

        if (hasTouchInput || hasKeyboardInput)
        {
            Bank.instance.AddCurrency(currentEarnings);
            BankUI.instance.UpdateCurrencyUI();
        }
    }

    void Rewarded(int id)
    {
        Bank.instance.AddCurrency(rewardedCurrencyAmount);
        BankUI.instance.UpdateCurrencyUI();
    }
}
