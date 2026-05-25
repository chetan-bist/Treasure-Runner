using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; 
    
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true); 
        Time.timeScale = 0f;        
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false); 
        Time.timeScale = 1f;         
        isPaused = false;
    }

    // ---- नयाँ थपिएको Restart फङ्सन (यो Game Over र Pause दुवैमा काम लाग्छ) ----
    public void RestartGame()
    {
        Time.timeScale = 1f; // गेम टाइम अन-फ्रिज गर्ने
        // हाल खेलिरहेको लेभललाई सुरुदेखि लोड गर्ने
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void QuitGame()
    {
        Debug.Log("Game matches quitting..."); 
        Application.Quit();                     
    }
}