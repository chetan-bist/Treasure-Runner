using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA; // लेफ्ट पोइन्ट
    [SerializeField] private Transform pointB; // राइट पोइन्ट
    [SerializeField] private float speed = 3f;   // प्लेटफर्मको स्पिड

    private Rigidbody2D rb;
    private Vector2 target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointA.position;
    }

    void FixedUpdate()
    {
        // प्लेटफर्मको नयाँ पोजिसन निकाल्ने
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        
        // Rigidbody मार्फत फिजिक्स मुभमेन्ट गराउने (यसले प्लेयरलाई जरक/झट्का दिँदैन)
        rb.MovePosition(newPos);

        // पोइन्ट A वा B मा पुगेपछि टारगेट चेन्ज गर्ने
        if (Vector2.Distance(rb.position, pointA.position) < 0.1f)
        {
            target = pointB.position;
        }
        else if (Vector2.Distance(rb.position, pointB.position) < 0.1f)
        {
            target = pointA.position;
        }
    }
}