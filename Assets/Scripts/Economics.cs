using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economics : MonoBehaviour
{
    public static Economics instance;
    int money;
    public int startMoney;
    List<Button> buttons;
    public List<int> prices;
    private void Awake()
    {
        instance = this;
        buttons = UIControll.instance.buttons;
        money = startMoney;
    }
    public void checkAvailableTowers()
    {
        for(int i = 0; i < buttons.Count && i <prices.Count; i++)
        {
            if (prices[i] < money)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    public void AddMoney(int amount)
    {
        money += amount;
    }
    public void Buy(int cost)
    {
        money -= cost;
    }
    public bool IsAvailable(int cost)
    {
        return money > cost;
    }
    public int GetPrice(int index)
    {
        if (index < 0 || index > prices.Count) return -1;
        else return prices[index];
    }
    private void Update()
    {
        checkAvailableTowers();
    }
}
