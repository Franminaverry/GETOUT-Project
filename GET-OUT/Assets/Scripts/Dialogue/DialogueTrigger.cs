using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;
    [Header("Ink JSON first conversation")]
    [SerializeField] private TextAsset firstConvo;

    [Header("Ink JSON first conversation")]
    [SerializeField] private TextAsset interactConvo;


    private bool hasAlreadyTalked;
    [SerializeField] private Transform lookat;

    private void Awake() 
    {
        playerInRange = false;
        hasAlreadyTalked = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().isDialoguePlaying && !hasAlreadyTalked)
        {
            hasAlreadyTalked = true;
            DialogueManager.GetInstance().EnterDialogueMode(firstConvo);
        }
        else if (Input.GetKeyUp(KeyCode.E ) && playerInRange && !DialogueManager.GetInstance().isDialoguePlaying && hasAlreadyTalked)
        {
            DialogueManager.GetInstance().EnterDialogueMode(interactConvo);
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.gameObject.GetComponent<FirstPersonController>();
            playerInRange = true;
            
            if (!hasAlreadyTalked)
            {
                player.StartRotation(lookat);
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
         if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
