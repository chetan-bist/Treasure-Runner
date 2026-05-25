using UnityEngine;
using UnityEngine.SceneManagement; // Scene फेर्न यो चाहिन्छ

public class LevelController : MonoBehaviour
{
    public GameObject winPanel; // Unity बाट WinPanel लाई यहाँ तान्नुहोस्

    // जब प्लेयर झण्डामा ठोकिन्छ, यो फङ्सन चल्छ
    public void ShowWinMenu()
    {
        winPanel.SetActive(true); // प्यानल देखाउने
        Time.timeScale = 0f;      // गेम रोक्ने
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // गेम सुरुमा अनपज गर्ने (नत्र अर्को लेभल पनि रोकिएकै हुन्छ)
        // अर्को लेभल लोड गर्ने (Build Settings को आधारमा)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitToMenu()
    {
        Debug.Log("Game matches quitting...");
        Time.timeScale = 1f;
        // यदि Main Menu छ भने त्यसको नाम लेख्ने, नत्र गेम बन्द गर्ने
        Application.Quit(); 
    }
}
