using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Image dialogueBoxAspect;
    public Sprite dreamAspect;
    public Sprite nightmareAspect;
    [Range(0, 0.5f)]
    public float keySpeed;
    public AudioSource audio;
    public AudioClip keySound;
    public Animator animator;
    public GameObject monJoueur;
    CharacterController scriptController;

    private Queue<string> sentences;
    RangeAttribute test;

    public static int dialogueExecutionStatut;
    public int setUpLastDialogueIndex;
    public static int lastDialogueIndex;

    [SerializeField]
    private int showDialogueExecutionStatut;

    // Start
    void Start()
    {
        lastDialogueIndex = setUpLastDialogueIndex;
        sentences = new Queue<string>();
        scriptController = monJoueur.GetComponent<CharacterController>();
    }

    void Update()
    {
        showDialogueExecutionStatut = dialogueExecutionStatut;

        if(scriptController.isDream == true)
        {
            dialogueBoxAspect.sprite = dreamAspect;
        }
        else if(scriptController.isDream == false)
        {
            dialogueBoxAspect.sprite = nightmareAspect;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Start");
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        //Debug.Log("Next");
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        int i = 0;
        i++;
        //Debug.Log(i);

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(LetterByLetter(sentence));
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        scriptController.dialogueHasStarted = false;
        StopAllCoroutines();

        DialogueManager.dialogueExecutionStatut++;
    }

    IEnumerator LetterByLetter (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if(audio != null)
            {
                audio.PlayOneShot(keySound);
            }
            yield return new WaitForSeconds(keySpeed);
        }
    }


}
