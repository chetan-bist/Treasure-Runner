using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;
    private player_movement playerScript;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        sentences = new Queue<string>();
    }

    void Start()
    {
        // गेममा भएको Player को script पत्ता लगाउने
        playerScript = FindFirstObjectByType<player_movement>();
    }

    public void StartDialogue(string name, string[] dialogueLines)
    {
        // १. डायलग बक्स देखाउने
        dialoguePanel.SetActive(true);
        nameText.text = name;

        // २. प्लेयरलाई हिँड्नबाट रोक्ने, फिजिक्स फ्रिज गर्ने र एनिमेसन पनि रोक्ने
        if (playerScript != null)
        {
            playerScript.StopMove(); // तपाईँको script को Stop फङ्सन कल गर्ने

            // [एनिमेसन फिक्स]: प्लेयरको Animator पत्ता लगाएर 'isruning' लाई false बनाउने
            Animator playerAnim = playerScript.GetComponent<Animator>();
            if (playerAnim != null)
            {
                playerAnim.SetBool("isruning", false); // हिँड्ने एनिमेसन बन्द गर्छ
                // यदि 'isjumping' पनि अन हुन सक्ने डर छ भने यसलाई पनि false गर्न सक्नुहुन्छ:
                playerAnim.SetBool("isjumping", false); 
            }

            // प्लेयरको Rigidbody2D गति जिरो बनाउने
            Rigidbody2D playerRB = playerScript.GetComponent<Rigidbody2D>();
            if (playerRB != null)
            {
                playerRB.linearVelocity = Vector2.zero; 
            }

            playerScript.enabled = false; // स्क्रिप्ट बन्द गर्ने
        }

        sentences.Clear();

        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        // Unity 6 को TextMeshPro Refresh फिक्स
        dialogueText.ForceMeshUpdate(); 
    }

    void EndDialogue()
    {
        // १. डायलग बक्स बन्द गर्ने
        dialoguePanel.SetActive(false);

        // पुराना अक्षरहरू सफा गर्ने
        nameText.text = "";
        dialogueText.text = "";

        // २. प्लेयरलाई फेरि हिँड्न दिने
        if (playerScript != null)
        {
            playerScript.enabled = true;
        }
    }
}