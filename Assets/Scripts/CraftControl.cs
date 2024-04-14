using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftControl : MonoBehaviour
{
    [SerializeField] InventorySlot firstCraftSlot, secondCraftSlot, resultCraftSlot;
    [SerializeField] Button craft;
    public List<ItemProperty> firsSlot, secondSlot;
    public List<CraftProperty> resultSlot;
    void Start()
    {
        craft.onClick.AddListener(Crafted);
        for (int i = 0; i < firsSlot.Count; i++)
        {
            CraftItems craftItems = transform.GetChild(i).GetComponent<CraftItems>();
            Items firstItem = firsSlot[i].items;
            Items secondItem = secondSlot[i].items;
            Reference.Instance.inventoryManager.craftInventory.ItemSlotUpdate(craftItems.firstSlot, firsSlot[i].quantity, firstItem.itemImage, firstItem.rarityType, firstItem);
            Reference.Instance.inventoryManager.craftInventory.ItemSlotUpdate(craftItems.secondSlot, secondSlot[i].quantity, secondItem.itemImage, secondItem.rarityType, secondItem);
            if (resultSlot[i].items)
            {
                Items resultItem = resultSlot[i].items;
                Reference.Instance.inventoryManager.craftInventory.ItemSlotUpdate(craftItems.resultSlot, resultSlot[i].quantity, resultItem.itemImage, resultItem.rarityType, resultItem);
            }
            else if (resultSlot[i].armours)
            {
                Armours resultArmour = resultSlot[i].armours;
                Reference.Instance.inventoryManager.craftInventory.ArmourSlotUpdate(craftItems.resultSlot, resultSlot[i].quantity, resultArmour.armourImage, resultArmour.rarityType, resultSlot[i].level, 100, resultArmour);
            }
            else if (resultSlot[i].weapons)
            {
                Weapons resultWeapon = resultSlot[i].weapons;
                Reference.Instance.inventoryManager.craftInventory.WeaponSlotUpdate(craftItems.resultSlot, resultSlot[i].quantity, resultWeapon.weaponImage, resultWeapon.rarityType, resultSlot[i].level, 100, resultWeapon);
            }
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    void Update()
    {
        
    }
    public bool Craftable()
    {
        bool craft = false;
        int index = 0;
        for (int i = 0; i < firsSlot.Count; i++)
        {
            if (firsSlot[i].items == firstCraftSlot.items && int.Parse(firstCraftSlot.quantityText.text) >= firsSlot[i].quantity)
            {
                if (secondSlot[i].items == secondCraftSlot.items && int.Parse(secondCraftSlot.quantityText.text) >= secondSlot[i].quantity)
                {
                    craft = true;
                }
            }
        }
        if (craft)
        {
            if (resultSlot[index].items)
            {
                Items resultItem = resultSlot[index].items;
                Reference.Instance.inventoryManager.craftInventory.ItemSlotUpdate(resultCraftSlot, resultSlot[index].quantity, resultItem.itemImage, resultItem.rarityType, resultItem);
            }
            else if (resultSlot[index].armours)
            {
                Armours resultArmour = resultSlot[index].armours;
                Reference.Instance.inventoryManager.craftInventory.ArmourSlotUpdate(resultCraftSlot, resultSlot[index].quantity, resultArmour.armourImage, resultArmour.rarityType, resultSlot[index].level, 100, resultArmour);
            }
            else if (resultSlot[index].weapons)
            {
                Weapons resultWeapon = resultSlot[index].weapons;
                Reference.Instance.inventoryManager.craftInventory.WeaponSlotUpdate(resultCraftSlot, resultSlot[index].quantity, resultWeapon.weaponImage, resultWeapon.rarityType, resultSlot[index].level, 100, resultWeapon);
            }
        }
        else
        {
            Reference.Instance.inventoryManager.craftInventory.SlotReset(resultCraftSlot);
        }
        return craft;
    }
    public void CraftSlotUpdate()
    {
        CraftSlotReset(firstCraftSlot);
        CraftSlotReset(secondCraftSlot);
        CraftSlotReset(resultCraftSlot);
    }
    void CraftSlotReset(InventorySlot slot)
    {
        if (slot.items && resultCraftSlot != slot)
        {
            Reference.Instance.inventoryManager.mainInventory.ItemSlotUpdate(null, int.Parse(slot.quantityText.text), slot.items.itemImage, slot.items.rarityType, slot.items);
        }
        else if (slot.weapons && resultCraftSlot != slot)
        {
            Reference.Instance.inventoryManager.mainInventory.WeaponSlotUpdate(null, 1, slot.weapons.weaponImage, slot.weapons.rarityType, slot.level, 100, slot.weapons);
        }
        else if (slot.armour && resultCraftSlot != slot)
        {
            Reference.Instance.inventoryManager.mainInventory.ArmourSlotUpdate(null, 1, slot.armour.armourImage, slot.armour.rarityType, slot.level, 100, slot.armour);
        }
        Reference.Instance.inventoryManager.craftInventory.SlotReset(slot);
    }
    void Crafted()
    {
        if (Craftable())
        {
            InventorySlot slot = resultCraftSlot;
            if (slot.items)
            {
                Reference.Instance.inventoryManager.mainInventory.ItemSlotUpdate(null, int.Parse(slot.quantityText.text), slot.items.itemImage, slot.items.rarityType, slot.items);
            }
            else if (slot.weapons)
            {
                Reference.Instance.inventoryManager.mainInventory.WeaponSlotUpdate(null, 1, slot.weapons.weaponImage, slot.weapons.rarityType, slot.level, 100, slot.weapons);
            }
            else if (slot.armour)
            {
                Reference.Instance.inventoryManager.mainInventory.ArmourSlotUpdate(null, 1, slot.armour.armourImage, slot.armour.rarityType, slot.level, 100, slot.armour);
            }
            Reference.Instance.inventoryManager.craftInventory.SlotReset(firstCraftSlot);
            Reference.Instance.inventoryManager.craftInventory.SlotReset(secondCraftSlot);
            Reference.Instance.inventoryManager.craftInventory.SlotReset(resultCraftSlot);
        }
    }
}
[System.Serializable]
public class ItemProperty
{
    public Items items;
    public int quantity;
}
[System.Serializable]
public class CraftProperty
{
    public Items items;
    public Weapons weapons;
    public Armours armours;
    public int quantity;
    public int level;
}
