using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    RectTransform rect;
    InventorySlot slot, mySlot;
    DropItem drop;
    UseItem use;
    Vector3 startPos;
    bool isSlot;
    [SerializeField] Inventory inventory;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.position;
        mySlot = GetComponentInParent<InventorySlot>();
    }
    public bool drag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (mySlot.items != null || mySlot.weapons != null || mySlot.armour != null)
        {
            drag = true;
            transform.parent = transform.parent.parent;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (mySlot.items != null || mySlot.weapons != null || mySlot.armour != null)
        {
            rect.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        drag = false;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<InventorySlot>(out InventorySlot slot))
            {
                this.slot = slot;
                isSlot = true;
            }
            else if (result.gameObject.TryGetComponent<DropItem>(out DropItem dropItem))
            {
                drop = dropItem;
            }
            else if (result.gameObject.TryGetComponent<UseItem>(out UseItem useItem))
            {
                use = useItem;
            }
        }
        if (isSlot)
        {
            if (slot.items == null && slot.weapons == null && slot.armour == null)
            {
                if (slot.gameObject.TryGetComponent<EquipItem>(out EquipItem equipItem))
                {
                    if (mySlot.weapons != null && mySlot.weapons.type == equipItem.type)
                    {
                        inventory.WeaponSlotUpdate(slot, 1, mySlot.weapons.weaponImage, mySlot.weapons.rarityType, mySlot.level, mySlot.durability, mySlot.weapons);
                        inventory.SlotReset(mySlot);
                        equipItem.Equip();
                    }
                    else if (mySlot.armour != null && mySlot.armour.type == equipItem.type)
                    {
                        inventory.ArmourSlotUpdate(slot, 1, mySlot.armour.armourImage, mySlot.armour.rarityType, mySlot.level, mySlot.durability, mySlot.armour);
                        inventory.SlotReset(mySlot);
                        equipItem.Equip();
                    }
                }
                else
                {
                    if (mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equip))
                    {
                        equip.Unequip();
                    }
                    if (mySlot.items != null)
                    {
                        inventory.ItemSlotUpdate(slot, int.Parse(mySlot.quantityText.text), mySlot.items.itemImage, mySlot.items.rarityType, mySlot.items);
                        inventory.SlotReset(mySlot);
                    }
                    else if (mySlot.weapons != null)
                    {
                        inventory.WeaponSlotUpdate(slot, 1, mySlot.weapons.weaponImage, mySlot.weapons.rarityType, mySlot.level, mySlot.durability, mySlot.weapons);
                        inventory.SlotReset(mySlot);
                    }
                    else if (mySlot.armour != null)
                    {
                        inventory.ArmourSlotUpdate(slot, 1, mySlot.armour.armourImage, mySlot.armour.rarityType, mySlot.level, mySlot.durability, mySlot.armour);
                        inventory.SlotReset(mySlot);
                    }
                    inventory.SlotReset(mySlot);
                }
            }
            else
            {
                if (slot.gameObject.TryGetComponent<EquipItem>(out EquipItem equipItem) && mySlot.items == null)
                {
                    if (mySlot.weapons != null && mySlot.weapons.type == equipItem.type)
                    {
                        inventory.SwitchSlot(mySlot, slot);
                    }
                    else if (mySlot.armour != null && mySlot.armour.type == equipItem.type)
                    {
                        inventory.SwitchSlot(mySlot, slot);
                    }
                }
                else if (mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equip) && slot.items == null)
                {
                    if (slot.weapons != null && slot.weapons.type == equip.type)
                    {
                        inventory.SwitchSlot(mySlot, slot);
                    }
                    else if (slot.armour != null && slot.armour.type == equip.type)
                    {
                        inventory.SwitchSlot(mySlot, slot);
                    }
                }
                else if (mySlot != slot)
                {
                    inventory.SwitchSlot(mySlot, slot);
                }
            }
        }
        else if (drop && !mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equip))
        {
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position + (Vector3.forward * 2);
            drop.Drop(mySlot, playerPos + (Vector3.up * .5f), int.Parse(mySlot.quantityText.text), mySlot.level, mySlot.durability);
            inventory.SlotReset(mySlot);
        }
        else if (use && mySlot.items != null)
        {
            use.Use(mySlot);
            mySlot.quantityText.text = (int.Parse(mySlot.quantityText.text) - 1).ToString();
            if (int.Parse(mySlot.quantityText.text) == 0)
            {
                inventory.SlotReset(mySlot);
            }
        }
        transform.parent = mySlot.transform;
        rect.position = startPos;
        isSlot = false;
        drop = null;
    }
    //Surukleme islemleri olacak 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
