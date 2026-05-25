using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource; // जम्प, कोइन, हर्ट, डेथ बजाउने स्पिकर
    [SerializeField] private AudioSource runSource; // रन साउन्ड मात्र बजाउने विशेष स्पिकर

    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip runSound;
    public AudioClip treasureSound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // १. यसले sfxSource (पहिलो स्पिकर) बाट मात्र साउन्ड बजाउँछ
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // २. यसले runSource (दोस्रो स्पिकर) बाट मात्र रन साउन्ड बजाउँछ
    public void PlayRunSound()
    {
        if (runSource != null && !runSource.isPlaying && runSound != null)
        {
            runSource.clip = runSound;
            runSource.loop = true; // लगातार बज्न दिने
            runSource.Play();
        }
    }

    // ३. मुख्य फिक्स: यसले runSource लाई मात्र स्टप गर्छ, अर्को स्पिकर (SFX) लाई छुँदा पनि छुँदैन!
    public void StopRunSound()
    {
        if (runSource != null && runSource.isPlaying)
        {
            runSource.Stop(); // यसले रन साउन्ड मात्र बन्द गर्छ
        }
    }
}