using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseNode : Node
{
    public int buttonCount, finishIndex;
    public List<string> buttonName;

    public virtual string GetString()
    {
        return null;
    }
    public virtual Sprite GetSprite()
    {
        return null;
    }
    public override object GetValue(NodePort port)
    {
        // Baðlý portlar varsa bu portun çýkýþ deðerini hesapla
        if (port.fieldName == "output")
        {
            // Bu örnekte çýkýþ deðeri örnek bir metin olacak
            return "Dialogue Text";
        }
        return null; // Diðer portlar için null dönebilirsiniz
    }
}