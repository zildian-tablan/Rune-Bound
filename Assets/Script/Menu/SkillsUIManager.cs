using UnityEngine;

public class SkillsUIManager : MonoBehaviour
{
    [Header("Knight Skill Buttons")]
    public GameObject knightSkillButton1;
    public GameObject knightSkillButton2;
    public GameObject knightSkillButtoncd1;
    public GameObject knightSkillButtoncd2;

    [Header("Mage Skill Buttons")]
    public GameObject mageSkillButton1;
    public GameObject mageSkillButton2;
    public GameObject mageSkillButtoncd1;
    public GameObject mageSkillButtoncd2;

    void Start()
    {
        UpdateSkillButtons();
    }

    public void UpdateSkillButtons()
    {
        if (PlayerManager.Instance == null)
        {
            Debug.LogWarning("SkillsUIManager: PlayerManager.Instance is null.");
            return;
        }

        string selectedClass = PlayerManager.Instance.selectedClass;
        bool isKnight = selectedClass == "Knight" || selectedClass == "Archer";
        bool isMage = selectedClass == "Mage" || selectedClass == "Assassin";

        knightSkillButton1?.SetActive(isKnight);
        knightSkillButton2?.SetActive(isKnight);
        knightSkillButtoncd1?.SetActive(isKnight);
        knightSkillButtoncd2?.SetActive(isKnight);
        mageSkillButton1?.SetActive(isMage);
        mageSkillButton2?.SetActive(isMage);
        mageSkillButtoncd1?.SetActive(isMage);
        mageSkillButtoncd2?.SetActive(isMage);
    }
}
