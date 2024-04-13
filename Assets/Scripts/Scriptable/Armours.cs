using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armours", menuName = "Inventory/Armours")]
public class Armours : ScriptableObject
{
    public string armourName;
    public string description;
    public Sprite armourImage;
    public GameObject prefab;
    public RarityType.rarityType rarityType;
    public float durability;
    public int levelIndex;
    public List<ArmourLevels> levelData;
    public EquipType.Type type;
}
[System.Serializable]
public class ArmourLevels
{
    public int sellPrice;
    public int buyPrice;
    public float armour;
    public float addHealth;
}

