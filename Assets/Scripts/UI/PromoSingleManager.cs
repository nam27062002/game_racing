using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromoSingleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private Button buyButton;
    [SerializeField] private Image image;
    [SerializeField] private Color c1;
    [SerializeField] private Color c2;
    private void Awake()
    {
        
    }

    public void SetUI(string text,int number, bool enabled)
    {
        title.text = text;
        cost.text = number.ToString();
        buyButton.enabled = enabled;
        image.color = enabled ? c1 : c2;
    }
}
