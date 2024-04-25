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
    bool nextDialogue = false, dialogueStart;
    Transform buttonPanel;
    int dialogueIndex;
    [SerializeField] int finishIndex;
    [SerializeField] float typingSpeed = .04f;
    public UnityEvent taskEvent;
    [SerializeField] int buttonCount;

    private void Start()
    {
        taskSystem = GetComponentInChildren<TaskSystem>();
        buttonPanel = Reference.Instance.uIManager.buttonPanel;
        speakerImage = Reference.Instance.uIManager.speakerImage;
        speaker = Reference.Instance.uIManager.speakerName;
        dialogue = Reference.Instance.uIManager.displayLine;
        //for (int i = 0; i < buttonPanel.childCount; i++)
        //{
        //    int j = i;
        //    buttonPanel.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate { NextDialogue(j); });
        //}
    }
    private void Update()
    {
        if (dialogueStart)
        {
            bool next = false;
            int finishIndex = 0;
            for (int i = 0; i < buttonPanel.childCount; i++)
            {
                if (buttonPanel.GetChild(i).gameObject.activeSelf)
                {
                    next = true;
                    finishIndex = i;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                buttonPanel.GetChild(buttonCount).GetComponent<Image>().color = buttonPanel.GetChild(buttonCount).GetComponent<Button>().colors.normalColor;
                buttonCount++;
                buttonCount = buttonCount > finishIndex ? 0 : buttonCount;
                buttonPanel.GetChild(buttonCount).GetComponent<Image>().color = buttonPanel.GetChild(buttonCount).GetComponent<Button>().colors.pressedColor;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                buttonPanel.GetChild(buttonCount).GetComponent<Image>().color = buttonPanel.GetChild(buttonCount).GetComponent<Button>().colors.normalColor;
                buttonCount--;
                buttonCount = buttonCount < 0 ? finishIndex : buttonCount;
                buttonPanel.GetChild(buttonCount).GetComponent<Image>().color = buttonPanel.GetChild(buttonCount).GetComponent<Button>().colors.pressedColor;
            }
            if (Input.GetKeyDown(KeyCode.Return) && next)
            {
                dialogueIndex = buttonCount;
                nextDialogue = true;
            }
        }
    }
    //void NextDialogue(int index)
    //{
    //    dialogueIndex = index;
    //    nextDialogue = true;
    //}
    public void DialogueStart()
    {
        Time.timeScale = 0;
        foreach (BaseNode baseNode in graph[taskSystem.currentGraph].nodes)
        {
            if (baseNode.GetString() == "Start")
            {
                dialogueStart = true;
                graph[taskSystem.currentGraph].current = baseNode;
                break;
            }
        }
        parser = StartCoroutine(ParseNode());
    }

    IEnumerator ParseNode()
    {
        buttonPanel.GetChild(buttonCount).GetComponent<Image>().color = buttonPanel.GetChild(buttonCount).GetComponent<Button>().colors.pressedColor;
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
            dialogueStart = false;
            Time.timeScale = 1;
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
            for (int i = 0; i < buttonPanel.childCount; i++)
            {
                buttonPanel.GetChild(i).GetComponent<Image>().color = buttonPanel.GetChild(i).GetComponent<Button>().colors.normalColor;
            }
            nextDialogue = false;
            buttonCount = 0;
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                skip = true;
                dialogue.text = line;
            }
            else if (dialogue.text.Length == line.ToCharArray().Length)
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