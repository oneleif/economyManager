using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public PlayerData playerData;

    public Text playerMoneyText;

    public void UpdatePlayerMoney(long moneyMade)
    {
        playerData.playerMoney += moneyMade;
        playerMoneyText.text = "$" + playerData.playerMoney;
    }
}
