using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] CanvasGroup ýnventoryPanel, shopPanel;
    public Inventory mainInventory;
    public UseItem use;
    public CanvasGroup interact;
    public InventorySlot headArmour, armArmour, bodyArmour, legArmour, shield, weapon;
    public GameObject select, selection;
    public InventorySlot selectSlot;
    [SerializeField] List<InventoryStart> inventoryStart;
    Vector3 selectPos, selectionPos;
    //Inventoryler slotlari arasindaki degis tokus
    // Start is called before the first frame update
    void Start()
    {
        selectPos = select.GetComponent<RectTransform>().position;
        selectionPos = selection.GetComponent<RectTransform>().position;
        Invoke("WaitStart", .5f);
    }
    void WaitStart()
    {
        for (int i = 0; i < inventoryStart.Count; i++)
        {
            InventorySlot mySlot = mainInventory.slots[inventoryStart[i].index];
            if (inventoryStart[i].items != null)
            {
                mainInventory.ItemSlotUpdate(mySlot, inventoryStart[i].quantity, inventoryStart[i].items.itemImage, inventoryStart[i].items.rarityType,
                    inventoryStart[i].items);
            }
            else if (inventoryStart[i].weapons != null)
            {
                mainInventory.WeaponSlotUpdate(mySlot, 1, inventoryStart[i].weapons.weaponImage, inventoryStart[i].weapons.rarityType, inventoryStart[i].level
                    , inventoryStart[i].durability, inventoryStart[i].weapons);
                if (inventoryStart[i].equip)
                {
                    mainInventory.SwitchSlot(mySlot, weapon);
                }
            }
            else if (inventoryStart[i].armours != null)
            {
                mainInventory.ArmourSlotUpdate(mySlot, 1, inventoryStart[i].armours.armourImage, inventoryStart[i].armours.rarityType, inventoryStart[i].level,
                    inventoryStart[i].durability, inventoryStart[i].armours);
                if (inventoryStart[i].equip)
                {
                    switch (mySlot.armour.type)
                    {
                        case EquipType.Type.HeadArmour:
                            mainInventory.SwitchSlot(mySlot, headArmour);
                            break;
                        case EquipType.Type.ArmArmour:
                            mainInventory.SwitchSlot(mySlot, armArmour);
                            break;
                        case EquipType.Type.BodyArmour:
                            mainInventory.SwitchSlot(mySlot, bodyArmour);
                            break;
                        case EquipType.Type.LegArmour:
                            mainInventory.SwitchSlot(mySlot, legArmour);
                            break;
                        case EquipType.Type.Shield:
                            mainInventory.SwitchSlot(mySlot, shield);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            select.GetComponent<RectTransform>().position = selectPos;
            selection.GetComponent<RectTransform>().position = selectionPos;
            ýnventoryPanel.alpha = (ýnventoryPanel.alpha == 0) ? 1 : 0;
            shopPanel.alpha = 0;
        }
    }
}
[System.Serializable]
public class InventoryStart
{
    public Items items;
    public Weapons weapons;
    public Armours armours;
    public int index;
    public bool equip;
    public float durability;
    public int level;
    public int quantity;
}
