using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    UIManager uIManager;
    Inventory mainInventory;
    void Start()
    {
        mainInventory = Reference.Instance.inventoryManager.craftInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Reference.Instance.inventoryManager.chestPanel.alpha != 1)
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
                //uIManager.craftSystem = this;
                //Reference.Instance.inventoryManager.chestPanel.alpha = 0;
                //Reference.Instance.inventoryManager.shopPanel.alpha = 0;
                Reference.Instance.inventoryManager.craftPanel.alpha = 1;
                //Reference.Instance.inventoryManager.chestPanel.blocksRaycasts = false;
                //Reference.Instance.inventoryManager.shopPanel.blocksRaycasts = false;
                Reference.Instance.inventoryManager.craftPanel.blocksRaycasts = true;
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
