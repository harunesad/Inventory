using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waeapons", menuName = "Inventory/Waeapons")]
public class Weapons : ScriptableObject
{
    public string weaponName;
    public string description;
    public Sprite weaponImage;
    public GameObject prefab;
    public RarityType.rarityType rarityType;
    public float durability;
    public int levelIndex;
    public List<WeaponLevels> levelData;
    public EquipType.Type type;
    public WeaponType weaponType;
    public enum WeaponType
    {
        Sword,
        Axe,
        Bow
    }
}
[System.Serializable]
public class WeaponLevels
{
    public int sellPrice;
    public int buyPrice;
    public float attack;
    public float attackSpeed;
    public float range;
}
