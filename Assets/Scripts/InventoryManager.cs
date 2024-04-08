using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] CanvasGroup �nventoryPanel;
    public Inventory mainInventory;
    public UseItem use;
    public CanvasGroup interact;
    //Inventoryler slotlari arasindaki degis tokus
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            �nventoryPanel.alpha = (�nventoryPanel.alpha == 0) ? 1 : 0;
        }
    }
}
