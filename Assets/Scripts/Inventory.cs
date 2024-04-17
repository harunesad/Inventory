using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    [SerializeField] Sprite common, rare, legendary;
    public bool isMine, shop;
    //Player inventory sindeki inventory slotlar arasinda degis tokus ve item collect de slot update
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<InventorySlot>(out InventorySlot slot) && !transform.GetChild(i).TryGetComponent<EquipItem>(out EquipItem equipItem))
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
    public void ItemSlotUpdate(InventorySlot slot, int quantity, Sprite itemImage, RarityType.rarityType rarityType, Items items)
    {
        if (slot == null)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].items && !slots[i].weapons && !slots[i].armour)
                {
                    Debug.Log(i);
                    SlotUpdate(slots[i], quantity, itemImage, rarityType);
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
        else
        {
            SlotUpdate(slot, quantity, itemImage, rarityType);
            slot.items = items;
        }
    }
    public void ArmourSlotUpdate(InventorySlot slot, int quantity, Sprite itemImage, RarityType.rarityType rarityType, int level, float durability, Armours armours)
    {
        if (!slot)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].items && !slots[i].weapons && !slots[i].armour)
                {
                    SlotUpdate(slots[i], quantity, itemImage, rarityType);
                    slots[i].armour = armours;
                    slots[i].level = level;
                    slots[i].durability = durability;
                    break;
                }
            }
        }
        else
        {
            SlotUpdate(slot, quantity, itemImage, rarityType);
            slot.armour = armours;
            slot.level = level;
            slot.durability = durability;
        }
    }
    public void WeaponSlotUpdate(InventorySlot slot, int quantity, Sprite itemImage, RarityType.rarityType rarityType, int level, float durability, Weapons weapons)
    {
        if (slot == null)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].items && !slots[i].weapons && !slots[i].armour)
                {
                    SlotUpdate(slots[i], quantity, itemImage, rarityType);
                    slots[i].weapons = weapons;
                    slots[i].level = level;
                    slots[i].durability = durability;
                    break;
                }
            }
        }
        else
        {
            SlotUpdate(slot, quantity, itemImage, rarityType);
            slot.weapons = weapons;
            slot.level = level;
            slot.durability = durability;
        }
    }
    public void SlotReset(InventorySlot slot)
    {
        slot.quantityText.text = "";
        slot.itemImage.sprite = null;
        slot.rarityImage.sprite = null;
        slot.items = null;
        slot.weapons = null;
        slot.armour = null;
        slot.level = 0;
        slot.durability = 0;
    }
    public void SwitchSlot(InventorySlot mySlot, InventorySlot changeSlot)
    {
        var quantity = mySlot.quantityText.text;
        var itemImage = mySlot.itemImage.sprite;
        var rarityImage = mySlot.rarityImage.sprite;
        var items = mySlot.items;
        var weapons = mySlot.weapons;
        var armour = mySlot.armour;
        var level = mySlot.level;
        var durability = mySlot.durability;

        if (changeSlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equipItem) && mySlot.items == null && (changeSlot.weapons || changeSlot.armour))
        {
            equipItem.Unequip();
        }
        else if (mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equipItem1) && changeSlot.items == null && (mySlot.weapons || mySlot.armour))
        {
            equipItem1.Unequip();
        }

        mySlot.itemImage.sprite = changeSlot.itemImage.sprite;
        mySlot.rarityImage.sprite = changeSlot.rarityImage.sprite;
        mySlot.items = changeSlot.items;
        mySlot.weapons = changeSlot.weapons;
        mySlot.armour = changeSlot.armour;
        mySlot.level = changeSlot.level;
        mySlot.durability = changeSlot.durability;

        changeSlot.itemImage.sprite = itemImage;
        changeSlot.rarityImage.sprite = rarityImage;
        changeSlot.items = items;
        changeSlot.weapons = weapons;
        changeSlot.armour = armour;
        changeSlot.level = level;
        changeSlot.durability = durability;

        if (mySlot.items && changeSlot.items && changeSlot.items == mySlot.items && mySlot != changeSlot)
        {
            int newQuantity = int.Parse(changeSlot.quantityText.text) + int.Parse(quantity);
            changeSlot.quantityText.text = newQuantity > changeSlot.items.maxStock ? changeSlot.items.maxStock.ToString() : newQuantity.ToString();
            newQuantity -= int.Parse(changeSlot.quantityText.text);
            if (newQuantity <= 0)
            {
                SlotReset(mySlot);
            }
            else
            {
                mySlot.quantityText.text = newQuantity.ToString();
            }
        }
        else
        {
            mySlot.quantityText.text = changeSlot.quantityText.text;
            changeSlot.quantityText.text = quantity;
        }

        if (changeSlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equip) && !mySlot.items)
        {
            equip.Equip();
        }
        else if (mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equip1) && !changeSlot.items && (mySlot.weapons || mySlot.armour))
        {
            equip1.Equip();
        }
    }
}
