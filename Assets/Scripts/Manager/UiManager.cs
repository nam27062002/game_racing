using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Button shopButton;
    [SerializeField] private GameObject shopObject;
    private void Awake()
    {
        shopButton.onClick.AddListener(OnShopButtonClick);
    }

    private void OnShopButtonClick()
    {
        shopObject.SetActive(true);
    }
}
