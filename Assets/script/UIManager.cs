using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject inGameUI;       // गेम खेल्दा देखिने बटन र कोइनहरू
    [SerializeField] private GameObject gameOverPanel;   // मरेपछि देखिने प्यानल
    [SerializeField] private GameObject winScreenPanel;  // जितेपछि देखिने प्यानल

    [Header("Win Screen Text")]
    [SerializeField] private TextMeshProUGUI finalScoreText; // "Congratulations" देखाउने टेक्स्ट

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // गेम लेभल सुरु हुँदा पुराना कन्ट्रोलहरू मात्र देखाउने, अरू सब लुकाउने
        Time.timeScale = 1f;
        if (inGameUI != null) inGameUI.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (winScreenPanel != null) winScreenPanel.SetActive(false);
    }

    // ==== १. MAIN MENU / GENERAL BUTTONS ====
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // विल्ड सेटिङको १ नम्बर (Level 1) लोड गर्ने
    }

    public void QuitGame()
    {
        Application.Quit(); // गेम बन्द गर्ने
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // ० नम्बरमा MainMenu हुन्छ
    }

    // ==== २. GAME OVER SCREEN ====
    public void TriggerGameOver()
    {
        Time.timeScale = 0f; // गेम रोक्ने
        if (inGameUI != null) inGameUI.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ==== ३. WIN SCREEN (ट्रेजर बक्स छोएपछि चल्ने) ====
    public void TriggerWin(int finalCoins)
    {
        Time.timeScale = 0f; // गेमको समय रोक्ने
        if (inGameUI != null) inGameUI.SetActive(false); // पुराना सबै कन्ट्रोल लुकाउने
        if (winScreenPanel != null) winScreenPanel.SetActive(true);   // विन स्क्रिन देखाउने

        // स्क्रिनमा बधाई र फाइनल कोइन स्कोर छाप्ने
        if (finalScoreText != null)
        {
            finalScoreText.text = "Congratulations!\nFinal Score: " + finalCoins.ToString() + " Coins";
        }
    }

    // NEXT LEVEL बटन थिच्दा चल्ने फङ्सन
    public void NextLevel()
    {
        Time.timeScale = 1f; // गेम ब्युँताउने
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // यदि लिस्टमा अर्को लेभल छ भने लोड गर्ने, नत्र मेन्युमा फर्किने
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0); 
        }
    }
}