using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<PromoSingleManager> promoSingleManager;
    [SerializeField] private int maxScore = 100;
    private void OnEnable()
    {
        var count = 0;
        var currentCoin = DatabaseManager.Instance.GetMoney();
        foreach (var promo in APIManager.Instance.PromoData.data)
        {
            promoSingleManager[count].SetUI(promo.title,100 - 10 * count,currentCoin >= 100 - 10 * count );
            count++;
        }
    }
}
