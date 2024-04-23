using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;

public class DialogueNode : BaseNode
{
    public string buttonLine;
    [Input] public int entry;
    //[Output] public int exit0, exit1, exit2, exit3;
    public string speakerName;
    public string dialogueLine;
    public Sprite sprite;
    public override string GetString()
    {
        return "DialogueNode/" + speakerName + "/" + dialogueLine;
    }
    public override Sprite GetSprite()
    {
        return sprite;
    }
}
[CustomNodeEditor(typeof(DialogueNode))]
public class DialogueGraphView : NodeEditor
{
    [SerializeField] DialogueGraph dialogue;
    List<string> outputName = new List<string>();
    public override void OnBodyGUI()
    {
        serializedObject.Update();
        DialogueNode node = target as DialogueNode;

        base.OnBodyGUI();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Choise") && node.buttonLine != "")
        {
            node.buttonCount++;
            node.buttonName.Add(node.buttonLine);
            node.AddDynamicOutput(typeof(int), Node.ConnectionType.Multiple, Node.TypeConstraint.None, "exit" + outputName.Count);
            outputName.Add("exit" + outputName.Count);
        }
        else if (GUILayout.Button("Remove Choise") && outputName.Count > 0)
        {
            node.buttonCount--;
            node.buttonName.Remove(node.buttonName[node.buttonName.Count - 1]);
            node.RemoveDynamicPort(outputName[outputName.Count - 1]);
            outputName.Remove(outputName[outputName.Count - 1]);
        }
        GUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();
    }
}