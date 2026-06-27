using UnityEngine;

public class StoneSpike : MonoBehaviour
{
    public int damage = 10;
    public Vector2 knockback = new Vector2(0, 5f);

    private Animator animator;
    private bool hasHit = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Optional: Destroy after X seconds fallback
        Destroy(gameObject, 3f);
    }

 void OnTriggerEnter2D(Collider2D collision)
{
    if (collision == null) return;

    Damageable damageable = collision.GetComponent<Damageable>();

    if (damageable != null)
    {
        Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

        if (damageable.Hit(damage, deliveredKnockback))
        {
            Debug.Log("Spike hit " + collision.name);
        }
    }
}

    // Called at end of animation using Animation Event
  public void DestroySpike()
{
    Destroy(gameObject);
}

}
