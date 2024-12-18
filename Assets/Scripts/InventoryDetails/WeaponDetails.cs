using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDetails : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName, description, quantity, sellPrice, buyPrice, attack, attackSpeed;
    [SerializeField] Image rarity, itemImage, durability;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DetailsUpdate(string itemName, string description, string quantity, float durability, int sellPrice, int buyPrice, float attack, float attackSpeed,
        Image rarity, Image itemImage)
    {
        this.itemName.text = itemName;
        this.description.text = description;
        this.quantity.text = quantity;
        this.durability.fillAmount = durability;
        this.sellPrice.text = sellPrice.ToString();
        this.buyPrice.text = buyPrice.ToString();
        this.attack.text = attack.ToString();
        this.attackSpeed.text = attackSpeed.ToString();
        this.rarity.sprite = rarity.sprite;
        this.itemImage.sprite = itemImage.sprite;
    }
}
