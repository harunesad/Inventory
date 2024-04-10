using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] Inventory mainInventory;
    [SerializeField] CanvasGroup ýnventoryPanel, shopPanel;
    [SerializeField] List<ShopInventoryStart> inventoryStart;
    void Start()
    {
        //Invoke("WaitStart", 2);
    }
    void WaitStart()
    {
        for (int i = 0; i < inventoryStart.Count; i++)
        {
            InventorySlot mySlot = mainInventory.slots[i];
            if (inventoryStart[i].items != null)
            {
                mainInventory.ItemSlotUpdate(mySlot, inventoryStart[i].quantity, inventoryStart[i].items.itemImage, inventoryStart[i].items.rarityType,
                    inventoryStart[i].items);
            }
            else if (inventoryStart[i].weapons != null)
            {
                mainInventory.WeaponSlotUpdate(mySlot, 1, inventoryStart[i].weapons.weaponImage, inventoryStart[i].weapons.rarityType, inventoryStart[i].level
                    , inventoryStart[i].durability, inventoryStart[i].weapons);
            }
            else if (inventoryStart[i].armours != null)
            {
                mainInventory.ArmourSlotUpdate(mySlot, 1, inventoryStart[i].armours.armourImage, inventoryStart[i].armours.rarityType, inventoryStart[i].level,
                    inventoryStart[i].durability, inventoryStart[i].armours);
            }
        }
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
