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
        inventory = Reference.Instance.inventoryManager.mainInventory;
        uIManager = Reference.Instance.uIManager;
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
                bool collectable = false;
                for (int i = 0; i < inventory.slots.Count; i++)
                {
                    if (!inventory.slots[i].items && !inventory.slots[i].weapons && !inventory.slots[i].armour)
                    {
                        collectable = true;
                    }
                }
                if (collectable)
                {
                    uIManager.NotificationActive(items.name + " collect", items.itemImage, false, false);
                    uIManager.InteractPassive();
                    inventory.ItemSlotUpdate(null, quantity, items.itemImage, items.rarityType, items);
                    transform.parent.gameObject.SetActive(false);
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
