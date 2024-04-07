using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public Image itemImage, rarityImage;
    public TextMeshProUGUI quantityText, equippedText;
    public float durability;
    public int level;
    public Armours armour;
    public Weapons weapons;
    public Items items;
    [SerializeField] ItemDetails itemDetails;
    [SerializeField] WeaponDetails weaponDetails;
    [SerializeField] ArmourDetails armourDetails;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (armour != null && !GetComponentInChildren<Drag>().drag)
        {
            armourDetails.gameObject.SetActive(true);
            armourDetails.DetailsUpdate(armour.armourName, armour.description, quantityText.text, durability / 100, armour.levelData[level].sellPrice,
                armour.levelData[level].buyPrice, armour.levelData[level].armour, armour.levelData[level].addHealth, rarityImage, itemImage);
        }
        else if (weapons != null && !GetComponentInChildren<Drag>().drag)
        {
            weaponDetails.gameObject.SetActive(true);
            weaponDetails.DetailsUpdate(weapons.weaponName, weapons.description, quantityText.text, durability / 100, weapons.levelData[level].sellPrice,
                weapons.levelData[level].buyPrice, weapons.levelData[level].attack, weapons.levelData[level].attackSpeed, rarityImage, itemImage);
        }
        else if (items != null && !GetComponentInChildren<Drag>().drag)
        {
            itemDetails.gameObject.SetActive(true);
            itemDetails.DetailsUpdate(items.itemName, items.description, quantityText.text, items.maxStock, items.sellPrice, items.buyPrice, rarityImage, itemImage);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        armourDetails.gameObject.SetActive(false);
        weaponDetails.gameObject.SetActive(false);
        itemDetails.gameObject.SetActive(false);
    }

    //Altindaki degiskenler olacak ve equip, drop, use islemleri olacak
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
