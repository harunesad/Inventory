using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform rect;
    public Image itemImage, rarityImage;
    public TextMeshProUGUI quantityText, equippedText;
    public float durability;
    public int level;
    public Armours armour;
    public Weapons weapons;
    public Items items;
    Drag drag;
    public ItemDetails itemDetails;
    public WeaponDetails weaponDetails;
    public ArmourDetails armourDetails;
    InventoryManager inventoryManager;
    Inventory inventory;
    int useClickCount, equipClickCount;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        drag = GetComponentInChildren<Drag>();
        inventoryManager = Reference.Instance.inventoryManager;
        inventory = GetComponentInParent<Inventory>();
    }
    private void Start()
    {
        if (drag)
        {
            if (drag.TryGetComponent<Button>(out Button button))
            {
                button.onClick.AddListener(SlotSelect);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if ((!inventory.TryGetComponent<CanvasGroup>(out CanvasGroup canvasGroup) || inventory.GetComponent<CanvasGroup>().alpha == 1) && drag)
        {
            inventoryManager.selection.GetComponent<RectTransform>().position = rect.position;
            if (armour && !drag.drag)
            {
                DetailsTransformUpate(armourDetails.GetComponent<RectTransform>());
                armourDetails.gameObject.SetActive(true);
                armourDetails.DetailsUpdate(armour.armourName, armour.description, quantityText.text, durability / 100, armour.levelData[level].sellPrice,
                    armour.levelData[level].buyPrice, armour.levelData[level].armour, armour.levelData[level].addHealth, rarityImage, itemImage);
            }
            else if (weapons && !drag.drag)
            {
                DetailsTransformUpate(weaponDetails.GetComponent<RectTransform>());
                weaponDetails.gameObject.SetActive(true);
                weaponDetails.DetailsUpdate(weapons.weaponName, weapons.description, quantityText.text, durability / 100, weapons.levelData[level].sellPrice,
                    weapons.levelData[level].buyPrice, weapons.levelData[level].attack, weapons.levelData[level].attackSpeed, rarityImage, itemImage);
            }
            else if (items && !drag.drag)
            {
                DetailsTransformUpate(itemDetails.GetComponent<RectTransform>());
                itemDetails.gameObject.SetActive(true);
                itemDetails.DetailsUpdate(items.itemName, items.description, quantityText.text, items.maxStock, items.sellPrice, items.buyPrice, rarityImage, itemImage);
            }
        }
    }
    void DetailsTransformUpate(RectTransform rect)
    {
        Vector3 newRectPos = this.rect.position + (Vector3.right * 300);
        if (newRectPos.x > 900)
        {
            rect.position = this.rect.position + (Vector3.left * 300);
        }
        else
        {
            rect.position = this.rect.position + (Vector3.right * 300);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        armourDetails.gameObject.SetActive(false);
        weaponDetails.gameObject.SetActive(false);
        itemDetails.gameObject.SetActive(false);
    }
    void SlotSelect()
    {
        bool active = !inventory.TryGetComponent<CanvasGroup>(out CanvasGroup canvasGroup) || inventory.GetComponent<CanvasGroup>().alpha == 1;
        if ((items || weapons || armour) && active)
        {
            inventoryManager.selectSlot = this;
            inventoryManager.select.GetComponent<RectTransform>().position = rect.position;
        }
    }
    public void DoubleClick()
    {
        if (items)
        {
            useClickCount++;
            equipClickCount = 0;
        }
        else if (weapons || armour)
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
            inventoryManager.use.Use(this);
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

    void Update()
    {

    }
}
