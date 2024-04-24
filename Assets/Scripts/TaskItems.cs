using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class TaskItems : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName, description;
    [SerializeField] Image itemImage;
    [SerializeField] Transform requiredItems, rewardItems;
    [SerializeField] Button startTask, finishTask;
    public GameObject taskSlot;
    public TaskInventoryStart taskInventoryStart;
    public bool taskStarted;
    Inventory inventory;
    void Start()
    {
        inventory = Reference.Instance.inventoryManager.mainInventory;
        Reference.Instance.uIManager.taskItems = this;
        startTask.onClick.AddListener(TaskNotification);
        finishTask.onClick.AddListener(RewardTake);
    }
    void Update()
    {
        
    }
    public void StartTask(TaskInventoryStart taskInventoryStart)
    {
        this.taskInventoryStart = taskInventoryStart;
        itemName.text = taskInventoryStart.taskName;
        description.text = taskInventoryStart.taskDescription;
        itemImage.sprite = taskInventoryStart.taskImage;
        for (int i = 0; i < taskInventoryStart.rewardItems.Count; i++)
        {
            if (taskInventoryStart.rewardItems[i].items)
            {
                Rewards(i);
                rewardItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.rewardItems[i].items.itemImage;
            }
            else if (taskInventoryStart.rewardItems[i].weapons)
            {
                Rewards(i);
                rewardItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.rewardItems[i].weapons.weaponImage;
            }
            else if (taskInventoryStart.rewardItems[i].armours)
            {
                Rewards(i);
                rewardItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.rewardItems[i].armours.armourImage;
            }
        }
        for (int i = 0; i < taskInventoryStart.itemsRequired.Count; i++)
        {
            if (taskInventoryStart.itemsRequired[i].items)
            {
                Required(i);
                requiredItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.itemsRequired[i].items.itemImage;
            }
            else if (taskInventoryStart.itemsRequired[i].weapons)
            {
                Required(i);
                requiredItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.itemsRequired[i].weapons.weaponImage;
            }
            else if (taskInventoryStart.itemsRequired[i].armours)
            {
                Required(i);
                requiredItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.itemsRequired[i].armours.armourImage;
            }
        }
        if (taskInventoryStart.isStarted)
        {
            startTask.interactable = false;
        }
        else
        {
            startTask.interactable = true;
            finishTask.interactable = false;
        }
    }
    void Required(int i)
    {
        requiredItems.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = taskInventoryStart.itemsRequired[i].quantity.ToString();
        requiredItems.GetChild(i).gameObject.SetActive(true);
    }
    void Rewards(int i)
    {
        rewardItems.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = taskInventoryStart.rewardItems[i].quantity.ToString();
        rewardItems.GetChild(i).gameObject.SetActive(true);
    }
    void TaskNotification()
    {
        Reference.Instance.uIManager.taskSystem.currentGraph++;
        taskStarted = true;
        taskInventoryStart.isStarted = true;
        startTask.interactable = false;
        string message = "";
        for (int i = 0; i < taskInventoryStart.itemsRequired.Count; i++)
        {
            if (taskInventoryStart.itemsRequired[i].items)
            {
                message = message + taskInventoryStart.itemsRequired[i].items.name + " 0 / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
            }
            else if (taskInventoryStart.itemsRequired[i].weapons)
            {
                message = message + taskInventoryStart.itemsRequired[i].weapons.name + " 0 / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
            }
            else if (taskInventoryStart.itemsRequired[i].armours)
            {
                message = message + taskInventoryStart.itemsRequired[i].armours.name + " 0 / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
            }
        }
    }
    public bool TaskUpdate(InventorySlot slot)
    {
        bool progress = false, complete = true;
        string message = "";
        for (int i = 0; i < taskInventoryStart.itemsRequired.Count; i++)
        {
            if (slot.weapons && slot.weapons == taskInventoryStart.itemsRequired[i].weapons)
            {
                progress = true;
                inventory.SlotReset(slot);
                taskInventoryStart.itemsRequired[i].collect = taskInventoryStart.itemsRequired[i].quantity;
            }
            else if (slot.armour && slot.armour == taskInventoryStart.itemsRequired[i].armours)
            {
                progress = true;
                inventory.SlotReset(slot);
                taskInventoryStart.itemsRequired[i].collect = taskInventoryStart.itemsRequired[i].quantity;
            }
            else if (slot.items && slot.items == taskInventoryStart.itemsRequired[i].items && int.Parse(slot.quantityText.text) >= taskInventoryStart.itemsRequired[i].quantity)
            {
                progress = true;
                if (int.Parse(slot.quantityText.text) == taskInventoryStart.itemsRequired[i].quantity)
                {
                    inventory.SlotReset(slot);
                }
                else
                {
                    inventory.ItemSlotUpdate(slot, int.Parse(slot.quantityText.text) - taskInventoryStart.itemsRequired[i].quantity, slot.items.itemImage, slot.items.rarityType, slot.items);
                }
                taskInventoryStart.itemsRequired[i].collect = taskInventoryStart.itemsRequired[i].quantity;
            }
            if (taskInventoryStart.itemsRequired[i].items)
            {
                message = message + taskInventoryStart.itemsRequired[i].items.name + " " + taskInventoryStart.itemsRequired[i].collect + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                if (taskInventoryStart.itemsRequired[i].collect != taskInventoryStart.itemsRequired[i].quantity)
                {
                    complete = false;
                }
            }
            else if (taskInventoryStart.itemsRequired[i].weapons)
            {
                message = message + taskInventoryStart.itemsRequired[i].weapons.name + " " + taskInventoryStart.itemsRequired[i].collect + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                if (taskInventoryStart.itemsRequired[i].collect != taskInventoryStart.itemsRequired[i].quantity)
                {
                    complete = false;
                }
            }
            else if (taskInventoryStart.itemsRequired[i].armours)
            {
                message = message + taskInventoryStart.itemsRequired[i].armours.name + " " + taskInventoryStart.itemsRequired[i].collect + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                if (taskInventoryStart.itemsRequired[i].collect != taskInventoryStart.itemsRequired[i].quantity)
                {
                    complete = false;
                }
            }
        }
        if (complete)
        {
            finishTask.interactable = true;
        }
        return progress;
    }
    void RewardTake()
    {
        int emptySlot = 0;
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (!inventory.slots[i].items && !inventory.slots[i].armour && !inventory.slots[i].weapons)
            {
                emptySlot++;
            }
        }
        if (emptySlot < taskInventoryStart.rewardItems.Count)
        {
            return;
        }
        else
        {
            for (int i = 0; i < taskInventoryStart.rewardItems.Count; i++)
            {
                for (int j = 0; j < inventory.slots.Count; j++)
                {
                    if (!inventory.slots[j].items && !inventory.slots[j].armour && !inventory.slots[j].weapons)
                    {
                        if (taskInventoryStart.rewardItems[i].items)
                        {
                            inventory.ItemSlotUpdate(null, taskInventoryStart.rewardItems[i].quantity, taskInventoryStart.rewardItems[i].items.itemImage, 
                                taskInventoryStart.rewardItems[i].items.rarityType, taskInventoryStart.rewardItems[i].items);
                            break;
                        }
                        else if (taskInventoryStart.rewardItems[i].weapons)
                        {
                            inventory.WeaponSlotUpdate(null, taskInventoryStart.rewardItems[i].quantity, taskInventoryStart.rewardItems[i].weapons.weaponImage,
                                taskInventoryStart.rewardItems[i].weapons.rarityType, taskInventoryStart.rewardItems[i].level, 100, taskInventoryStart.rewardItems[i].weapons);
                            break;
                        }
                        else if (taskInventoryStart.rewardItems[i].armours)
                        {
                            inventory.ArmourSlotUpdate(null, taskInventoryStart.rewardItems[i].quantity, taskInventoryStart.rewardItems[i].armours.armourImage,
                                taskInventoryStart.rewardItems[i].armours.rarityType, taskInventoryStart.rewardItems[i].level, 100, taskInventoryStart.rewardItems[i].armours);
                            break;
                        }
                    }
                }
            }
        }
        taskStarted = false;
        taskInventoryStart.isStarted = false;
        startTask.interactable = true;
        finishTask.interactable = false;
        ResetItems();
        Reference.Instance.uIManager.NotificationActive("", taskInventoryStart.taskImage, true, true);
        Reference.Instance.uIManager.taskSystem.currentTask++;
        Reference.Instance.uIManager.taskSystem.currentGraph++;
        Reference.Instance.uIManager.taskSystem.WaitStart();
    }
    void ResetItems()
    {
        for (int i = 0; i < requiredItems.childCount; i++)
        {
            requiredItems.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < rewardItems.childCount; i++)
        {
            rewardItems.GetChild(i).gameObject.SetActive(false);
        }
    }
}
