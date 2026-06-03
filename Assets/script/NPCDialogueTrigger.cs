using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public string npcName;
    [TextArea(3, 5)]
    public string[] dialogueLines;

    private bool hasSpoken = false; // गेममा एक पटक मात्र डायलग आओस् भन्नका लागि

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // यदि प्लेयर ठोक्कियो र पहिले कुरा भएको छैन भने डायलग सुरु गर्ने
        if (collision.CompareTag("Player") && !hasSpoken)
        {
            DialogueManager.instance.StartDialogue(npcName, dialogueLines);
            hasSpoken = true; 
        }
    }
}