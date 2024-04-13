using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName, description, quantity, maxStock, sellPrice, buyPrice;
    [SerializeField] Image rarity, itemImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DetailsUpdate(string itemName, string description, string quantity, int maxStock, int sellPrice, int buyPrice, Image rarity, Image itemImage)
    {
        this.itemName.text = itemName;
        this.description.text = description;
        this.quantity.text = quantity;
        this.maxStock.text = maxStock.ToString();
        this.sellPrice.text = sellPrice.ToString();
        this.buyPrice.text = buyPrice.ToString();
        this.rarity.sprite = rarity.sprite;
        this.itemImage.sprite = itemImage.sprite;
    }
}
