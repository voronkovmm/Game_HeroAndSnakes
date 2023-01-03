using UnityEngine;
using System.Collections;

public class Snake : Enemy
{
    int _damage = 1,
        health = 1;
    float _moveSpeed,
        _currentMoveSpeed,
        _distAgro = 8,
        _distAtk = 1,
        _distAtkSpray = 4,
        _timerAtk,
        _timeBtwAtk_0 = 3,
        _timeBtwAtk_spray = 3,
        _distToPlayer;
    
    Vector2 _moveDir;
    bool _stopMove,
        _isAttack;

    enum TypeSnake { GREEN, BLUE, RED }
    public enum State { IDLE, PATRUL, CHASE, ATTACK, ATTACK_SPRAY , DEATH }
    State _state;
    public State state
    {
        get => _state;
        set
        { 
            _stopMove = false;
            _currentMoveSpeed = _moveSpeed;
            float randomSpeed = UnityEngine.Random.Range(4f, 5f);

            switch (value)
            {
                case State.PATRUL:
                    _state = value;
                    SetAnim("move");
                    break;

                case State.IDLE:
                    _state = value;
                    SetAnim("idle");
                    _stopMove = true;
                    break;

                case State.CHASE:
                    _state = value;
                    SetAnim("move");
                    _currentMoveSpeed = randomSpeed;
                    break;

                case State.ATTACK:
                    _state = value;
                    _currentMoveSpeed = randomSpeed;
                    break;

                case State.ATTACK_SPRAY:
                    _state = value;
                    _currentMoveSpeed = randomSpeed;
                    break;

                case State.DEATH:
                    _state = value;
                    _currentMoveSpeed = randomSpeed;
                    _stopMove = true;
                    SetAnim("death");
                    StartCoroutine(Tools.DisappearingAndDestroy(_sr, 9f));
                    break;
            }
        }
    }

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] TypeSnake _type;
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Animator _anim;

    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    void Start() {
        _moveDir = Vector2.left;
        state = State.PATRUL;

        _anim.SetFloat("idle_speed", 1 + UnityEngine.Random.Range(-0.15f, 0.15f));
        _moveSpeed = _currentMoveSpeed = UnityEngine.Random.Range(2.5f, 3.5f);
    }

    void Update()
    {
        _distToPlayer = Vector2.Distance(transform.position, Player.i.GetPos());

        if(_timerAtk >= 0) _timerAtk -= Time.deltaTime;

        switch (state)
        {
            case State.PATRUL:
                Patrul();
                break;
            case State.CHASE:
                Chase();
                break;
            case State.ATTACK:
                Attack();
                break;
            case State.ATTACK_SPRAY:
                AttackSpray();
                break;
        }
    }

    void FixedUpdate()
    {
        if(!_stopMove) _rb.velocity = _moveDir * _currentMoveSpeed;
    }

    void Patrul()
    {
        if(_distToPlayer <= _distAgro) {
            state = State.CHASE;
            return;
        }

        Vector2 start = new Vector2(transform.position.x + (_moveDir.x * 0.35f), transform.position.y - 0.16f);

        if(!Physics2D.Raycast(start, Vector2.down, 1, groundLayer))
        {
            bool isMoveLeft = _moveDir.x == -1;
            _moveDir = isMoveLeft ? Vector2.right : Vector2.left;
            _sr.flipX = isMoveLeft;
        }
    }

    void Chase()
    {
        Vector2 playerPos = Player.i.GetPos();
        Vector2 snakePos = transform.position;

        if(_distToPlayer <= _distAtk)
            state = State.ATTACK;
        else if((_type == TypeSnake.BLUE || _type == TypeSnake.RED) && _distToPlayer <= _distAtkSpray)
            state = State.ATTACK_SPRAY;
        else if(_distToPlayer < _distAgro)
        {
            SetAnim("move");

            if (snakePos.x < playerPos.x)
            {
                _moveDir = Vector2.right;
                _sr.flipX = true;
            }
            else
            {
                _moveDir = Vector2.left;
                _sr.flipX = false;
            }

            Vector2 start = new Vector2(transform.position.x + (_moveDir.x * 0.35f), transform.position.y - 0.16f);
            if (!Physics2D.Raycast(start, Vector2.down, 1, groundLayer))
            {
                SetAnim("idle");
                _moveDir = Vector2.zero;
            }
        }
        else
            state = State.PATRUL;
    }

    void Attack()
    {
        Vector2 playerPos = Player.i.GetPos();
        Vector2 snakePos = transform.position;

        _stopMove = true;
        
        if(_distToPlayer > _distAtk && !_isAttack) {
            state = State.CHASE;
            return;
        }
        
        if (_timerAtk <= 0) {
            _isAttack = true;
            _timerAtk = _timeBtwAtk_0;
            _anim.SetTrigger("atk_0");
        }
        else if (!_isAttack) {
            _sr.flipX = snakePos.x < playerPos.x ? true : false;
            SetAnim("idle");
        }
    }

    void AttackSpray()
    {
        Vector2 playerPos = Player.i.GetPos();
        Vector2 snakePos = transform.position;

        _stopMove = true;
        
        if(!_isAttack)
        {
            if (_distToPlayer <= _distAtk)
            {
                state = State.ATTACK;
                return;
            }
            else if (_distToPlayer > _distAtkSpray)
            {
                state = State.CHASE;
                return;
            }
        }
        
        if (_timerAtk <= 0) {
            _isAttack = true;
            _timerAtk = _timeBtwAtk_spray;
            _anim.SetTrigger("atk_spray");
        }
        else if (!_isAttack) {
            _sr.flipX = snakePos.x < playerPos.x ? true : false;
            SetAnim("idle");
        }
    }

    void EVENT_Attack_0()
    {
        float corX = _sr.flipX ? 0.35f : -0.35f;
        Vector2 direction = _sr.flipX ? Vector2.right : Vector2.left;
        Vector2 origin = new Vector2(transform.position.x + corX, transform.position.y + 0.3f);
        
        RaycastHit2D hit = Physics2D.BoxCast(origin, new Vector2(1, 0.6f), 0, direction, 0, playerLayer);
        if(hit.collider != null)
        {
            hit.collider.GetComponent<IDamageable>().Damage(_damage, GetPos());
        }
        
    }
    void EVENT_AttackSpray()
    {
        float angle = 45f;
        Vector2 dirToTarget = Player.i.GetCenterPos() - GetCenterPos();

        float x = dirToTarget.magnitude;
        float y = dirToTarget.y;

        float angleInRadian = angle * Mathf.PI / 180;

        float v2 = (9.8f * x*x)/ (2 * (y - Mathf.Tan(angleInRadian) * x) * Mathf.Pow(Mathf.Cos(angleInRadian), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        GameObject atk = Pull.i.GetSnakeAtkSpray(GetCenterPos());
        Rigidbody2D rb = atk.GetComponent<Rigidbody2D>();

        // узнать влево или вправо
        int dirShoot = GetPos().x < Player.i.GetPos().x ? 1 : -1;
        Vector2 dir = new Vector2(dirShoot, 0.5f);
        rb.velocity = dir * v;
    }

    void SetAnim(string name)
    {
        foreach (var item in _anim.parameters)
        {
            if(item.name.Equals("idle_speed")) continue;
            _anim.SetBool(item.name, false);
        }

        if(name.Equals("idle"))
            return;
        else
            _anim.SetBool(name, true);
    }

    void EVENT_EndAnimation()
    {
        _stopMove = false;
        _isAttack = false;

        if(_distToPlayer <= _distAtk)
            state = State.ATTACK;
        else if((_type == TypeSnake.BLUE || _type == TypeSnake.RED) && _distToPlayer <= _distAtkSpray)
            state = State.ATTACK_SPRAY;
        else if(_distToPlayer < _distAgro)
            state = State.CHASE;
        else
            state = State.PATRUL;
    }


    public override void Damage(int damage, Vector2 dirImpact)
    {
        Pull.i.GetBlood(GetCenterPos(), flip: dirImpact.x < GetPos().x ? true : false);    
        Pull.i.GetBloodstain(GetPos());

        health -= damage;

        if(health == 0) Death(dirImpact);
    }

    public override Vector2 GetPos() => transform.position;
    public Vector2 GetCenterPos() => new Vector2(transform.position.x, transform.position.y + 0.3f);

    void Death(Vector2 dirImpact)
    {
        state = State.DEATH;
        Pull.i.GetBloodSplash(GetPos(), flip: GetPos().x < dirImpact.x ? false : true);

        GameObject head = Pull.i.GetSnakeHead(GetComponent<Collider2D>().bounds.max, needBack: false);
        Rigidbody2D rb = head.GetComponent<Rigidbody2D>();
        float randomPower = UnityEngine.Random.Range(3f, 10f);
        float randomTorque = UnityEngine.Random.Range(10f, 50f);
        rb.AddForce(GetPos().x < dirImpact.x ? new Vector2(-randomPower, 0.1f) : new Vector2(randomPower, 0.1f), ForceMode2D.Impulse);
        rb.AddTorque(randomTorque);

        Pull.i.GetMineral(GetCenterPos(), 20);

        StartCoroutine(Pull.i.DisappearingAndGoBack(head.GetComponent<SpriteRenderer>(), 9f));
    }


}
