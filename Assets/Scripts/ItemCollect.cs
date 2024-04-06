using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public Items items;
    [SerializeField] Inventory inventory;
    [SerializeField] int quantity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.ItemSlotUpdate(null, quantity, items.itemImage, items.rarityType, items);
            gameObject.SetActive(false);
        }
    }
}
