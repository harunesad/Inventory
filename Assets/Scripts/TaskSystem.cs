using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    UIManager uIManager;
    [SerializeField] List<TaskInventoryStart> inventoryStart;
    [SerializeField] TaskItems taskItems;
    public int currentTask;
    void Start()
    {

    }
    public void WaitStart()
    {
        if (currentTask > inventoryStart.Count - 1)
        {
            Debug.Log("aaa");
            Reference.Instance.inventoryManager.taskEmptyPanel.gameObject.SetActive(true);
        }
        else
        {
            taskItems.StartTask(inventoryStart[currentTask]);
            Reference.Instance.inventoryManager.taskEmptyPanel.gameObject.SetActive(false);
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
                taskItems = Reference.Instance.uIManager.taskItems;
                if ((Reference.Instance.uIManager.taskSystem == this || !taskItems.taskStarted))
                {
                    uIManager.InteractPassive();
                    if (currentTask > inventoryStart.Count - 1)
                    {
                        Debug.Log("aaa");
                        Reference.Instance.inventoryManager.taskEmptyPanel.gameObject.SetActive(true);
                    }
                    if (uIManager.taskSystem != this)
                    {
                        WaitStart();
                    }
                    Reference.Instance.uIManager.taskSystem = this;
                    Reference.Instance.inventoryManager.taskPanel.alpha = 1;
                    Reference.Instance.inventoryManager.taskPanel.blocksRaycasts = true;
                    Reference.Instance.inventoryManager.inventoryPanel.alpha = 1;
                }
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
    public List<ItemsRequired> itemsRequired;
    public string taskName;
    public string taskDescription;
    public Sprite taskImage;
    public List<ItemsRequired> rewardItems;
    public bool isStarted, isFinished;
}
[System.Serializable]
public class ItemsRequired
{
    public Items items;
    public Weapons weapons;
    public Armours armours;
    public int quantity;
    public int level;
    public int collect;
}