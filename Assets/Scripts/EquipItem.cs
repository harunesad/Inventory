using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public EquipType.Type type;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Equip()
    {
        Debug.Log("Equip");
    }
    public void Unequip()
    {
        Debug.Log("Unequip");
    }
}
