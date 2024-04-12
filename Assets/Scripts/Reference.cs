using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    private static Reference _instance;

    public static Reference Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("Reference");
                _instance = singletonObject.AddComponent<Reference>();
            }

            return _instance;
        }
    }
    public UIManager uIManager;
    public InventoryManager inventoryManager;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
