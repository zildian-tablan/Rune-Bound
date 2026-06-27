using System.Collections;
using UnityEngine;

public class BossSpikeSummon : MonoBehaviour
{
    public GameObject spikePrefab;
    public Transform[] spikeSpawnPoints;
    public float destroytime = 2.5f;



    public void SpikeAttack()
    {
        // Start the coroutine to spawn spikes
        StartCoroutine(Spawn());
        Destroy(gameObject, destroytime); // Destroy the spike summon object after 5 seconds
    }
    public IEnumerator Spawn()
    {
        // Wait for 1 second before spawning spikes
        yield return new WaitForSeconds(5f);
        // Call the method to spawn spikes
        SpawnSpikes();
    }
    public void SpawnSpikes()
    {
        // Get the boss's current facing direction
        float bossDirection = transform.localScale.x;

        foreach (Transform point in spikeSpawnPoints)
        {
            // Instantiate spike at spawn point
            GameObject spike = Instantiate(spikePrefab, point.position, Quaternion.identity);

            // Flip spike based on boss's facing direction
            Vector3 scale = spike.transform.localScale;
            scale.x = bossDirection > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            spike.transform.localScale = scale;
        }

        Debug.Log("Spikes summoned in boss's facing direction.");
    }
}
