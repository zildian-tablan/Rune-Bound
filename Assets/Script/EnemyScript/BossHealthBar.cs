using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public GameObject healthbar;

    private void Awake()
    {
        if (healthbar != null)
        {
            healthbar.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Something entered: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered by player!");
            if (healthbar != null)
            {
                healthbar.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Healthbar not assigned!");
            }
        }
    }

    public void Hide()
    {
        if (healthbar != null)
        {
            healthbar.SetActive(false);
        }
    }

}
