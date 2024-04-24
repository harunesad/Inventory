using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;
using TMPro;
using UnityEngine.Events;

public class NodeParser : MonoBehaviour
{
    public List<DialogueGraph> graph;
    Coroutine parser, displayLine;
    TMP_Text speaker, dialogue;
    Image speakerImage;
    TaskSystem taskSystem;
    bool nextDialogue = false;
    Transform buttonPanel;
    int dialogueIndex;
    [SerializeField] int finishIndex;
    [SerializeField] float typingSpeed = .04f;
    public UnityEvent taskEvent;

    private void Start()
    {
        taskSystem = GetComponentInChildren<TaskSystem>();
        buttonPanel = Reference.Instance.uIManager.buttonPanel;
        speakerImage = Reference.Instance.uIManager.speakerImage;
        speaker = Reference.Instance.uIManager.speakerName;
        dialogue = Reference.Instance.uIManager.displayLine;
        for (int i = 0; i < buttonPanel.childCount; i++)
        {
            int j = i;
            buttonPanel.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate { NextDialogue(j); });
        }
    }
    void NextDialogue(int index)
    {
        dialogueIndex = index;
        nextDialogue = true;
    }
    public void DialogueStart()
    {
        foreach (BaseNode baseNode in graph[taskSystem.currentGraph].nodes)
        {
            if (baseNode.GetString() == "Start")
            {
                graph[taskSystem.currentGraph].current = baseNode;
                break;
            }
        }
        parser = StartCoroutine(ParseNode());
    }

    IEnumerator ParseNode()
    {
        BaseNode baseNode = graph[taskSystem.currentGraph].current;
        string data = baseNode.GetString();
        string[] dataParts = data.Split("/");
        if (dataParts[0] == "Start")
        {
            NextNode("exit");
        }
        if (dataParts[0] == "Finish")
        {
            if (baseNode.finishIndex == finishIndex)
            {
                taskEvent.Invoke();
            }
            dialogueIndex = 0;
            Reference.Instance.uIManager.speakPanel.alpha = 0;
            Reference.Instance.uIManager.speakPanel.blocksRaycasts = false;
        }
        if (dataParts[0] == "DialogueNode")
        {
            for (int i = 0; i < buttonPanel.childCount; i++)
            {
                buttonPanel.GetChild(i).gameObject.SetActive(false);
            }
            speaker.text = dataParts[1];
            if (displayLine != null)
            {
                StopCoroutine(displayLine);
            }
            displayLine = StartCoroutine(DisplayeLine(dataParts[2], baseNode));
            speakerImage.sprite = baseNode.GetSprite();
            yield return new WaitUntil(() => nextDialogue);
            nextDialogue = false;
            NextNode("exit" + dialogueIndex);


        }
    }
    IEnumerator DisplayeLine(string line, BaseNode baseNode)
    {
        dialogue.text = "";

        bool skip = false;
        foreach (char letter in line.ToCharArray())
        {
            dialogue.text += letter;
            if (Input.GetMouseButtonDown(0))
            {
                skip = true;
                dialogue.text = line;
            }
            if (dialogue.text.Length == line.ToCharArray().Length)
            {
                for (int i = 0; i < baseNode.buttonCount; i++)
                {
                    buttonPanel.GetChild(i).gameObject.SetActive(true);
                    buttonPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = baseNode.buttonName[i];
                }
            }
            if (skip)
            {
                break;
            }
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
    void NextNode(string fieldName)
    {
        if (parser != null)
        {
            StopCoroutine(parser);
            parser = null;
        }
        foreach (NodePort nodePort in graph[taskSystem.currentGraph].current.Ports)
        {
            if (nodePort.fieldName == fieldName)
            {
                graph[taskSystem.currentGraph].current = nodePort.Connection.node as BaseNode;
                break;
            }
        }
        parser = StartCoroutine(ParseNode());
    }
}