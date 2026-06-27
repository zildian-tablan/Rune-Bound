using UnityEngine;

public class LaserLauncher : MonoBehaviour
{
    public Transform launchPoint;          // Where the laser spawns from
    public GameObject laserPrefab;         // Your laser prefab
    public float laserDuration = 2f;       // How long the laser stays active

    public void FireLaser()
    {
        // Instantiate the laser at launchPoint's position and rotation
        GameObject laser = Instantiate(laserPrefab, launchPoint.position, launchPoint.rotation);

        // Flip laser's scale based on enemy facing direction (optional)
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        Vector3 origScale = laser.transform.localScale;

        laser.transform.localScale = new Vector3(
            Mathf.Abs(origScale.x) * direction,
            origScale.y,
            origScale.z
        );

        // Optional: Align the laser’s facing direction to the launch point’s forward direction
        laser.transform.right = launchPoint.right;

        // Destroy after duration
        Destroy(laser, laserDuration);
    }
}
