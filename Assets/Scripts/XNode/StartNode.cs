using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;
using XNodeEditor;

public class StartNode : BaseNode
{
    [Output] public int exit;

    public override string GetString()
    {
        return "Start";
    }
}
