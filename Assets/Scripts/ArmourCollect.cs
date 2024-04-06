using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourCollect : MonoBehaviour
{
    public int level;
    public float durability;
    public Armours armour;
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
            inventory.ArmourSlotUpdate(1, armour.armourImage, armour.rarityType, level, durability, armour);
            gameObject.SetActive(false);
        }
    }
}
