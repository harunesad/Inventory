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
    TaskItems taskItems;
    Vector3 startPos;
    bool isSlot;
    Inventory inventory;
    UIManager uIManager;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.position;
        mySlot = GetComponentInParent<InventorySlot>();
        inventory = GetComponentInParent<Inventory>();
        uIManager = Reference.Instance.uIManager;
    }
    public bool drag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (mySlot.items || mySlot.weapons || mySlot.armour)
        {
            mySlot.armourDetails.gameObject.SetActive(false);
            mySlot.weaponDetails.gameObject.SetActive(false);
            mySlot.itemDetails.gameObject.SetActive(false);
            drag = true;
            transform.parent = transform.parent.parent.parent;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (mySlot.items || mySlot.weapons || mySlot.armour)
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
            else if (result.gameObject == Reference.Instance.uIManager.taskItems.taskSlot)
            {
                taskItems = Reference.Instance.uIManager.taskItems;
            }
        }
        bool slotState = slot;
        bool shopState = false;
        if (slotState)
        {
            shopState = !mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equipItem1) && !slot.gameObject.TryGetComponent<EquipItem>(out EquipItem equipItem2)
    && !(inventory.shop && slot.GetComponentInParent<Inventory>().shop) && !(!inventory.shop && !slot.GetComponentInParent<Inventory>().shop);
        }
        if (isSlot && shopState)
        {
            int buyItem = 0, buyWeapon = 0, buyArmour = 0, sellItem = 0, sellWeapon = 0, sellArmour = 0;
            if (mySlot.items)
            {
                int quantity = 0;
                if (slot.items == mySlot.items)
                {
                    quantity = int.Parse(mySlot.quantityText.text) + int.Parse(slot.quantityText.text) - slot.items.maxStock;
                }

                quantity = quantity <= 0 ? int.Parse(mySlot.quantityText.text) : int.Parse(mySlot.quantityText.text) - quantity;
                int buySell = (inventory.shop ? 1 * mySlot.items.buyPrice : -1 * mySlot.items.sellPrice) * quantity;
                buyItem = buySell;
            }
            else if (mySlot.weapons)
            {
                int buySell = inventory.shop ? 1 * mySlot.weapons.levelData[mySlot.level].buyPrice : -1 * mySlot.weapons.levelData[mySlot.level].sellPrice;
                buyWeapon = buySell;
            }
            else if (mySlot.armour)
            {
                int buySell = inventory.shop ? 1 * mySlot.armour.levelData[mySlot.level].buyPrice : -1 * mySlot.armour.levelData[mySlot.level].sellPrice;
                buyArmour = buySell;
            }
            if (slot.items && !mySlot.items)
            {
                int quantity = 0;
                quantity = int.Parse(slot.quantityText.text);
                int buySell = (inventory.shop ? -1 * slot.items.sellPrice : 1 * slot.items.buyPrice) * quantity;
                sellItem = buySell;
            }
            else if (slot.weapons)
            {
                int buySell = inventory.shop ? -1 * slot.weapons.levelData[mySlot.level].sellPrice : 1 * slot.weapons.levelData[mySlot.level].buyPrice;
                sellWeapon = buySell;
            }
            else if (slot.armour)
            {
                int buySell = inventory.shop ? -1 * slot.armour.levelData[mySlot.level].sellPrice : 1 * slot.armour.levelData[mySlot.level].buyPrice;
                sellArmour = buySell;
            }
            int totalCost = buyItem + buyWeapon + buyArmour + sellItem + sellWeapon + sellArmour;
            if (uIManager.coin >= totalCost)
            {
                uIManager.coin -= totalCost;
                uIManager.CoinUpdate();
                inventory.SwitchSlot(mySlot, slot);
                Reference.Instance.uIManager.shopSystem.ShopItemsUpdate();
            }
        }
        else if (isSlot && slot.GetComponentInChildren<Drag>() && !inventory.shop && !slot.GetComponentInParent<Inventory>().shop)
        {
            if (slot.gameObject.TryGetComponent<EquipItem>(out EquipItem equipItem))
            {
                if (mySlot.weapons && mySlot.weapons.type == equipItem.type)
                {
                    inventory.SwitchSlot(mySlot, slot);
                }
                else if (mySlot.armour && mySlot.armour.type == equipItem.type)
                {
                    inventory.SwitchSlot(mySlot, slot);
                }
            }
            else if (mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equip))
            {
                if (slot.weapons && slot.weapons.type == equip.type)
                {
                    inventory.SwitchSlot(mySlot, slot);
                }
                else if (slot.armour && slot.armour.type == equip.type)
                {
                    inventory.SwitchSlot(mySlot, slot);
                }
                else if (!slot.items && !slot.weapons && !slot.armour)
                {
                    inventory.SwitchSlot(mySlot, slot);
                }
            }
            else
            {
                inventory.SwitchSlot(mySlot, slot);
            }
            if (Reference.Instance.inventoryManager.chestPanel.alpha == 1)
            {
                Reference.Instance.uIManager.chestSystem.ChestItemsUpdate();
            }
            //else if (Reference.Instance.inventoryManager.shopPanel.alpha == 1)
            //{
            //    Reference.Instance.uIManager.shopSystem.ShopItemsUpdate();
            //}
            else if (Reference.Instance.inventoryManager.craftPanel.alpha == 1)
            {
                Reference.Instance.uIManager.craftControl.Craftable();
            }
        }
        else if (drop && !mySlot.gameObject.TryGetComponent<EquipItem>(out EquipItem equip) && inventory.isMine)
        {
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position + (Vector3.forward * 3);
            drop.Drop(mySlot, playerPos + (Vector3.up * .5f), int.Parse(mySlot.quantityText.text), mySlot.level, mySlot.durability);
            inventory.SlotReset(mySlot);
        }
        else if (use && mySlot.items && inventory.isMine)
        {
            use.Use(mySlot);
            mySlot.quantityText.text = (int.Parse(mySlot.quantityText.text) - 1).ToString();
            if (int.Parse(mySlot.quantityText.text) == 0)
            {
                inventory.SlotReset(mySlot);
            }
        }
        else if (taskItems && taskItems.taskInventoryStart.isStarted)
        {
            //taskItems.TaskUpdate(mySlot);
            Debug.Log("aaaa");
            if (taskItems.TaskUpdate(mySlot))
            {
                inventory.SlotReset(mySlot);
            }
        }
        transform.parent = mySlot.transform;
        rect.position = startPos;
        isSlot = false;
        drop = null;
        use = null;
        taskItems = null;
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
