using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;
    private GameOverManager gameOverManager;

    private ScoreManager scoreManager;

    public bool isPlayer = false; // <-- Add this to differentiate player
     public bool isBoss = false; // <-- Set true in Inspector for the boss
    public bool isLastBoss = false; // <-- Set true in Inspector for the last boss
    public bool isSummoner = false; // <-- Set true in Inspector for the summoner
    public bool isNecromancer = false;
    public bool isHell = false;
    public GameObject ariseText; // <-- Set this in Inspector for the boss
    public GameObject necro1, necro2, necro3,necro4,necro5; // <-- Set these in Inspector for the boss
    private LevelCompleteManager levelCompleteManager;
     public HealthBar playerHealthBar,bossHealthBar; // <-- Reference to UI Health Bar (only set for player)

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get => _maxHealth;
        private set => _maxHealth = value;
    }

    [SerializeField]
    public int _health = 100;
    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
             if (_health <= 0)
             {
                 _health = 0;
                 IsAlive = false;
                 if (isPlayer)
                 {
                     var controller = GetComponent<PlayerController>();
                     if (controller != null)
                     {
                         controller.enabled = false;
                     }

                     StartCoroutine(DelayedGameOver(1.5f));
                 }
                 if (isLastBoss && levelCompleteManager != null)
                 {
                     levelCompleteManager.ShowLevelComplete(); // now with delay
                 }
             }
             if (isPlayer && playerHealthBar != null)
             {
                 playerHealthBar.SetHealth(_health);
             }
             if (isBoss && bossHealthBar != null)
             {
                bossHealthBar.SetHealth(_health);
             }
             if (_health <= 0 && isBoss)
             {
                 if (bossHealthBar != null)
                 {
                     bossHealthBar.gameObject.SetActive(false);
                 }

                 BossHealthBar bossHealthBarTrigger = FindFirstObjectByType<BossHealthBar>();
                 if (bossHealthBarTrigger != null)
                 {
                     bossHealthBarTrigger.Hide();
                 }
             }
            if (isBoss && (isNecromancer || isHell) && _health <= 100 && !hasArisen)
            {
                hasArisen = true;
                Arise();
            }
            if (isLastBoss && isNecromancer && _health <= 300)
            {
                Summon();
            }

        }
    }

    private void Summon()
    {
        throw new NotImplementedException();
    }

    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get => _isAlive;
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("isAlive: " + value);
        }
    }

    [SerializeField]
    private bool isInvincible = false;

    [SerializeField]
    private bool ignoreKnockback = false; // ✅ NEW FIELD

    private Coroutine hideAriseCoroutine;
    private bool hasArisen = false;

    public bool IgnoreKnockback => ignoreKnockback;

    public bool IsHit
    {
        get => animator.GetBool(AnimationStrings.isHit);
        private set => animator.SetBool(AnimationStrings.isHit, value);
    }

    public bool LockVelocity
    {
        get => animator.GetBool(AnimationStrings.lockVelocity);
        set => animator.SetBool(AnimationStrings.lockVelocity, value);
    }

    private float timeSinceHit;
    public float invincibilityTime = 0.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
           gameOverManager = FindFirstObjectByType<GameOverManager>();
        scoreManager = FindFirstObjectByType<ScoreManager>();

        if (isBoss)
        {
            levelCompleteManager = FindFirstObjectByType<LevelCompleteManager>();
        }

        if (isPlayer && playerHealthBar != null)
        {
            playerHealthBar.SetMaxHealth(MaxHealth);
            playerHealthBar.SetHealth(Health);
        }
        if (isBoss && bossHealthBar != null)
        {
            bossHealthBar.SetMaxHealth(MaxHealth);
            bossHealthBar.SetHealth(Health);
        }
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

        if (ariseText != null && ariseText.activeSelf && hideAriseCoroutine == null)
        {
            hideAriseCoroutine = StartCoroutine(HideAriseTextAfterDelay(3f));
        }
    }

    public bool Hit(int damage, Vector2 knockback, bool ignoreInvincibility = false)
    {
        if (!_isAlive)
            return false;

        if (isInvincible && !ignoreInvincibility)
            return false;

        Attack attackScript = GetComponentInChildren<Attack>(); // or get from parent

        if (!ignoreInvincibility && attackScript != null && attackScript.IsInvincible())
        {
            Debug.Log("Player is invincible! No damage taken.");
            return false;
        }

        Health -= damage;
        if (!ignoreInvincibility)
            isInvincible = true;

        animator.SetTrigger(AnimationStrings.hitTrigger);
        LockVelocity = true;
        Vector2 finalKnockback = ignoreKnockback ? Vector2.zero : knockback;
        damageableHit?.Invoke(damage, finalKnockback);
        // Check if the hit is from a normal enemy or a boss
        if (scoreManager != null)
        {
            int scoreToAdd = isBoss ? 50 : 20; // Boss gives 50 points, normal enemies give 20
            scoreManager.AddScore(scoreToAdd); // Add score based on the enemy type
        }

        return true;
    }

    private IEnumerator DelayedGameOver(float delay)
{
    yield return new WaitForSeconds(delay);

    if (gameOverManager != null)
    {
        gameOverManager.ShowGameOver();
    }
}
    private void Arise()
    {
        if (ariseText != null)
        {
            ariseText.SetActive(true);
        }

        if (hideAriseCoroutine != null)
        {
            StopCoroutine(hideAriseCoroutine);
        }

        hideAriseCoroutine = StartCoroutine(HideAriseTextAfterDelay(3f));

        if (necro1 != null) necro1.SetActive(true);
        if (necro2 != null) necro2.SetActive(true);
        if (necro3 != null) necro3.SetActive(true);
        if (necro4 != null) necro4.SetActive(true);
        if (necro5 != null) necro5.SetActive(true);
    }

    private IEnumerator HideAriseTextAfterDelay(float v)
    {
        yield return new WaitForSeconds(v);
        if (ariseText != null)
        {
            ariseText.SetActive(false);
        }
        hideAriseCoroutine = null;
    }
}
// if (_isAlive && !isInvincible)
//         {
//             Health -= damage;
//             isInvincible = true;
//             animator.SetTrigger(AnimationStrings.hitTrigger);
//             LockVelocity = true;

//             // ✅ Send zero knockback if ignoreKnockback is true
//             Vector2 finalKnockback = ignoreKnockback ? Vector2.zero : knockback;
//             damageableHit?.Invoke(damage, finalKnockback);
//             return true;
//         }