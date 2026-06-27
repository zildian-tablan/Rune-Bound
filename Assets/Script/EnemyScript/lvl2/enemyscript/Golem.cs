using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Golem : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.6f;

    public DetectionZone attackZone;        // Melee range
    public DetectionZone laserZone;         // Laser range (longer)
    public DetectionZone hellZone;
    public bool isLastLvl3boss;
    //public GameObject HellNados;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    public DetectionZone cliffDetectionZone;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;
    public Vector2 walkDirectionVector = Vector2.right;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Flip sprite when changing direction
                gameObject.transform.localScale = new Vector2(
                    gameObject.transform.localScale.x * -1,
                    gameObject.transform.localScale.y
                );

                walkDirectionVector = value == WalkableDirection.Right ? Vector2.right : Vector2.left;
            }
            _walkDirection = value;
        }
    }

    [SerializeField] private bool _hasTarget = false;
    public bool HasTarget
    {
        get => _hasTarget;
        private set
        {
            
                _hasTarget = value;
                animator.SetBool(AnimationStrings.hasTarget, value);
         
           
        }
    }


    public bool CanMove => animator.GetBool(AnimationStrings.canMove);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    void Update()
    {
        bool hasMeleeTarget = attackZone && attackZone.detectedcolliders.Count > 0;
        bool hasHellTarget = hellZone && hellZone.detectedcolliders.Count > 0;
        bool hasLaserTarget = laserZone && laserZone.detectedcolliders.Count > 0;
        //bool hasFireTarget = hellZone && hellZone.detectedcolliders.Count > 0;

        HasTarget = hasMeleeTarget || hasLaserTarget;

        // Check if "range" exists before trying to set it
        if (HasAnimatorParameter("range"))
        {
            animator.SetBool("range", hasLaserTarget);
        }
        if (HasAnimatorParameter("fire"))
        {
           StartCoroutine(Fire(hasHellTarget));
        }
        if (HasAnimatorParameter("fires"))
        {
            StartCoroutine(Fires(hasHellTarget));
        }
        //if (HasAnimatorParameter("fire"))
        //{
        //    if(damageable._health <= 300)
        //    {
        //        animator.SetBool("fire", hasFireTarget);
        //        StartCoroutine(hellNadoActivate());
        //    }

        //}
    }

    private IEnumerator Fire(bool fire)
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("fire",fire );
    }

    private IEnumerator Fires(bool fire)
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("fires", fire);
    }

    private bool HasAnimatorParameter(string paramName)
{
    foreach (AnimatorControllerParameter param in animator.parameters)
    {
        if (param.name == paramName)
            return true;
    }
    return false;
}

    private void FixedUpdate()
    {
        if (touchingDirections.IsOnWall && touchingDirections.IsGrounded && !isLastLvl3boss)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            Debug.Log("Flipping direction to left");
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            Debug.Log("Flipping direction to right");
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current direction is unknown");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y);
    }

    public void OnNoCliffDetected()
    {
        if (touchingDirections.IsGrounded && !isLastLvl3boss)
        {
            FlipDirection();
        }
    }

    // public IEnumerator hellNadoActivate()
    //{
    //    HellNados.SetActive(true);
    //    yield return new WaitForSeconds(2f);
    //    animator.SetBool("fire", false);
    //    HellNados.SetActive(false);
    //}
  
}
