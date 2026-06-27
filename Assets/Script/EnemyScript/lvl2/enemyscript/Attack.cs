using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Attack : MonoBehaviour
{
    public int attackdamage = 10;
    public Vector2 knockback = Vector2.zero;
    public bool isPlayer = false;
    public bool isKnight = false;
    public bool isMage = false;
    public int damagemultiplier = 3;
    public bool ignoreTargetInvincibility = false;
    public float invincibilityDuration = 20f;// Duration of invincibility
    public float fireballDuration = 1f; // Duration of fireball
    public float damageDuration = 30f; // Duration of damage boost

    public float attackCooldown = 2f; // Cooldown between attacks
    private float attackCooldownTimer = 0f;
    public float soundAttackDelay = 0.3f; // Delay before playing attack sound// time between sounds
    private float playerAttackSoundTimer = 0f;

    public GameObject ability1Text;
    public Image abilityImage1; // Damage boost skill
    public float cooldown1 = 5f;

    public GameObject ability2Text;
    public Image abilityImage2; // Invincibility skill
    public float cooldown2 = 10f;
    public ParticleSystem invincibilityEffect;

    //for knight
    private int damage;
    private bool isSkillActive = false;
    private bool isCooldown1 = false;

    private bool isInvincible = false;
    private bool isCooldown2 = false;

    //for mage
    private bool isFireball = false;
    private bool isHeal = false;
    public int healAmount = 100; // Amount to heal

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public AudioClip attackSound;
    private AudioSource audioSource;
    Animator animator;
    Damageable damageables;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        damageables = GetComponent<Damageable>();
        if (damageables == null)
            damageables = GetComponentInParent<Damageable>();
        damage = attackdamage;

        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        //else
        //{
        //    Debug.LogWarning("SpriteRenderer not found on parent!");
        //}

        if (invincibilityEffect != null)
        {
            invincibilityEffect.Stop();
        }
    }

    void Update()
    {

        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if (playerAttackSoundTimer > 0f)
        {
            playerAttackSoundTimer -= Time.deltaTime;
        }
        if (isKnight)
        {
            // Skill 1 - Damage Boost (K)
            if (isPlayer && Input.GetKeyDown(KeyCode.K) && !isCooldown1)
            {

                StartCoroutine(ActivateDamageBoost());
            }

            if (isPlayer && playerAttackSoundTimer <= 0f && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.J)))
            {
                PlayAttackSound();
                playerAttackSoundTimer = soundAttackDelay; // reset cooldown
            }

            // Skill 2 - Invincibility (L)
            if (isPlayer && Input.GetKeyDown(KeyCode.L) && !isCooldown2)
            {
                StartCoroutine(ActivateInvincibility());
            }
        }
        if (isMage)
        {
            // Skill 1 - Fireball (K)
            if (isPlayer && Input.GetKeyDown(KeyCode.K) && !isCooldown1)
            {
                PlayAttackSound();
                StartCoroutine(onFireBall());
            }
            // Skill 2 - Invincibility (L)
            if (isPlayer && Input.GetKeyDown(KeyCode.L) && !isCooldown2)
            {
                StartCoroutine(ActivateInvincibility());
            }
            if (isPlayer && playerAttackSoundTimer <= 0f && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.J)))
            {
                PlayAttackSound();
                playerAttackSoundTimer = soundAttackDelay; // reset cooldown
            }
        }

            // Cooldown UI - Damage Boost
            if (isCooldown1)
            {
                abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
                if (abilityImage1.fillAmount <= 0)
                {
                    abilityImage1.fillAmount = 0;
                    isCooldown1 = false;
                }
            }

            // Cooldown UI - Invincibility
            if (isCooldown2)
            {
                abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
                if (abilityImage2.fillAmount <= 0)
                {
                    abilityImage2.fillAmount = 0;
                    isCooldown2 = false;
                }
            }
        
    }

    //KNIGHT SKILL
    private IEnumerator ActivateDamageBoost()
    {
        isSkillActive = true;
        isCooldown1 = true;
        abilityImage1.fillAmount = 1;
        damage = attackdamage * damagemultiplier;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1f, 0.84f, 0.3f); // Gold color
        }

        Debug.Log("Damage skill activated: " + damage);
        ability1Text.SetActive(true);
        StartCoroutine(HideAbility1TextAfterDelay(3f));
        yield return new WaitForSeconds(damageDuration);

        isSkillActive = false;
        damage = attackdamage;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }

        Debug.Log("Damage skill ended.");
    }

    private IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        isCooldown2 = true;
        abilityImage2.fillAmount = 1;

        Debug.Log("Invincibility activated!");

        if (invincibilityEffect != null)
        {
            invincibilityEffect.Play();
        }

        ability2Text.SetActive(true);
        StartCoroutine(HideAbility2TextAfterDelay(3f));
        yield return new WaitForSeconds(invincibilityDuration); // invincibility duration

        isInvincible = false;

        if (invincibilityEffect != null)
        {
            invincibilityEffect.Stop();
        }

        Debug.Log("Invincibility ended.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (isPlayer) {
           
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                //if (audioSource != null && attackSound != null)
                //    audioSource.PlayOneShot(attackSound);

                int currentDamage = isSkillActive ? damage : attackdamage;

                Vector2 deliveredKnockback = transform.parent.localScale.x > 0
                    ? knockback
                    : new Vector2(-knockback.x, knockback.y);
               

                //StartCoroutine(ActivateDamageBoost());

                bool gotHit = damageable.Hit(currentDamage, deliveredKnockback, ignoreTargetInvincibility);
                if (gotHit)
                {
                    Debug.Log("Hit " + collision.name + " : " + currentDamage);
                }
            }
        }
        if (!isPlayer && attackCooldownTimer <= 0f)  // Non-player attack cooldown
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                int currentDamage = attackdamage;

                Vector2 deliveredKnockback = transform.parent.localScale.x > 0
                    ? knockback
                    : new Vector2(-knockback.x, knockback.y);

                bool gotHit = damageable.Hit(currentDamage, deliveredKnockback, ignoreTargetInvincibility);
                if (gotHit)
                {
                    Debug.Log("Hit " + collision.name + " : " + currentDamage);
                }

                // Set the cooldown timer
                attackCooldownTimer = attackCooldown;
            }
        }

    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public void TryActivateSkill1()
    {
        if (!isPlayer || isCooldown1)
        {
            return;
        }

        if (isMage)
        {
            PlayAttackSound();
            StartCoroutine(onFireBall());
        }
        else if (isKnight)
        {
            StartCoroutine(ActivateDamageBoost());
        }
    }

    //private IEnumerator DelayedAttackSound()
    //{
    //    yield return new WaitForSeconds(soundAttackDelay); // delay before playing
    //    PlayAttackSound();
    //}


    private void PlayAttackSound()
    {
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
            Debug.Log("Attack sound played.");
        }
    }

    //Mage Skill
    public IEnumerator onFireBall()
    {
        isFireball = true;
        isCooldown1 = true;
        abilityImage1.fillAmount = 1;

        Debug.Log("Fireball activated!");
        ability1Text.SetActive(true);
        animator.SetBool("rangeAttack", true);
        yield return new WaitForSeconds(fireballDuration); // fireball duration
        ability1Text.SetActive(false);
        animator.SetBool("rangeAttack", false);
        isFireball = false;

        Debug.Log("Fireball ended.");
    }

    private IEnumerator HideAbility2TextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ability2Text.SetActive(false);
    }
    private IEnumerator HideAbility1TextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ability1Text.SetActive(false);
    }


}
