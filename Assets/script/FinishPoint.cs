using UnityEngine;

public class FinishPoint : MonoBehaviour // तपाईंको क्लासको नाम यहाँ हुन सक्छ
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // प्लेयर झण्डामा ठोकियो कि नाइँ चेक गर्ने
        if (collision.CompareTag("Player"))
        {
            // सिनमा भएको LevelController को ShowWinMenu() चलाउने
            LevelController levelCtrl = Object.FindAnyObjectByType<LevelController>();
            
            if (levelCtrl != null)
            {
                levelCtrl.ShowWinMenu();
            }
            else
            {
                Debug.LogError("सिनमा LevelController भेटिएन! एउटा GameObject मा LevelController स्क्रिप्ट हाल्नुहोस्।");
            }
        }
    }
}