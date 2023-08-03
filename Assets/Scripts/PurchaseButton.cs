using UnityEngine;
using TMPro;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField] private Character _character;

    private TextMeshProUGUI _priceText;

    private void Start()
    {
        _priceText = GetComponentInChildren<TextMeshProUGUI>();
        _priceText.text = _character.GetPrice().ToString();
    }

    public Character GetCharacter()
    {
        return _character;
    }
}
