using UnityEngine;
using TMPro;

public class BankUI : MonoBehaviour
{
    public static BankUI instance;

    [SerializeField] private TextMeshProUGUI _currencyAmountText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateCurrencyUI()
    {
        _currencyAmountText.text = $"{Bank.instance.GetAmountCurrency()}";
    }
}
