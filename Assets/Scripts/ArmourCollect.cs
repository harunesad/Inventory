using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourCollect : MonoBehaviour
{
    public int level;
    public float durability;
    public Armours armour;
    Inventory inventory;
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
            inventory.ArmourSlotUpdate(null, 1, armour.armourImage, armour.rarityType, level, durability, armour);
            gameObject.SetActive(false);
        }
    }
}
