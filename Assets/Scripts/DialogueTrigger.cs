using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    [SerializeField]
    string id;

    CharacterController scriptController;
    SpeechData data;

    private void Awake()
    {

    }

    private void Update()
    {
        data = Resources.Load<SpeechData>("Dialogues/" + (id + DialogueManager.dialogueExecutionStatut));

        dialogue.name = data.name + " :";

        if (GameLanguage.lang == Language.french && dialogue.sentences != data.speechFR)
        {
            dialogue.sentences = data.speechFR;
        }
        else if(GameLanguage.lang == Language.english && dialogue.sentences != data.speechGB)
        {
            dialogue.sentences = data.speechGB;
        }
    }


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject myPlayer = collision.gameObject;
            scriptController = myPlayer.GetComponent<CharacterController>();
            scriptController.dialogueTriggerObject = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject myPlayer = collision.gameObject;
            scriptController = myPlayer.GetComponent<CharacterController>();
            scriptController.dialogueTriggerObject = null;
        }
    }

}
