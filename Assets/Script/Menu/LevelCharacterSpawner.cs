using Unity.Cinemachine;
using UnityEngine;

public class LevelCharacterSpawner : MonoBehaviour
{
    public GameObject knightPrefab;
    public GameObject magePrefab;
    public GameObject archerPrefab;
    public GameObject assassinPrefab;

    public CinemachineCamera camera1;
    public CinemachineCamera camera2;
    public CinemachineCamera camera3;
    public CinemachineCamera camera4;

    void Start()
    {
        string selected = PlayerManager.Instance.selectedClass;

        // Deactivate all first
        knightPrefab.SetActive(false);
        magePrefab.SetActive(false);
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(false);
        camera3.gameObject.SetActive(false);
        camera4.gameObject.SetActive(false);
        archerPrefab.SetActive(false);
        assassinPrefab.SetActive(false);

        // Activate the selected one
        switch (selected)
        {
            case "Knight":
                knightPrefab.SetActive(true);
                camera1.gameObject.SetActive(true);
                break;
            case "Mage":
                magePrefab.SetActive(true);
                camera2.gameObject.SetActive(true);
                break;
            case "Archer":
                archerPrefab.SetActive(true);
                camera3.gameObject.SetActive(true);
                break;
            case "Assassin":
                assassinPrefab.SetActive(true);
                camera4.gameObject.SetActive(true);
                break;
        }
    }
}
