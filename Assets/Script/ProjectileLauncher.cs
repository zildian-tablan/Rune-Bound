using UnityEngine;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform lauchPoint;
    public GameObject projectilePrefab;
    public float timer = 3;
    public float balltimer = 0.5f;
    private bool hasFired = false;
    public void SpikeAttack()
    {
        // Start the coroutine to spawn spikes
      

        StartCoroutine(Firing());
        Destroy(gameObject, timer); // Destroy the spike summon object after 5 seconds
    }
    public IEnumerator Firing()
    {

        yield return new WaitForSeconds(balltimer);
        // Call the method to spawn spikes
        FireProjectile();
    }
    public void FireProjectile()
{
    GameObject projectile = Instantiate(projectilePrefab, lauchPoint.position, projectilePrefab.transform.rotation);
    Vector3 origScale = projectile.transform.localScale;

    float direction = transform.localScale.x > 0 ? 1f : -1f;

    projectile.transform.localScale = new Vector3(
        Mathf.Abs(origScale.x) * direction,
        origScale.y,
        origScale.z
    );
}


}
