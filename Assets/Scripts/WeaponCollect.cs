using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour
{
    public int level;
    public float durability;
    public Weapons weapons;
    Inventory inventory;
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
                inventory.WeaponSlotUpdate(null, 1, weapons.weaponImage, weapons.rarityType, level, durability, weapons);
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
