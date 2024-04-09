using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour
{
    public int level;
    public float durability;
    public Weapons weapons;
    Inventory inventory;
    [SerializeField] UIManager uIManager;
    void Start()
    {
        inventory = FindObjectOfType<InventoryManager>().mainInventory;
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
                inventory.WeaponSlotUpdate(null, 1, weapons.weaponImage, weapons.rarityType, level, durability, weapons);
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
