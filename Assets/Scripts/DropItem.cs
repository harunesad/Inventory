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
    public void Drop(Items item, Weapons weapons, Armours armours, Vector3 pos, int quantity, int level, float durability)
    {
        if (item != null)
        {
            var newItem = Instantiate(item.prefab, pos, Quaternion.identity);
            newItem.GetComponent<ItemCollect>().quantity = quantity;
        }
        else if (weapons != null)
        {
            var newWeapon = Instantiate(weapons.prefab, pos, Quaternion.identity);
            newWeapon.GetComponent<WeaponCollect>().level = level;
            newWeapon.GetComponent<WeaponCollect>().durability = durability;
        }
        else if (armours != null)
        {
            var newArmour = Instantiate(armours.prefab, pos, Quaternion.identity);
            newArmour.GetComponent<ArmourCollect>().level = level;
            newArmour.GetComponent<ArmourCollect>().durability = durability;
        }
    }
}
