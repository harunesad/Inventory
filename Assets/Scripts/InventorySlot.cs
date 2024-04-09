using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public Image itemImage, rarityImage;
    public TextMeshProUGUI quantityText, equippedText;
    public float durability;
    public int level;
    public Armours armour;
    public Weapons weapons;
    public Items items;
    Drag drag;
    [SerializeField] ItemDetails itemDetails;
    [SerializeField] WeaponDetails weaponDetails;
    [SerializeField] ArmourDetails armourDetails;
    [SerializeField] InventoryManager inventoryManager;
    int useClickCount, equipClickCount;
    void Start()
    {
        drag = GetComponentInChildren<Drag>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (armour != null && !drag.drag)
        {
            armourDetails.gameObject.SetActive(true);
            armourDetails.DetailsUpdate(armour.armourName, armour.description, quantityText.text, durability / 100, armour.levelData[level].sellPrice,
                armour.levelData[level].buyPrice, armour.levelData[level].armour, armour.levelData[level].addHealth, rarityImage, itemImage);
        }
        else if (weapons != null && !drag.drag)
        {
            weaponDetails.gameObject.SetActive(true);
            weaponDetails.DetailsUpdate(weapons.weaponName, weapons.description, quantityText.text, durability / 100, weapons.levelData[level].sellPrice,
                weapons.levelData[level].buyPrice, weapons.levelData[level].attack, weapons.levelData[level].attackSpeed, rarityImage, itemImage);
        }
        else if (items != null && !drag.drag)
        {
            itemDetails.gameObject.SetActive(true);
            itemDetails.DetailsUpdate(items.itemName, items.description, quantityText.text, items.maxStock, items.sellPrice, items.buyPrice, rarityImage, itemImage);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        armourDetails.gameObject.SetActive(false);
        weaponDetails.gameObject.SetActive(false);
        itemDetails.gameObject.SetActive(false);
    }

    //Altindaki degiskenler olacak ve equip, drop, use islemleri olacak
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoubleClick()
    {
        if (items != null)
        {
            useClickCount++;
            equipClickCount = 0;
            Debug.Log(useClickCount);
        }
        else if (weapons != null || armour != null)
        {
            equipClickCount++;
            useClickCount = 0;
        }
        else
        {
            useClickCount = 0;
            equipClickCount = 0;
        }
        StartCoroutine(CountReset());
    }
    IEnumerator CountReset()
    {
        if (useClickCount == 2)
        {
            FindObjectOfType<InventoryManager>().use.Use(this);
            quantityText.text = (int.Parse(quantityText.text) - 1).ToString();
            if (int.Parse(quantityText.text) == 0)
            {
                GetComponentInParent<Inventory>().SlotReset(this);
            }
        }
        else if (equipClickCount == 2)
        {
            EquipType.Type type = weapons != null ? weapons.type : armour.type;
            switch (type)
            {
                case EquipType.Type.HeadArmour:
                    inventoryManager.mainInventory.SwitchSlot(this, inventoryManager.headArmour);
                    break;
                case EquipType.Type.ArmArmour:
                    inventoryManager.mainInventory.SwitchSlot(this, inventoryManager.armArmour);
                    break;
                case EquipType.Type.BodyArmour:
                    inventoryManager.mainInventory.SwitchSlot(this, inventoryManager.bodyArmour);
                    break;
                case EquipType.Type.LegArmour:
                    inventoryManager.mainInventory.SwitchSlot(this, inventoryManager.legArmour);
                    break;
                case EquipType.Type.Shield:
                    inventoryManager.mainInventory.SwitchSlot(this, inventoryManager.shield);
                    break;
                case EquipType.Type.Weapon:
                    inventoryManager.mainInventory.SwitchSlot(this, inventoryManager.weapon);
                    break;
                default:
                    break;
            }
        }
        yield return new WaitForSecondsRealtime(.25f);
        useClickCount = 0;
        equipClickCount = 0;
    }
}
