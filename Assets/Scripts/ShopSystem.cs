using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    UIManager uIManager;
    Inventory mainInventory;
    [SerializeField] int shopCount;
    [SerializeField] List<ShopInventoryStart> inventoryStart, newRandomStart, randomStart;
    void Start()
    {
        mainInventory = Reference.Instance.inventoryManager.shopInventory;
        newRandomStart.AddRange(randomStart);
    }
    void WaitStart()
    {
        for (int i = 0; i < shopCount; i++)
        {
            InventorySlot mySlot = mainInventory.slots[i];
            if (inventoryStart[i].items)
            {
                mainInventory.ItemSlotUpdate(mySlot, inventoryStart[i].quantity, inventoryStart[i].items.itemImage, inventoryStart[i].items.rarityType,
                    inventoryStart[i].items);
            }
            else if (inventoryStart[i].weapons)
            {
                mainInventory.WeaponSlotUpdate(mySlot, 1, inventoryStart[i].weapons.weaponImage, inventoryStart[i].weapons.rarityType, inventoryStart[i].level
                    , inventoryStart[i].durability, inventoryStart[i].weapons);
            }
            else if (inventoryStart[i].armours)
            {
                mainInventory.ArmourSlotUpdate(mySlot, 1, inventoryStart[i].armours.armourImage, inventoryStart[i].armours.rarityType, inventoryStart[i].level,
                    inventoryStart[i].durability, inventoryStart[i].armours);
            }
        }
    }
    public void RandomShopItems()
    {
        int randomSlot = Random.Range(2, mainInventory.slots.Count - 2);
        shopCount = randomSlot;
        for (int i = 0; i < randomSlot; i++)
        {
            InventorySlot mySlot = mainInventory.slots[i];
            int random = Random.Range(0, randomStart.Count);
            Debug.Log(random);
            if (randomStart[random].items)
            {
                mainInventory.ItemSlotUpdate(mySlot, randomStart[random].quantity, randomStart[random].items.itemImage, randomStart[random].items.rarityType,
                    randomStart[random].items);
            }
            else if (randomStart[random].weapons)
            {
                mainInventory.WeaponSlotUpdate(mySlot, 1, randomStart[random].weapons.weaponImage, randomStart[random].weapons.rarityType, randomStart[random].level
                    , randomStart[random].durability, randomStart[random].weapons);
            }
            else if (randomStart[random].armours)
            {
                mainInventory.ArmourSlotUpdate(mySlot, 1, randomStart[random].armours.armourImage, randomStart[random].armours.rarityType, randomStart[random].level,
                    randomStart[random].durability, randomStart[random].armours);
            }
            randomStart.RemoveAt(random);
        }
        randomStart.Clear();
        randomStart.AddRange(newRandomStart);
        ShopItemsUpdate();
    }
    public void ShopItemsUpdate()
    {
        for (int i = 0; i < mainInventory.slots.Count; i++)
        {
            if (mainInventory.slots[i].weapons || mainInventory.slots[i].armour)
            {
                inventoryStart[i].weapons = mainInventory.slots[i].weapons ? mainInventory.slots[i].weapons : null;
                inventoryStart[i].armours = mainInventory.slots[i].armour ? mainInventory.slots[i].armour : null;
                inventoryStart[i].durability = mainInventory.slots[i].durability;
                inventoryStart[i].level = mainInventory.slots[i].level;
                inventoryStart[i].items = null;
            }
            else if (mainInventory.slots[i].items)
            {
                inventoryStart[i].items = mainInventory.slots[i].items;
                inventoryStart[i].quantity = int.Parse(mainInventory.slots[i].quantityText.text);
                inventoryStart[i].weapons = null;
                inventoryStart[i].armours = null;
            }
            else
            {
                inventoryStart[i].weapons = null;
                inventoryStart[i].armours = null;
                inventoryStart[i].items = null;
            }
        }
        for (int i = mainInventory.slots.Count; i < inventoryStart.Count; i++)
        {
            inventoryStart[i].weapons = null;
            inventoryStart[i].armours = null;
            inventoryStart[i].items = null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Reference.Instance.inventoryManager.shopPanel.alpha != 1)
        {
            if (!uIManager)
            {
                uIManager = Reference.Instance.uIManager;
            }
            uIManager.InteractActive("Speak");
            if (Input.GetKeyDown(KeyCode.E))
            {
                uIManager.InteractPassive();
                if (uIManager.shopSystem != this)
                {
                    WaitStart();
                }
                uIManager.shopSystem = this;
                Reference.Instance.inventoryManager.craftPanel.alpha = 0;
                Reference.Instance.inventoryManager.chestPanel.alpha = 0;
                Reference.Instance.inventoryManager.shopPanel.alpha = 1;
                Reference.Instance.inventoryManager.craftPanel.blocksRaycasts = false;
                Reference.Instance.inventoryManager.chestPanel.blocksRaycasts = false;
                Reference.Instance.inventoryManager.shopPanel.blocksRaycasts = true;
                Reference.Instance.inventoryManager.inventoryPanel.alpha = 1;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIManager.InteractPassive();
        }
    }
}
[System.Serializable]
public class ShopInventoryStart
{
    public Items items;
    public Weapons weapons;
    public Armours armours;
    public float durability;
    public int level;
    public int quantity;
}
