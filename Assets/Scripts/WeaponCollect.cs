using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollect : MonoBehaviour
{
    public int level;
    public float durability;
    public Weapons weapons;
    [SerializeField] Inventory inventory;
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
            inventory.WeaponSlotUpdate(null, 1, weapons.weaponImage, weapons.rarityType, level, durability, weapons);
            gameObject.SetActive(false);
        }
    }
}
