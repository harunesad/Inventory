using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourCollect : MonoBehaviour
{
    public int level;
    public float durability;
    public Armours armour;
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
                uIManager.NotificationActive(armour.name + " collect", armour.armourImage);
                uIManager.InteractPassive();
                inventory.ArmourSlotUpdate(null, 1, armour.armourImage, armour.rarityType, level, durability, armour);
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
