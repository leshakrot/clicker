using UnityEngine;
using UnityEngine.UI;

public class ButtonVisibility : MonoBehaviour
{
    public Button[] buttons;
    private PurchaseButton _purchaseButton;

    void Start()
    {
        RestoreButtonVisibility();
    }

    void RestoreButtonVisibility()
    {
        foreach (Button button in buttons)
        {
            string key = "Button" + button.name;
            bool isVisible = PlayerPrefs.GetInt(key, 1) == 1;

            button.gameObject.SetActive(isVisible);
        }
    }

    public void OnButtonClick(Button button)
    {
        if(button.gameObject.GetComponent<PurchaseButton>() == null)
        {
            if (2500 <= Bank.instance.GetAmountCurrency())
            {
                Debug.Log("NULL");
                string key = "Button" + button.name;
                button.gameObject.SetActive(false);
                PlayerPrefs.SetInt(key, BoolToInt(button.gameObject.activeSelf));    
            }
            return;
        }

        _purchaseButton = button.gameObject.GetComponent<PurchaseButton>();
        if (_purchaseButton.GetCharacter().GetPrice() <= Bank.instance.GetAmountCurrency())
        { 
            string key = "Button" + button.name;
            button.gameObject.SetActive(false);
            PlayerPrefs.SetInt(key, BoolToInt(button.gameObject.activeSelf));
        }
    }

    private int BoolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    private bool IntToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
