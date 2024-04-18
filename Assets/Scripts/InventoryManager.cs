using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public CanvasGroup inventoryPanel;
    public CanvasGroup shopPanel, chestPanel, craftPanel, taskPanel, taskEmptyPanel;
    public Inventory mainInventory, chestInventory, shopInventory, craftInventory;
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
            if (inventoryStart[i].items)
            {
                mainInventory.ItemSlotUpdate(mySlot, inventoryStart[i].quantity, inventoryStart[i].items.itemImage, inventoryStart[i].items.rarityType,
                    inventoryStart[i].items);
            }
            else if (inventoryStart[i].weapons)
            {
                mainInventory.WeaponSlotUpdate(mySlot, 1, inventoryStart[i].weapons.weaponImage, inventoryStart[i].weapons.rarityType, inventoryStart[i].level
                    , inventoryStart[i].durability, inventoryStart[i].weapons);
                if (inventoryStart[i].equip)
                {
                    mainInventory.SwitchSlot(mySlot, weapon);
                }
            }
            else if (inventoryStart[i].armours)
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
    public void InventoryItemsUpdate()
    {
        for (int i = 0; i < mainInventory.slots.Count; i++)
        {
            if (mainInventory.slots[i].weapons || mainInventory.slots[i].armour)
            {
                inventoryStart[i].weapons = mainInventory.slots[i].weapons ? mainInventory.slots[i].weapons : null;
                inventoryStart[i].armours = mainInventory.slots[i].armour ? mainInventory.slots[i].armour : null;
                inventoryStart[i].durability = mainInventory.slots[i].durability;
                inventoryStart[i].level = mainInventory.slots[i].level;
                inventoryStart[i].items = null;
            }
            else if (mainInventory.slots[i].items)
            {
                inventoryStart[i].items = mainInventory.slots[i].items;
                inventoryStart[i].quantity = int.Parse(mainInventory.slots[i].quantityText.text);
                inventoryStart[i].weapons = null;
                inventoryStart[i].armours = null;
            }
            else
            {
                inventoryStart[i].weapons = null;
                inventoryStart[i].armours = null;
                inventoryStart[i].items = null;
            }
        }
        for (int i = mainInventory.slots.Count; i < inventoryStart.Count; i++)
        {
            inventoryStart[i].weapons = null;
            inventoryStart[i].armours = null;
            inventoryStart[i].items = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            craftPanel.GetComponentInChildren<CraftControl>().CraftSlotUpdate();
            select.GetComponent<RectTransform>().position = selectPos;
            selection.GetComponent<RectTransform>().position = selectionPos;
            shopPanel.blocksRaycasts = false;
            chestPanel.blocksRaycasts = false;
            craftPanel.blocksRaycasts = false;
            taskPanel.blocksRaycasts = false;
            shopPanel.alpha = 0;
            chestPanel.alpha = 0;
            craftPanel.alpha = 0;
            taskPanel.alpha = 0;
            taskEmptyPanel.gameObject.SetActive(false);
            inventoryPanel.alpha = (inventoryPanel.alpha == 0) ? 1 : 0;
        }
        Application.quitting += InventoryItemsUpdate;
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
