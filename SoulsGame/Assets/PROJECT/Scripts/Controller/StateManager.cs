using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2;
    public float runSpeed = 3.5f;
    public float rotateSpeed = 5;
    public float distGround = 0.5f;
    public float rollSpeed = 1.0f;
    public int score = 0;
    public int Max_Health = 100;
    public int Current_Health;
    public bool EndItemFound = false;

    [Header("States")]
    public bool onGround;
    public bool run;
    public bool lockOn;
    public bool attacking;
    public bool canMove;
    public bool isTwoHanded;
    public bool poweredUp = false;
    public bool dead;
    

    [Header("Other")]
    public EnemyTarget lockOnTarget;
    public Transform lockOnTransform;
    public AnimationCurve roll_curve;
    public ParticleSystem flameParticles;

    [Header("Inputs")]
    public float vertical;
    public float horizontal;
    public float moveAmount;
    public Vector3 moveDir;
    public bool rt, rb, lt, lb;
    public bool dodgeInput;
    
    [Header("UI")]
    public Text scoreText;
    public RectTransform HealthBar;
    public RectTransform PowerIndicator;

    [Header("Components")]
    public GameObject _activeModel;
    public Animator _anim;
    public Rigidbody _rb;
    public AnimatorHook a_hook;
    public ActionManager actionManager;
    public InventoryManager inventoryManager;
    public WeaponCollisions weaponCollisions;
    public PlayerCollisions playerCollisions;

    public float delta;
    public LayerMask ignoreLayers;

    float _actionDelay;

    public void Init()
    {
        SetupAnimator();
        _rb = GetComponent<Rigidbody>();
        _rb.angularDrag = 999;
        _rb.drag = 4;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        inventoryManager = GetComponent<InventoryManager>();
        inventoryManager.Init();

        actionManager = GetComponent<ActionManager>();
        actionManager.Init(this);

        a_hook = _activeModel.AddComponent<AnimatorHook>();
        a_hook.Init(this);

        weaponCollisions = GetComponentInChildren<WeaponCollisions>();
        weaponCollisions.Init(this);

        playerCollisions = GetComponent<PlayerCollisions>();
        playerCollisions.Init(this);

        Current_Health = Max_Health;

        flameParticles.Pause();


        gameObject.layer = 8;
        ignoreLayers = ~(1 << 9);

        _anim.SetBool("OnGround", true);
    }

    public void FixedTick(float d)
    {
        delta = d;

        if (dead) { return; }

        DetectAction();

        if (attacking)
        {
            _anim.applyRootMotion = true;

            _actionDelay += delta;
            if(_actionDelay > 0.3f)
            {
                attacking = false;
                _actionDelay = 0;
            }
            else
            {
                return;
            }
        }

        canMove = _anim.GetBool("CanMove");

        if (!canMove)
        {
            return;
        }



        //a_hook.rootMotionMultiplier = 1;
        a_hook.CloseRoll();
        HandleDodges();



        _anim.applyRootMotion = false;
        if(moveAmount > 0 || !onGround )
        {
            _rb.drag = 0;
        }
        else
        {
            _rb.drag = 4;
        }


        float targetSpeed = moveSpeed;
        if (run)
        {
            targetSpeed = runSpeed;
        }


        if (onGround)
        {
            _rb.velocity = moveDir * (targetSpeed * moveAmount);
        }

        if (run)
        {
            lockOn = false;
        }

        Vector3 targetDir = (lockOn == false) ? moveDir
            : (lockOnTransform != null) ?
            lockOnTransform.position - transform.position
            : moveDir;

        targetDir.y = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * moveAmount * rotateSpeed);
        transform.rotation = targetRotation;


        _anim.SetBool("LockOn", lockOn);
        
        if(!lockOn)
        {
            HandleMovementAnimations();
        }
        else
        {
            HandleLockOnAnimations(moveDir);}



    }

    public void Tick(float d)
    {
        delta = d;

        UpdateHealthBar();
        CheckIfDead();

        if (poweredUp)
        {
            PowerIndicator.gameObject.SetActive(true);
        }
        else
        {
            PowerIndicator.gameObject.SetActive(false);

        }

        onGround = OnGround();
        _anim.SetBool("OnGround", onGround);
    }

    void UpdateHealthBar()
    {
        HealthBar.sizeDelta = new Vector2(Current_Health * 2, 30);
    }


    void HandleDodges()
    {
        if (!dodgeInput)
        {
            return;
        }

        float v = vertical;
        float h = horizontal;

        if (lockOn == false)
        {
            v = (moveAmount > 0.3f) ? 1 : 0;
            h = 0;
        }
        else
        {
            if (Mathf.Abs(v) < 0.3f)
            {
                v = 0;
            }
            if (Mathf.Abs(h) < 0.3f)
            {
                h = 0;
            }
        }

        if (v != 0)
        {
            if (moveDir == Vector3.zero)
            {
                moveDir = transform.forward;
            }

            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = targetRot; 
            a_hook.InitForRoll();
            a_hook.rootMotionMultiplier = rollSpeed;
        }
        else
        {
            a_hook.rootMotionMultiplier = 1.3f;
        }


        _anim.SetFloat("Vertical", v);
        _anim.SetFloat("Horizontal", h);

        canMove = false;
        attacking = true;
        _anim.CrossFade("Dodges", 0.2f);
    }



    public void DetectAction()
    {
        if(!canMove) { return;  }



        if(!rb && !rt && !lb && !lt) { return; }

        string targetAnim = null;



        Action slot = actionManager.GetActionSlot(this);
        if(slot == null)
        {
            return;
        }
        targetAnim = slot.targetAnimation;



        if(string.IsNullOrEmpty(targetAnim))
        {
            return;
        }

        canMove = false;
        attacking = true;
        _anim.CrossFade(targetAnim, 0.2f);
        //_rb.velocity = Vector3.zero;
    }


    public bool OnGround()
    {
        bool r = false;

        Vector3 origin = transform.position + (Vector3.up * distGround);
        Vector3 dir = -Vector3.up;
        float dist = distGround + 0.3f;

        RaycastHit hit;

        if (Physics.Raycast(origin, dir, out hit, dist, ignoreLayers))
        {
            r = true;
            Vector3 targetPosition = hit.point;
            transform.position = targetPosition;
        }


        return r;
    }


    public void HandleTwoHanded()
    {
        _anim.SetBool("TwoHanded", isTwoHanded);

        if (isTwoHanded)
        {
            actionManager.UpdateActionsTwoHanded();
            flameParticles.Play();
        }
        else
        {
            actionManager.UpdateActionsOneHanded();
            poweredUp = false;
            flameParticles.Stop();
        }
    }

    void HandleLockOnAnimations(Vector3 moveDir)
    {
        Vector3 relativeDir = transform.InverseTransformDirection(moveDir);
        float h = relativeDir.x;
        float v = relativeDir.z;

        _anim.SetFloat("Vertical", v, 0.2f, delta);
        _anim.SetFloat("Horizontal", h, 0.2f, delta);
    }


    void HandleMovementAnimations()
    {
        _anim.SetBool("Running", run);
        _anim.SetFloat("Vertical", moveAmount, 0.4f, delta);
    }

    public void ChangeHealth(int value)
    {
        if (Current_Health + value > Max_Health)
        {
            Current_Health = Max_Health;
        }
        else if (Current_Health - value < 0)
        {
            Current_Health = 0;
        }
        else
        {
            Current_Health += value;
        }
    }

    public void CheckIfDead()
    {
        if(Current_Health <= 0)
        {
            dead = true;
        }
    }

    void SetupAnimator()
    {
        if (_activeModel == null)
        {
            _anim = GetComponentInChildren<Animator>();
            if (_anim == null)
            {
                Debug.Log("No model found");
            }
            else
            {
                _activeModel = _anim.gameObject;
            }
        }

        if (_anim == null)
        {
            _anim = _activeModel.GetComponent<Animator>();
        }

        _anim.applyRootMotion = false;
    }

}
