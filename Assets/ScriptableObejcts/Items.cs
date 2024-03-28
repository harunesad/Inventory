using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items" , menuName = "Inventory/Items")]
public class Items : ScriptableObject
{
    public string itemName;
    public string description;
    public int maxStock;
    public int sellPrice;
    public int buyPrice;
    public Sprite itemImage;
    public bool drink, eat, use;
    public GameObject prefab;
}
