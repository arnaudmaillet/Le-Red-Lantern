using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    // select the money text object in the inspector
    public TMPro.TextMeshProUGUI moneyText;
    private int money = 0;

    void Start()
    {
        moneyText.text = money.ToString();
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Update();
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
        Update();
    }

    public void ResetMoney()
    {
        money = 0;
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = money.ToString();
    }
}
