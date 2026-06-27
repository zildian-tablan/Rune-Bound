using UnityEngine;

public class BlockController1 : MonoBehaviour
{
    public Collider2D blockCollider;

    void Update()
    {
        // Count how many enemies are left
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("boss1");
       // Debug.Log("Enemies left: " + enemies.Length);
        if (enemies.Length == 0)
        {
            // Disable the collider if no enemies remain
            if (blockCollider.enabled)
                blockCollider.enabled = false;
        }
        else
        {
            // Make sure collider is on if enemies are alive
            if (!blockCollider.enabled)
                blockCollider.enabled = true;
        }
    }
}
