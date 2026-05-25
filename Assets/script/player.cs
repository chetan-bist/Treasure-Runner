using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class player_movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int maxJumps = 2; 

    [Header("Coin UI Settings")]
    [SerializeField] private TextMeshProUGUI coinText; 

    [Header("Health UI Settings (Drop 3 Hearts Here)")]
    [SerializeField] private Image[] hearts; 

    [Header("Level Complete Settings")]
    [SerializeField] private GameObject winPanel; 

    [Header("Game Over Settings")]
    [SerializeField] private GameObject gameOverPanel; 

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim; // थपिएको: Animator को reference

    private float moveInput;
    private int jumpCount; 

    private Vector3 startPoint; 
    private int totalCoins; 
    
    private int currentHealth = 3; 
    private int maxHealth = 3;     

    private Rigidbody2D platformRigidbody; 

    void Start()
    {
        Time.timeScale = 1f; 

        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>(); // थपिएको: Player मा भएको Animator पत्ता लगाउने
        
        startPoint = transform.position;

        if (PlayerPrefs.HasKey("HasCheckpoint") && PlayerPrefs.GetInt("HasCheckpoint") == 1)
        {
            float savedX = PlayerPrefs.GetFloat("CheckpointX");
            float savedY = PlayerPrefs.GetFloat("CheckpointY");
            transform.position = new Vector2(savedX, savedY);
        }

        totalCoins = PlayerPrefs.GetInt("SavedCoins", 0);
        UpdateCoinUI(); 
        UpdateHealthUI();

        if (winPanel != null) winPanel.SetActive(false); 
        if (gameOverPanel != null) gameOverPanel.SetActive(false); 
    }

    void Update()
    {
        if (IsGrounded())
        {
            jumpCount = 0;
        }
        FlipPlayer();
        
        // थपिएको: Animation हरू कन्ट्रोल गर्ने फङ्सन कल गरेको
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        float platformHorizontalVelocity = 0f;
        
        if (platformRigidbody != null)
        {
            platformHorizontalVelocity = platformRigidbody.linearVelocity.x;
        }

        body.linearVelocity = new Vector2((moveInput * moveSpeed) + platformHorizontalVelocity, body.linearVelocity.y);

        if (IsGrounded() && moveInput != 0f)
        {
            SoundManager.instance.PlayRunSound();
        }
        else
        {
            SoundManager.instance.StopRunSound();
        }
    }

    // ======================================================================
    // नयाँ थपिएको फङ्सन: Code बाट Animator को parameters कन्ट्रोल गर्ने
    // ======================================================================
    private void UpdateAnimations()
    {
        if (anim != null)
        {
            // १. यदि प्लेयर हिडिरहेको छ (moveInput ० छैन) र जमिनमा छ भने 'isruning' true हुन्छ
            bool isRunning = (moveInput != 0f) && IsGrounded();
            anim.SetBool("isruning", isRunning);

            // २. यदि प्लेयर हावामा छ (जमिनमा छैन) भने 'isjumping' true हुन्छ
            bool isJumping = !IsGrounded();
            anim.SetBool("isjumping", isJumping);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trap")) TakeDamage();
        if (collision.CompareTag("Enemy")) TakeDamage();
        if (collision.CompareTag("Coin")) CollectCoin(collision.gameObject);
        if (collision.CompareTag("Heart")) CollectHeart(collision.gameObject);

        if (collision.gameObject.name == "finishpoint" || collision.CompareTag("Finish"))
        {
            CompleteLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
            platformRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
            platformRigidbody = null;
        }
    }

    private void TakeDamage()
    {
        currentHealth--; 
        UpdateHealthUI(); 

        if (currentHealth <= 0)
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.deathSound);
            TriggerGameOver(); 
        }
        else
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.hurtSound);
            transform.position = startPoint;
            body.linearVelocity = Vector2.zero;
        }
    }

    private void CollectHeart(GameObject heartObject)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++; 
            UpdateHealthUI(); 
            Destroy(heartObject); 
        }
    }

    private void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); 
        }
        Time.timeScale = 0f; 
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth) hearts[i].enabled = true; 
            else hearts[i].enabled = false; 
        }
    }

    private void CollectCoin(GameObject coinObject)
    {
        SoundManager.instance.PlaySFX(SoundManager.instance.coinSound);

        totalCoins++;
        UpdateCoinUI();
        PlayerPrefs.SetInt("SavedCoins", totalCoins);
        PlayerPrefs.Save();
        Destroy(coinObject);
    }

    private void UpdateCoinUI()
    {
        if (coinText != null) coinText.text = totalCoins.ToString();
    }

    private void CompleteLevel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true); 
        }
        Time.timeScale = 0f; 
    }

    public void MoveLeftDown() { moveInput = -1f; }
    public void MoveRightDown() { moveInput = 1f; }
    public void StopMove() { moveInput = 0f; }

    public void Jump()
    {
        if (IsGrounded() || jumpCount < maxJumps)
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.jumpSound);

            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return hit.collider != null;
    }

    private void FlipPlayer()
    {
        if (moveInput > 0.01f) transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        else if (moveInput < -0.01f) transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
    }
}