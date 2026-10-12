using System;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{
    [SerializeField]
    private Dialogue _currentDialogue;

    [SerializeField]
    DialogueManager _dialogueManager;
    [SerializeField]
    Player_Interaction _playerInteraction;

    private void Start()
    {
        _dialogueManager = FindAnyObjectByType<DialogueManager>();
        _playerInteraction = FindAnyObjectByType<Player_Interaction>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            //tell the dialogue manager this is the current dialogue to use if interacted with. 
            _dialogueManager.currentDialogue = _currentDialogue;
            _playerInteraction.SetCurrentNPC(this);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //tell the dialogue manager this is the current dialogue to use if interacted with. 
            _dialogueManager.currentDialogue = null;
            _playerInteraction.SetCurrentNPC(this);
        }
    }

    public void Interact()
    {
        //start dialogue with this NPC 
        _dialogueManager.StartDialogue();
    }
}
