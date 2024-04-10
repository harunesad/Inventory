using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] Inventory mainInventory;
    [SerializeField] CanvasGroup ýnventoryPanel, shopPanel;
    [SerializeField] List<ShopInventoryStart> inventoryStart, newInventoryStart;
    void Start()
    {
        //Invoke("WaitStart", 2);
        newInventoryStart.AddRange(inventoryStart);
    }
    public void WaitStart()
    {
        int randomSlot = Random.Range(2, mainInventory.slots.Count - 2);
        for (int i = 0; i < randomSlot; i++)
        {
            InventorySlot mySlot = mainInventory.slots[i];
            int random = Random.Range(0, inventoryStart.Count);
            Debug.Log(random);
            if (inventoryStart[random].items != null)
            {
                mainInventory.ItemSlotUpdate(mySlot, inventoryStart[random].quantity, inventoryStart[random].items.itemImage, inventoryStart[random].items.rarityType,
                    inventoryStart[random].items);
            }
            else if (inventoryStart[random].weapons != null)
            {
                mainInventory.WeaponSlotUpdate(mySlot, 1, inventoryStart[random].weapons.weaponImage, inventoryStart[random].weapons.rarityType, inventoryStart[random].level
                    , inventoryStart[random].durability, inventoryStart[random].weapons);
            }
            else if (inventoryStart[random].armours != null)
            {
                mainInventory.ArmourSlotUpdate(mySlot, 1, inventoryStart[random].armours.armourImage, inventoryStart[random].armours.rarityType, inventoryStart[random].level,
                    inventoryStart[random].durability, inventoryStart[random].armours);
            }
            inventoryStart.RemoveAt(random);
        }
        inventoryStart.Clear();
        inventoryStart.AddRange(newInventoryStart);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && shopPanel.alpha != 1)
        {
            uIManager.InteractActive("Speak");
            if (Input.GetKeyDown(KeyCode.E))
            {
                uIManager.shopSystem = this;
                uIManager.InteractPassive();
                WaitStart();
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
