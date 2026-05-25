using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform pointA; // लेफ्ट पोइन्ट
    [SerializeField] private Transform pointB; // राइट पोइन्ट
    [SerializeField] private float speed = 3f;   // एनिमीको स्पिड

    private Rigidbody2D rb;
    private Vector2 target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointA.position;
        UpdateFacingDirection(); // गेम सुरु हुँदा सहि दिशामा फर्काउने
    }

    void FixedUpdate()
    {
        // एनिमीको नयाँ पोजिसन निकाल्ने
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        
        // Rigidbody मार्फत मुभमेन्ट गराउने
        rb.MovePosition(newPos);

        // पोइन्ट A मा पुगेपछि टारगेट B बनाउने र दिशा फेर्ने
        if (Vector2.Distance(rb.position, pointA.position) < 0.1f)
        {
            target = pointB.position;
            UpdateFacingDirection(); // ---- नयाँ थपिएको लाइन ----
        }
        // पोइन्ट B मा पुगेपछि टारगेट A बनाउने र दिशा फेर्ने
        else if (Vector2.Distance(rb.position, pointB.position) < 0.1f)
        {
            target = pointA.position;
            UpdateFacingDirection(); // ---- नयाँ थपिएको लाइन ----
        }
    }

    // ======================================================================
    // नयाँ थपिएको फङ्सन: एनिमी हिँडेको दिशातिर त्यसको फेस (Flip) गराउन
    // ======================================================================
    private void UpdateFacingDirection()
    {
        // यदि एनिमी पोइन्ट B तिर जाँदैछ भने त्यो दायाँ (Right) जाँदैछ
        if (target == (Vector2)pointB.position)
        {
            // दायाँ फर्कन scale पोजिटिभ (+) राख्ने
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // यदि एनिमी पोइन्ट A तिर जाँदैछ भने त्यो बायाँ (Left) जाँदैछ
        else
        {
            // बायाँ फर्कन scale नेगेटिभ (-) राख्ने
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}