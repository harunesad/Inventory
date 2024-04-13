using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Drop(InventorySlot mySlot, Vector3 pos, int quantity, int level, float durability)
    {
        if (mySlot.items)
        {
            var newItem = Instantiate(mySlot.items.prefab, pos, Quaternion.identity);
            newItem.GetComponentInChildren<ItemCollect>().quantity = quantity;
        }
        else if (mySlot.weapons)
        {
            var newWeapon = Instantiate(mySlot.weapons.prefab, pos, Quaternion.identity);
            newWeapon.GetComponentInChildren<WeaponCollect>().level = level;
            newWeapon.GetComponentInChildren<WeaponCollect>().durability = durability;
        }
        else if (mySlot.armour)
        {
            var newArmour = Instantiate(mySlot.armour.prefab, pos, Quaternion.identity);
            newArmour.GetComponentInChildren<ArmourCollect>().level = level;
            newArmour.GetComponentInChildren<ArmourCollect>().durability = durability;
        }
    }
}
