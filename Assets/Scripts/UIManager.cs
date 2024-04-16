using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup interact, notification, taskNotification;
    [SerializeField] TextMeshProUGUI message, coinText;
    [SerializeField] Button refresh;
    [SerializeField] int refreshCost;
    public int coin;
    public ShopSystem shopSystem;
    public ChestSystem chestSystem;
    public TaskSystem taskSystem;
    public CraftControl craftControl;
    public TaskItems taskItems;
    void Start()
    {
        CoinUpdate();
        refresh.onClick.AddListener(Refresh);
    }
    void Update()
    {
        
    }
    public void CoinUpdate()
    {
        coinText.text = coin.ToString();
    }
    void Refresh()
    {
        if (coin >= refreshCost)
        {
            coin -= refreshCost;
            coinText.text = coin.ToString();
            shopSystem.RandomShopItems();
        }
    }
    public void NotificationActive(string message, Sprite itemSprite, bool task, bool taskComplete)
    {
        if (!task)
        {
            notification.GetComponentInChildren<TextMeshProUGUI>().text = message;
            notification.transform.GetChild(0).GetComponent<Image>().sprite = itemSprite;
            notification.alpha = 1;
            StartCoroutine(NotificationPassive());
        }
        else
        {
            if (taskComplete)
            {
                taskNotification.alpha = 0;
                return;
            }
            taskNotification.GetComponentInChildren<TextMeshProUGUI>().text = message;
            taskNotification.transform.GetChild(0).GetComponent<Image>().sprite = itemSprite;
            taskNotification.alpha = 1;
        }
    }
    IEnumerator NotificationPassive()
    {
        yield return new WaitForSeconds(1);
        notification.alpha = 0;
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
