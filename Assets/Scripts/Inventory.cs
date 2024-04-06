using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] Sprite common, rare, legendary;
    //Player inventory sindeki inventory slotlar arasinda degis tokus ve item collect de slot update
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(transform.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }
    void Update()
    {
        
    }
    public void SlotUpdate(InventorySlot slot, int quantity, Sprite itemImage, RarityType.rarityType rarityType)
    {
        slot.quantityText.text = "" + quantity;
        slot.itemImage.sprite = itemImage;
        switch (rarityType)
        {
            case RarityType.rarityType.Common:
                slot.rarityImage.sprite = common;
                break;
            case RarityType.rarityType.Rare:
                slot.rarityImage.sprite = rare;
                break;
            case RarityType.rarityType.Legendary:
                slot.rarityImage.sprite = legendary;
                break;
            default:
                break;
        }
    }
    public void ItemSlotUpdate(int quantity, Sprite itemImage, RarityType.rarityType rarityType, Items items)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].items == null && slots[i].weapons == null && slots[i].armour == null)
            {
                SlotUpdate(slots[i], quantity , itemImage, rarityType);
                slots[i].items = items;
                return;
            }
        }
        int newQuantity = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].items == items)
            {
                newQuantity = (newQuantity == 0) ? (int.Parse(slots[i].quantityText.text) + quantity) : (int.Parse(slots[i].quantityText.text) + newQuantity);
                Debug.Log(newQuantity);
                SlotUpdate(slots[i], newQuantity > items.maxStock ? items.maxStock : newQuantity, itemImage, rarityType);
                slots[i].items = items;
                newQuantity -= items.maxStock;
                if (newQuantity <= 0)
                {
                    break;
                }
            }
        }
    }
    public void ArmourSlotUpdate(int quantity, Sprite itemImage, RarityType.rarityType rarityType, int level, float durability, Armours armours)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].items == null && slots[i].weapons == null && slots[i].armour == null)
            {
                SlotUpdate(slots[i], quantity, itemImage, rarityType);
                slots[i].armour = armours;
                slots[i].level = level;
                slots[i].durability = durability;
                break;
            }
        }
    }
    public void WeaponSlotUpdate(int quantity, Sprite itemImage, RarityType.rarityType rarityType, int level, float durability, Weapons weapons)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].items == null && slots[i].weapons == null && slots[i].armour == null)
            {
                SlotUpdate(slots[i], quantity, itemImage, rarityType);
                slots[i].weapons = weapons;
                slots[i].level = level;
                slots[i].durability = durability;
                break;
            }
        }
    }
}
