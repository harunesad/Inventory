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
        if (mySlot.items != null)
        {
            var newItem = Instantiate(mySlot.items.prefab, pos, Quaternion.identity);
            newItem.GetComponent<ItemCollect>().quantity = quantity;
        }
        else if (mySlot.weapons != null)
        {
            var newWeapon = Instantiate(mySlot.weapons.prefab, pos, Quaternion.identity);
            newWeapon.GetComponent<WeaponCollect>().level = level;
            newWeapon.GetComponent<WeaponCollect>().durability = durability;
        }
        else if (mySlot.armour != null)
        {
            var newArmour = Instantiate(mySlot.armour.prefab, pos, Quaternion.identity);
            newArmour.GetComponent<ArmourCollect>().level = level;
            newArmour.GetComponent<ArmourCollect>().durability = durability;
        }
    }
}
