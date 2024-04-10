using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    //UIManager uIManager;
    //InventorySlot mySlot;
    public EquipType.Type type;
    void Start()
    {
        //uIManager = GetComponentInParent<UIManager>();
        //mySlot = GetComponent<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Equip()
    {
        //if (mySlot.weapons != null)
        //{
        //    uIManager.NotificationActive(mySlot.weapons.name + " equip", mySlot.weapons.weaponImage);
        //}
        //else if (mySlot.armour != null)
        //{
        //    uIManager.NotificationActive(mySlot.armour.name + " equip", mySlot.armour.armourImage);
        //}
        Debug.Log("Equip");
    }
    public void Unequip()
    {
        Debug.Log("Unequip");
    }
}
