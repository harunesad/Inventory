using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup interact;
    [SerializeField] TextMeshProUGUI message, coinText;
    [SerializeField] Button refresh;
    [SerializeField] int coin, refreshCost;
    public ShopSystem shopSystem;
    void Start()
    {
        coinText.text = coin.ToString();
        refresh.onClick.AddListener(Refresh);
    }
    void Update()
    {
        
    }
    void Refresh()
    {
        if (coin >= refreshCost)
        {
            coin -= refreshCost;
            coinText.text = coin.ToString();
            shopSystem.WaitStart();
        }
    }
    public void InteractActive(string message)
    {
        this.message.text = message;
        interact.alpha = 1;
    }
    public void InteractPassive()
    {
        interact.alpha = 0;
    }
}
