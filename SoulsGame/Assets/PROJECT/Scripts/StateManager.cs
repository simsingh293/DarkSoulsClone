using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2;
    public float runSpeed = 3.5f;
    public float rotateSpeed = 5;
    public float distGround = 0.5f;

    [Header("States")]
    public bool onGround;
    public bool run;
    public bool lockOn;
    public bool attacking;
    public bool canMove;

    [Header("Inputs")]
    public float vertical;
    public float horizontal;
    public float moveAmount;
    public Vector3 moveDir;
    public bool rt, rb, lt, lb;

    [Header("Components")]
    public GameObject _activeModel;
    public Animator _anim;
    public Rigidbody _rb;
    public AnimatorHook a_hook;

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

        a_hook = _activeModel.AddComponent<AnimatorHook>();
        a_hook.Init(this);

        gameObject.layer = 8;
        ignoreLayers = ~(1 << 9);

        _anim.SetBool("OnGround", true);
    }

    public void FixedTick(float d)
    {
        delta = d;

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

        if (!lockOn)
        {
            Vector3 targetDir = moveDir;
            targetDir.y = 0;
            if (targetDir == Vector3.zero)
            {
                targetDir = transform.forward;
            }
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, delta * moveAmount * rotateSpeed);
            transform.rotation = targetRotation; 
        }

        HandleMovementAnimations();



    }

    public void Tick(float d)
    {
        delta = d;
        onGround = OnGround();

        _anim.SetBool("OnGround", onGround);
    }


    public void DetectAction()
    {
        if(!canMove) { return;  }



        if(!rb && !rt && !lb && !lt) { return; }

        string targetAnim = null;

        if (rb) { targetAnim = "OH_Slash 1"; }
        if (rt) { targetAnim = "OH_Slash 2"; }
        if (lb) { targetAnim = "OH_Slash 3"; }
        if (lt) { targetAnim = "TH_Slash 1"; }

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





    void HandleMovementAnimations()
    {
        _anim.SetBool("Running", run);
        _anim.SetFloat("Vertical", moveAmount, 0.4f, delta);
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
