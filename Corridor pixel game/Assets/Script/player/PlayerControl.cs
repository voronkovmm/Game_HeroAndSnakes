using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl i;

    [Header("Move")]
    [SerializeField] float _speed;
    public Vector2 _dirMove;
    public static bool isCanMove;

    [Header("Jump")]
    [SerializeField] float _jumpPower;
    bool _isJumping;
    Transform _groundCheck;
    LayerMask _groundLayer;
    Vector2 _dirGravity;

    [Header("Attack")]
    bool _isCanAttack;
    Transform _atkPoint;
    public bool iAttaking;

    Joystick _joystick;
    bool _isButtonJumpPressed;
    bool _isButtonJumpBlocked;
    public bool _isButtonAttackPressed;

    Rigidbody2D _rb;
    static Animator _anim;
    SpriteRenderer _sr;

    [SerializeField] LayerMask idamageableLayer;

    void Awake() {
        if(i == null) i = this;

        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _groundCheck = transform.Find("groundCheck");
        _groundLayer = LayerMask.GetMask("ground");
        _dirGravity = new Vector2(0, -Physics2D.gravity.y);
        _joystick = FindObjectOfType<Joystick>();
        _atkPoint = transform.Find("atkPoint");

        _dirMove = Vector2.zero;
    }

    void Start() {
        isCanMove = true;   
        _isCanAttack = true; 
    }

    void Update() {
        Jump();
        Move();
        Attack();
    }

    void FixedUpdate()
    {
        if(isCanMove)
            _rb.velocity = new Vector2(_dirMove.x * _speed, _rb.velocity.y);
        else
            _rb.velocity = Vector2.zero;
    }

    void Jump()
    {
        if(isCanMove)
        {
            if ((_isButtonJumpPressed || Input.GetKeyDown(KeyCode.Space)) && IsGrounded())
            {
                if(GameManager.isMobileDevice) 
                {
                    _isButtonJumpBlocked = true;
                    _isButtonJumpPressed = false;
                }

                _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);

                _anim.SetBool("isGrounded", false);
                _anim.SetTrigger("Jump");
                StartCoroutine(WaitIsJumping());
            }



            if(_isJumping && IsGrounded())
            {
                _isJumping = false;
                _anim.SetBool("isGrounded", true);

                _isButtonJumpBlocked = false;
            }
        }
        
    }

    IEnumerator WaitIsJumping()
    {
        yield return new WaitForSeconds(0.001f);
        _isJumping = true;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(_groundCheck.position, new Vector2(0.22f, 0.06f), CapsuleDirection2D.Horizontal, 0, _groundLayer);
    }

    void Move()
    {
        if(isCanMove)
        {
            if(GameManager.isMobileDevice)
                if(_joystick.Horizontal > 0) _dirMove.x = 1;
                else if(_joystick.Horizontal < 0) _dirMove.x = -1;
                else _dirMove.x = 0;
            else
                _dirMove.x = Input.GetAxisRaw("Horizontal");


            if(_dirMove.x != 0)
            {
                _sr.flipX = _dirMove.x > 0 ? false : true;
                _anim.SetBool("isRun", true);
            }
            else
                _anim.SetBool("isRun", false);
        }
    }

    public void ButtonJump()
    {
        if(!_isButtonJumpBlocked)
        {
            _isButtonJumpPressed = IsGrounded() ? true : false;
        }
    } 
    public void ButtonAttack() => _isButtonAttackPressed = true;

    void Attack()
    {
        if((Input.GetKeyDown(KeyCode.J) || _isButtonAttackPressed) && !_isJumping && _isCanAttack)
        {
            iAttaking = true;
            isCanMoveAndAttack(false);
            if(GameManager.isMobileDevice) _isButtonAttackPressed = false;

            _anim.SetTrigger("atk_noWeapon");
        }
    }

    void isCanMoveAndAttack(bool arg)
    {
        _isCanAttack = arg;
        isCanMove = arg;
    }
    void CanMoveAndAttackEvent()
    {
        _isCanAttack = true;
        isCanMove = true;
        iAttaking = false;
    }

    public static void StopAllMove()
    {
        isCanMove = false;
        _anim.SetBool("isRun", false);
    }

    public void EVENT_Damage()
    {
        float boxPosX = _sr.flipX ? -0.42f : 0.42f;
        Vector2 boxStart = new Vector2(transform.position.x + boxPosX, transform.position.y + 0.37f);

        RaycastHit2D hit = Physics2D.BoxCast(boxStart, new Vector2(0.41f, 0.74f), 0, Vector2.zero, 0, idamageableLayer);
        if(hit.collider != null)
            hit.collider?.GetComponent<IDamageable>()?.Damage(Player.i.Damage, GetPos());
    }

    Vector2 GetPos() => transform.position;
}