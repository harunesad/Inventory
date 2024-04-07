using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour
{
    public int level;
    public float durability;
    public Weapons weapons;
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
            inventory.WeaponSlotUpdate(null, 1, weapons.weaponImage, weapons.rarityType, level, durability, weapons);
            gameObject.SetActive(false);
        }
    }
}
