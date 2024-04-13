using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items" , menuName = "Inventory/Items")]
public class Items : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite itemImage;
    public GameObject prefab;
    public RarityType.rarityType rarityType;
    public int maxStock;
    public int sellPrice;
    public int buyPrice;
    public bool drink, eat, use;
}
