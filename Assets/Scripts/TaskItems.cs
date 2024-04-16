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
    [SerializeField] Button startTask;
    public TaskInventoryStart taskInventoryStart;
    public bool taskStarted;
    void Start()
    {
        Reference.Instance.uIManager.taskItems = this;
        startTask.onClick.AddListener(StartTask);
    }
    void Update()
    {
        
    }
    public void TaskUpdate(TaskInventoryStart taskInventoryStart)
    {
        this.taskInventoryStart = taskInventoryStart;
        itemName.text = taskInventoryStart.taskName;
        description.text = taskInventoryStart.taskDescription;
        itemImage.sprite = taskInventoryStart.taskImage;
        for (int i = 0; i < taskInventoryStart.rewardItems.Count; i++)
        {
            if (taskInventoryStart.itemsRequired[i].items)
            {
                Rewards(i);
                rewardItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.rewardItems[i].items.itemImage;
            }
            else if (taskInventoryStart.itemsRequired[i].weapons)
            {
                Rewards(i);
                rewardItems.GetChild(i).GetComponent<Image>().sprite = taskInventoryStart.rewardItems[i].weapons.weaponImage;
            }
            else if (taskInventoryStart.itemsRequired[i].armours)
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
    void StartTask()
    {
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
        bool progress = false;
        string message = "";
        for (int i = 0; i < taskInventoryStart.itemsRequired.Count; i++)
        {
            if (slot.weapons && slot.weapons == taskInventoryStart.itemsRequired[i].weapons)
            {
                Debug.Log("aa");
                message = message + taskInventoryStart.itemsRequired[i].weapons.name + " " + slot.quantityText.text + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                progress = true;
                taskInventoryStart.itemsRequired[i].collect = taskInventoryStart.itemsRequired[i].quantity;
            }
            else if (slot.armour && slot.armour == taskInventoryStart.itemsRequired[i].armours)
            {
                message = message + taskInventoryStart.itemsRequired[i].armours.name + " " + slot.quantityText.text + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                progress = true;
                taskInventoryStart.itemsRequired[i].collect = taskInventoryStart.itemsRequired[i].quantity;
            }
            else if (slot.items && slot.items == taskInventoryStart.itemsRequired[i].items && int.Parse(slot.quantityText.text) >= taskInventoryStart.itemsRequired[i].quantity)
            {
                message = message + taskInventoryStart.itemsRequired[i].items.name + " " + taskInventoryStart.itemsRequired[i].quantity + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                progress = true;
                taskInventoryStart.itemsRequired[i].collect = taskInventoryStart.itemsRequired[i].quantity;
            }
            else
            {
                if (taskInventoryStart.itemsRequired[i].items)
                {
                    Debug.Log("aa");
                    message = message + taskInventoryStart.itemsRequired[i].items.name + " " + taskInventoryStart.itemsRequired[i].collect + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                    Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                }
                else if (taskInventoryStart.itemsRequired[i].weapons)
                {
                    message = message + taskInventoryStart.itemsRequired[i].weapons.name + " " + taskInventoryStart.itemsRequired[i].collect + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                    Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                }
                else if (taskInventoryStart.itemsRequired[i].armours)
                {
                    message = message + taskInventoryStart.itemsRequired[i].armours.name + " " + taskInventoryStart.itemsRequired[i].collect + " / " + taskInventoryStart.itemsRequired[i].quantity.ToString() + "\n";
                    Reference.Instance.uIManager.NotificationActive(message, taskInventoryStart.taskImage, true, false);
                }
            }
        }
        return progress;
    }
}
