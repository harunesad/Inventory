using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public Items items;
    Inventory inventory;
    public int quantity;
    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>().mainInventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inventory.ItemSlotUpdate(null, quantity, items.itemImage, items.rarityType, items);
            gameObject.SetActive(false);
        }
    }
}
