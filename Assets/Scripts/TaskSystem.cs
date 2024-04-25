using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    [SerializeField] NodeParser dialogue;
    public int currentTask, currentGraph;
    [SerializeField] List<TaskInventoryStart> inventoryStart;
    TaskItems taskItems;
    UIManager uIManager;
    void Start()
    {

    }
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Reference.Instance.uIManager.speakPanel.alpha == 0)
        {
            if (!uIManager)
            {
                uIManager = Reference.Instance.uIManager;
            }
            uIManager.InteractActive("Speak");
            if (Input.GetKeyDown(KeyCode.E) && dialogue.graph.Count > currentGraph)
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
                    dialogue.DialogueStart();
                    Reference.Instance.uIManager.speakPanel.alpha = 1;
                    Reference.Instance.uIManager.speakPanel.blocksRaycasts = true;
                    Reference.Instance.uIManager.InteractPassive();
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
    public void TaskOpen()
    {
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