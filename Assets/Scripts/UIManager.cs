using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup interact;
    [SerializeField] TextMeshProUGUI message;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void InteractActive(string message)
    {
        this.message.text = message;
        interact.alpha = 1;
    }
    public void InteractPassive()
    {
        interact.alpha = 0;
    }
}
