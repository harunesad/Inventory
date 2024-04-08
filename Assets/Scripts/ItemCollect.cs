using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public Items items;
    Inventory inventory;
    public int quantity;
    CanvasGroup interact;
    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>().mainInventory;
        interact = FindObjectOfType<InventoryManager>().interact;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.alpha = 1;
            if (Input.GetKeyDown(KeyCode.E))
            {
                interact.alpha = 0;
                inventory.ItemSlotUpdate(null, quantity, items.itemImage, items.rarityType, items);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact.alpha = 0;
        }
    }
}
