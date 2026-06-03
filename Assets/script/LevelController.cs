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

        // --- नयाँ लेभल अनलक गर्ने कोड ---
        string sceneName = SceneManager.GetActiveScene().name;
        string levelNumberString = sceneName.Replace("level", ""); 
        
        int currentLevel;
        if (int.TryParse(levelNumberString, out currentLevel))
        {
            int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

            if (currentLevel == reachedLevel)
            {
                PlayerPrefs.SetInt("ReachedLevel", currentLevel + 1);
                PlayerPrefs.Save(); 
                Debug.Log("Next Level Unlocked: Level " + (currentLevel + 1));
            }
        }
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // गेम सुरुमा अनपज गर्ने (नत्र अर्को लेभल पनि रोकिएकै हुन्छ)
        // अर्को लेभल लोड गर्ने (Build Settings को आधारमा)
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         // --- तपाईंको पुरानो First-Time Player Logic यहाँ छ ---
        if (!PlayerPrefs.HasKey("FirstTimePlayed"))
        {
            // गेम जीवनमा पहिलो पटक खोल्दै छ भने:
            PlayerPrefs.SetInt("FirstTimePlayed", 1);
            PlayerPrefs.Save();

            // सिधै Level1 लोड गर्ने (तपाईंको पुरानो कोडमा Scene Index २ थियो)
            // नोट: यदि Build Settings मा level1 को index 2 हो भने ठीक छ, नत्र "level1" नाम लेख्दा झन् सुरक्षित हुन्छ।
            SceneManager.LoadScene(2); 
        }
        else
        {
            // पहिले नै खेलिसकेको प्लेयर हो भने Map (Level Selection) मा पठाउने
            SceneManager.LoadScene("LevelSelection"); 
        }
    }

    public void QuitToMenu()
    {
        Debug.Log("Game matches quitting...");
        Time.timeScale = 1f;
        // यदि Main Menu छ भने त्यसको नाम लेख्ने, नत्र गेम बन्द गर्ने
        Application.Quit(); 
    }
}
