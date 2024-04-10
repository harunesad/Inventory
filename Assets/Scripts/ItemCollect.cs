using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public Items items;
    public int quantity;
    Inventory inventory;
    UIManager uIManager;
    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>().mainInventory;
        uIManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIManager.InteractActive("Collect");
            if (Input.GetKeyDown(KeyCode.E))
            {
                uIManager.InteractPassive();
                inventory.ItemSlotUpdate(null, quantity, items.itemImage, items.rarityType, items);
                transform.parent.gameObject.SetActive(false);
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
