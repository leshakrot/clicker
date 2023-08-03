using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isPurchased;

    [SerializeField] private int _price;

    public int GetPrice()
    {
        return _price;
    }
}
