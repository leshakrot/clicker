using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank instance;

    private int _amountCurrency;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _amountCurrency = PlayerPrefs.GetInt("currency");
        BankUI.instance.UpdateCurrencyUI();
    }
    public void AddCurrency(int amount)
    {
        _amountCurrency += amount;
        PlayerPrefs.SetInt("currency", Bank.instance.GetAmountCurrency());
    }

    public void SpendCurrency(int amount)
    {
        _amountCurrency -= amount;
        PlayerPrefs.SetInt("currency", Bank.instance.GetAmountCurrency());
    }

    public int GetAmountCurrency()
    {
        return _amountCurrency;
    }
}
