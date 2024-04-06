using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    RectTransform rect;
    InventorySlot slot, mySlot;
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
        drag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
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
        }
        if (isSlot)
        {
            if (slot.items == null && slot.weapons == null && slot.armour == null)
            {
                if (mySlot.items != null)
                {
                    inventory.ItemSlotUpdate(slot, int.Parse(mySlot.quantityText.text), mySlot.items.itemImage, mySlot.items.rarityType, mySlot.items);
                }
                else if (mySlot.weapons != null)
                {
                    inventory.WeaponSlotUpdate(slot, 1, mySlot.weapons.weaponImage, mySlot.weapons.rarityType, mySlot.level, mySlot.durability, mySlot.weapons);
                }
                else if (mySlot.armour != null)
                {
                    inventory.ArmourSlotUpdate(slot, 1, mySlot.armour.armourImage, mySlot.armour.rarityType, mySlot.level, mySlot.durability, mySlot.armour);
                }
                inventory.SlotReset(mySlot);
            }
            Debug.Log(eventData.pointerDrag.name + " " + eventData.pointerEnter.name);
        }
        rect.position = startPos;
        //if (eventData.)
        //{

        //}
        isSlot = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.name + " " + eventData.pointerEnter.name);
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
