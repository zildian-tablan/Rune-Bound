using UnityEngine;
using System.Collections.Generic;

public class LaserBeam : MonoBehaviour
{
    public int damage = 10;
    public Vector2 knockback = new Vector2(0, 0);
    public float tickRate = 0.5f; // how often it deals damage
    private float nextTickTime = 0f;

    private HashSet<Damageable> damagedTargets = new HashSet<Damageable>();

    void Update()
    {
        if (Time.time >= nextTickTime)
        {
            damagedTargets.Clear(); // Allow damaging again after each tick
            nextTickTime = Time.time + tickRate;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null && !damagedTargets.Contains(damageable))
        {
            // Flip knockback based on direction
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            if (gotHit)
            {
                Debug.Log("Laser hit " + collision.name + " for " + damage);
                damagedTargets.Add(damageable);
            }
        }
    }
}
