using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDialogue : MonoBehaviour
{
    NodeParser dialogue;
    //CanvasGroup speakInteractPanel, speakPanel;
    [SerializeField]
    LayerMask characterLayer;
    void Start()
    {
        dialogue = GetComponent<NodeParser>();
        //speakInteractPanel = referances.instance.speakInteractPanel;
        //speakPanel = referances.instance.speakPanel;

    }
    void Update()
    {
        Collider[] character = Physics.OverlapBox(transform.position + transform.forward * 1.5f, Vector3.one / 2, Quaternion.identity, characterLayer);
        if (character.Length > 0 && Reference.Instance.uIManager.speakPanel.alpha == 0)
        {
            Debug.Log("a");
            Reference.Instance.uIManager.InteractActive("Speak");
            //speakInteractPanel.alpha = 1;
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogue.DialogueStart();
                Reference.Instance.uIManager.speakPanel.alpha = 1;
                Reference.Instance.uIManager.speakPanel.blocksRaycasts = true;
                //speakInteractPanel.alpha = 0;
                Reference.Instance.uIManager.InteractPassive();
            }
        }
        else
        {
            Reference.Instance.uIManager.InteractPassive();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + transform.forward * 1.5f, Vector3.one / 2);
    }
}