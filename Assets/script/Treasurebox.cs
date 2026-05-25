using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // यदि प्लेयर आयो र बक्स खुलेको छैन भने मात्र चल्ने
        if (collision.CompareTag("Player") && !isOpened)
        {
            isOpened = true; 
            OpenTreasure();
        }
    }

    private void OpenTreasure()
    {
        // १. साउन्ड म्यानेजरबाट ट्रेजर ओपन साउन्ड बजाउने
        if (SoundManager.instance != null && SoundManager.instance.treasureSound != null)
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.treasureSound);
        }

        // २. प्लेयरको स्क्रिप्टबाट हालको कोइनको संख्या पत्ता लगाउने
        int totalCoinsOnWin = 0;
        player_movement player = FindFirstObjectByType<player_movement>();
        
        if (player != null)
        {
            // नोट: तपाईँको प्लेयर स्क्रिप्टमा कोइन सेभ गर्ने भ्यारियबलको नाम जस्तै (उदा: PlayerPrefs) बाट तानिएको
            totalCoinsOnWin = PlayerPrefs.GetInt("SavedCoins", 0); 
        }

        // ३. UIManager लाई फाइनल कोइन बुझाएर विन स्क्रिन अन गर्न लगाउने
        if (UIManager.instance != null)
        {
            UIManager.instance.TriggerWin(totalCoinsOnWin);
        }
    }
}