using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    UIManager uIManager;
    [SerializeField] List<TaskInventoryStart> inventoryStart;
    int currentTask;
    void Start()
    {

    }
    void WaitStart()
    {
        for (int i = 0; i < inventoryStart.Count; i++)
        {
            //InventorySlot mySlot = mainInventory.slots[i];
            //if (inventoryStart[i].items)
            //{
            //    mainInventory.ItemSlotUpdate(mySlot, inventoryStart[i].quantity, inventoryStart[i].items.itemImage, inventoryStart[i].items.rarityType,
            //        inventoryStart[i].items);
            //}
            //else if (inventoryStart[i].weapons)
            //{
            //    mainInventory.WeaponSlotUpdate(mySlot, 1, inventoryStart[i].weapons.weaponImage, inventoryStart[i].weapons.rarityType, inventoryStart[i].level
            //        , inventoryStart[i].durability, inventoryStart[i].weapons);
            //}
            //else if (inventoryStart[i].armours)
            //{
            //    mainInventory.ArmourSlotUpdate(mySlot, 1, inventoryStart[i].armours.armourImage, inventoryStart[i].armours.rarityType, inventoryStart[i].level,
            //        inventoryStart[i].durability, inventoryStart[i].armours);
            //}
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Reference.Instance.inventoryManager.taskPanel.alpha != 1)
        {
            if (!uIManager)
            {
                uIManager = Reference.Instance.uIManager;
            }
            uIManager.InteractActive("Speak");
            if (Input.GetKeyDown(KeyCode.E))
            {
                uIManager.InteractPassive();
                //if (uIManager.chestSystem != this)
                //{
                //    WaitStart();
                //}
                uIManager.taskSystem = this;
                //Reference.Instance.inventoryManager.craftPanel.alpha = 0;
                //Reference.Instance.inventoryManager.shopPanel.alpha = 0;
                Reference.Instance.inventoryManager.taskPanel.alpha = 1;
                //Reference.Instance.inventoryManager.craftPanel.blocksRaycasts = false;
                //Reference.Instance.inventoryManager.shopPanel.blocksRaycasts = false;
                Reference.Instance.inventoryManager.taskPanel.blocksRaycasts = true;
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
public class TaskInventoryStart
{
    public List<ItemsRequired> items;
    public List<Weapons> weapons;
    public List<Armours> armours;
    public string taskName;
    public string taskDescription;
    public Sprite taskImage;
    public Rewards rewards;
    public bool isStarted;
}
[System.Serializable] 
public class Rewards
{
    public List<ItemsRequired> items;
    public List<Weapons> weapons;
    public List<Armours> armours;
}
[System.Serializable]
public class ItemsRequired
{
    public Items items;
    public int quantity;
}