using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Corrected: use transform.localScale.x instead of 'c', and 'velocity' instead of 'linearVelocity'
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // Flip knockback based on direction
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            // Hit the object
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            if (gotHit)
            {
                Debug.Log("Hit " + collision.name + ": " + damage);
                  Destroy(gameObject);
            }
            

          
          
        }
        
    }

    void Update()
    {
        // Optional: add lifetime or boundary checks if needed
    }
}
