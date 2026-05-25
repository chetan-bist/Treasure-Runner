using UnityEngine;
using UnityEngine.SceneManagement; 

public class LevelChanger : MonoBehaviour
{
    public string nextSceneName; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Yo massage play garyo bhane portal le collision thaha payo
        Debug.Log("Kehi kura portal sanga thokkiyo: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            // 2. Yo message aayo bhane Player nai thokkiko ho ra Scene change huna lagyo
            Debug.Log("Player thokkio! Aba scene load hudai xa: " + nextSceneName);
            
            SceneManager.LoadScene(nextSceneName);
        }
    }
}