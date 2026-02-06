using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Serializable]
    struct DialogueData
    {
        [TextArea(1, 5)] public string dialogue;
        public Sprite character;    // The character associated with the dialogue, if needed
    }
    [SerializeField] private int index;
    [SerializeField, Range(0,0.4f)] private float delay;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueImage;
    [SerializeField] private DialogueData[] dialogues;
    TextMeshProUGUI text;

    private void Start()
    {
        text = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        StartDialogue(); // Start the dialogue immediately when the scene starts
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {if (text.text == dialogues[index].dialogue)
            {
                Next();
            }
            else
            {
                StopAllCoroutines();
                text.text = dialogues[index].dialogue;
            }

        }
    }
    private void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        index = 0;
        StartCoroutine(Show());
        //Time.timeScale = 0f;
    }
    private IEnumerator Show()
    {
        text.text = string.Empty;
        dialogueImage.GetComponent<SpriteRenderer>().sprite = dialogues[index].character;
        foreach (char ch in dialogues[index].dialogue)
        {
            text.text += ch;
            yield return new WaitForSecondsRealtime(delay);
        }
    }
    private void Next()
    {
        index++;
        if (index < dialogues.Length)
        {
            StartCoroutine(Show());
        }
        else
        {
            dialoguePanel.SetActive(false);
            //Time.timeScale = 1f;
        }
    }
}
