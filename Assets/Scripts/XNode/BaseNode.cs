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
        // Ba�l� portlar varsa bu portun ��k�� de�erini hesapla
        if (port.fieldName == "output")
        {
            // Bu �rnekte ��k�� de�eri �rnek bir metin olacak
            return "Dialogue Text";
        }
        return null; // Di�er portlar i�in null d�nebilirsiniz
    }
}