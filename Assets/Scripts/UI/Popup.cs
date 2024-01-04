using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Button buttonClose;
    [SerializeField] private Button buttonConfirm;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    private void Awake()
    {
        buttonClose.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        buttonConfirm.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }
    
    public void OpenPopup(string title,string description)
    {
        this.title.text = title;
        this.description.text = description;
        gameObject.SetActive(true);
    }
}
