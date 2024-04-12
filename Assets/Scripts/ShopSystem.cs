using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] Inventory mainInventory;
    [SerializeField] CanvasGroup ýnventoryPanel, shopPanel;
    [SerializeField] List<ShopInventoryStart> inventoryStart, newRandomStart, randomStart;
    [SerializeField] int shopCount;
    void Start()
    {
        //Invoke("WaitStart", 2);
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
            //inventoryStart.RemoveAt(i);
        }
        //inventoryStart.Clear();
        //inventoryStart.AddRange(newInventoryStart);
    }
    public void RandomShopItems()
    {
        int randomSlot = Random.Range(2, mainInventory.slots.Count - 2);
        shopCount = randomSlot;
        //inventoryStart.Clear();
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
            //inventoryStart.Add(randomStart[random]);
            randomStart.RemoveAt(random);
        }
        randomStart.Clear();
        randomStart.AddRange(newRandomStart);
        ShopItemsUpdate();
    }
    public void ShopItemsUpdate()
    {
        //inventoryStart.Clear();
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
        //inventoryStart[index]
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && shopPanel.alpha != 1)
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
                shopPanel.alpha = 1;
                ýnventoryPanel.alpha = 1;
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
