using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Coroutine corAttack;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;


    private SpriteRenderer BodySpriteRenderer;
    private SpriteRenderer HeadSpriteRenderer;
    private SpriteRenderer HelmetSpriteRenderer;
    private SpriteRenderer SwordSpriteRenderer;
    private SpriteRenderer ShieldSpriteRenderer;
    private SpriteRenderer LeftLegSpriteRenderer;
    private SpriteRenderer RightLegSpriteRenderer;


    [SerializeField]
    private float movementSpeed;
    //stat
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private int extraJumpsValue;
    [SerializeField]
    private float jumpForce;
    
    
    



    //move
    public bool facingLeft = true;

    //jump
    public bool isPassGround;
    public bool isNoPassGround;
    private int extraJumps;
    private bool downJump = false;
    private bool isJump = false;
    private bool isAttack = false;
    private int weapon = 1;

    private float checkRadius = 0.1f;
    public LayerMask noPassGround;
    public LayerMask passGround;

    //hurt
    public bool knockBack = false;
    private bool superArmor = false;//캐릭터 무적(true = 무적, false = 무적해제)

    //stat
    public int Hp = 20000;
    public int currentHp;
    public float power = 20;
    public float defence;
    public float dex;
    public float _power;
    public float _defence;
    public float _dex;

    private BufferUI bf;

    public void Consume(bool onoff, string item, int type, int opt1, int opt2)
    {
        bf = transform.parent.transform.Find("BufferUI").GetComponent<BufferUI>();
        if (onoff)//착용
        {
            switch (type)
            {
                case 1://모자
                    HelmetSpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    defence += opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence += opt1;
                    return;
                case 2://갑옷
                    BodySpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    defence += opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence += opt1;
                    return;
                case 3://신발
                    defence += opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence += opt1;
                    return;
                case 4://무기
                    SwordSpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    weapon = Convert.ToInt32(item.Substring(2, 1));//무기변경함
                                                                   //퀵슬롯 바껴야됨


                    power += opt1 * (bf.buf[0, 0] + bf.buf[0, 1]);
                    dex += opt1 * (bf.buf[2, 0] + bf.buf[2, 1]);
                    _power += opt1;
                    return;
                case 5://방패
                    ShieldSpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    defence += opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence += opt1;
                    return;
                case 6://회복물약
                    currentHp += opt1;
                    if (currentHp > Hp)
                        currentHp = Hp;

                    //opt1 체력회복량
                    return;




                //opt1 체력회복량
                //opt2 회복시간

                case 7://버프
                    return;
                case 8://버프
                    return;
                case 9://버프
                    return;
                case 10://버프
                    return;
                case 11://버프
                    return;

            }
        }
        else//제거
        {
            switch (type)
            {
                case 1://모자
                    HelmetSpriteRenderer.sprite = (Sprite)Resources.Load("None", typeof(Sprite));
                    defence -= opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence -= opt1;
                    return;
                case 2://갑옷
                    BodySpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/1201", typeof(Sprite));
                    defence -= opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence -= opt1;
                    return;
                case 3://신발
                    defence -= opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence -= opt1;
                    return;
                case 4://무기
                    SwordSpriteRenderer.sprite = (Sprite)Resources.Load("None", typeof(Sprite));
                    power -= opt1 * (bf.buf[0, 0] + bf.buf[0, 1]);
                    dex -= opt1 * (bf.buf[2, 0] + bf.buf[2, 1]);
                    _power -= opt1;
                    _dex -= opt2;
                    return;
                case 5://방패
                    ShieldSpriteRenderer.sprite = (Sprite)Resources.Load("None", typeof(Sprite));
                    defence -= opt1 * (bf.buf[1, 0] + bf.buf[1, 1]);
                    _defence -= opt1;
                    return;
            }
        }
    }


    void Awake()
    {
        Application.targetFrameRate = 40;
    }
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = transform.Find("PlayerPart").GetComponent<Animator>();

        InitSpriteRenderer();
        InitStat();

        extraJumps = extraJumpsValue;

        StartCoroutine(FadeIn());
        Inventory.instance.Add("1101201");
        Inventory.instance.Add("1103201");
        Inventory.instance.Add("1401301");
        Inventory.instance.Add("1413301");
        Inventory.instance.Add("1425301");


    }
    
    void Update()
    {
        CheckGround();
        //Debug.Log("공" + power + "    방" + defence  + "치" + dex);
    }

    void FixedUpdate()
    {
        InputKey();
        CheckHp();
        
    }
    private void InitStat()
    {
        currentHp = Hp;
        power = 20;
        defence = 10;
        dex = 50;
        _power = power;
        _defence = defence;
        _dex = dex;
    }
    private void CheckHp()
    {
        if (currentHp <= 0)
        {
            myAnimator.Play("Player_Die");
            myRigidbody.velocity = new Vector2(0, 0);
        }
    }

    private void CheckGround()
    {
        isPassGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, passGround);
        isNoPassGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, noPassGround);
        if ((isPassGround || isNoPassGround) && myRigidbody.velocity.y <= 0) // 캐릭터가 지면일때 
        {
            extraJumps = extraJumpsValue; // extrajumps를 설정한 extraJumpValue로 초기화
            isJump = false;
        }
    }


    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = 1;

            transform.Find("PlayerPart").transform.Find("Body").transform.Find("Weapon").transform.Find("WeaponPart").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Item/ItemUse/1401", typeof(Sprite));//몬스터 이름 번호에 맞춰서 설정
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon = 2;
            transform.Find("PlayerPart").transform.Find("Body").transform.Find("Weapon").transform.Find("WeaponPart").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Item/ItemUse/1411", typeof(Sprite));//몬스터 이름 번호에 맞춰서 설정

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon = 3;
            transform.Find("PlayerPart").transform.Find("Body").transform.Find("Weapon").transform.Find("WeaponPart").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Item/ItemUse/1421", typeof(Sprite));//몬스터 이름 번호에 맞춰서 설정1421
        }
        if (currentHp > 0)
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (!isAttack)
            {
                if (!downJump)
                {
                    if (isPassGround || isNoPassGround)
                    {
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);//제자리 정지
                            switch (weapon)
                            {
                                case 1:
                                    myAnimator.Play("Player_Attack1");
                                    break;
                                case 2:
                                    myAnimator.Play("Player_Attack2");
                                    break;
                                case 3:
                                    myAnimator.Play("Player_Attack3");
                                    break;
                            }
                            isAttack = true;
                            StartCoroutine(AttackTimer());
                            return;
                        }

                        if (Input.GetKey(KeyCode.S))//아래 방향키
                        {
                            myAnimator.Play("Player_Sit");//Sit 애니메이션 실행
                            myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);//제자리 정지
                            if (Input.GetKeyDown(KeyCode.LeftShift) && isPassGround)
                            {
                                downJump = true;
                                myAnimator.Play("Player_Jump");
                                StartCoroutine(DownJumpTimer());//아래로 점프시 DownJumpTimer코루틴 (0.5초)
                            }
                            return;
                        }
                        if (Input.GetKeyDown(KeyCode.LeftShift) && extraJumps > 0)//점프키를 눌렀을때 extraJumps가 0이상이면
                        {
                            myAnimator.Play("Player_Jump");
                            extraJumps--;
                            myRigidbody.velocity = Vector2.up * jumpForce;
                            isJump = true;
                            return;
                        }
                        
                    }
                    if (Input.GetKeyDown(KeyCode.LeftShift) && extraJumps > 0)//점프키를 눌렀을때 extraJumps가 0이상이면
                    {
                        myAnimator.Play("Player_Jump");
                        extraJumps--;
                        myRigidbody.velocity = Vector2.up * jumpForce;
                        isJump = true;
                    }
                    if (!isJump)
                    {
                        if (horizontal == 0)
                            myAnimator.Play("Player_Idle");
                        else
                        {
                            myAnimator.Play("Player_Move");

                        }
                    }
                }
                myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);//방향키 눌렀을때 가속도설정(이동)
                Flip(horizontal);
            }
        }
    }
    
    private void Flip(float horizontal)//캐릭터 스프라이트 방향전환용
    {
        if (horizontal != 0 && (horizontal < 0 && !facingLeft || horizontal > 0 && facingLeft))
        {
            facingLeft = !facingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void CreateDamageUI(GameObject target, GameObject owner, bool who, bool knockBack, bool cri, float damagePump)//false player true monster
    {
        GameObject damageUI = Instantiate(Resources.Load("UI/Prefabs/DamageUI")) as GameObject;//데미지UI 오브젝트생성
        damageUI.GetComponent<DamageUI>().SetDamage(target, owner, who, knockBack, cri, damagePump);//false player true monster
    }

    public void Hurt(GameObject owner, bool cri, float damagePump)
    {
        CreateDamageUI(gameObject, owner, false, false, cri, damagePump);
    }

    public void KnockBackHurt(GameObject owner, bool cri, float damagePump)
    {
        if (!knockBack)
        {
            CreateDamageUI(gameObject, owner, false, true, cri, damagePump);
            knockBack = true;
            superArmor = true;
            StartCoroutine(KnockBackTimer());
        }
        else if(!superArmor)
        {
            CreateDamageUI(gameObject, owner, false, false, cri, damagePump);
        }
    }

    private void InitSpriteRenderer()
    {
        BodySpriteRenderer = transform.Find("PlayerPart").transform.Find("Body").GetComponent<SpriteRenderer>();
        HeadSpriteRenderer = transform.Find("PlayerPart").transform.Find("Body").transform.Find("Head").GetComponent<SpriteRenderer>();
        HelmetSpriteRenderer = transform.Find("PlayerPart").transform.Find("Body").transform.Find("Head").transform.Find("Helmet").GetComponent<SpriteRenderer>();
        SwordSpriteRenderer = transform.Find("PlayerPart").transform.Find("Body").transform.Find("Weapon").transform.Find("WeaponPart").GetComponent<SpriteRenderer>();
        ShieldSpriteRenderer = transform.Find("PlayerPart").transform.Find("Body").transform.Find("Shield").GetComponent<SpriteRenderer>();
        LeftLegSpriteRenderer = transform.Find("PlayerPart").transform.Find("LeftLeg").GetComponent<SpriteRenderer>();
        RightLegSpriteRenderer = transform.Find("PlayerPart").transform.Find("RightLeg").GetComponent<SpriteRenderer>();
    }

    public void SetSpriteRenderer(byte alpha)
    {
        BodySpriteRenderer.color = new Color32(255, 255, 255, alpha);//투명도
        HeadSpriteRenderer.color = new Color32(255, 255, 255, alpha);//투명도
        HelmetSpriteRenderer.color = new Color32(255, 255, 255, alpha);//투명도
        SwordSpriteRenderer.color = new Color32(255, 255, 255, alpha);//투명도
        ShieldSpriteRenderer.color = new Color32(255, 255, 255, alpha);//투명도
        LeftLegSpriteRenderer.color = new Color32(255, 255, 255, alpha);//투명도
        RightLegSpriteRenderer.color = new Color32(255, 255, 255, alpha);//투명도
    }
    
    IEnumerator FadeIn()//몬스터 생성시 슈퍼아머 FadeIn코루틴 (2초)
    {
        int time = 1;
        superArmor = true;//슈퍼아머 설정
        // 공격2초간 x
        while (time <= 20)
        {
            SetSpriteRenderer((byte)(time * 12));
            yield return new WaitForSeconds(0.1f);
            time++;
        }
        SetSpriteRenderer(255);
        superArmor = false;//슈퍼아머 해제
    }

    IEnumerator DownJumpTimer()//아래로 점프시 DownJumpTimer코루틴 (0.5초)
    {
        Vector2 CapsuleCollider2D = GetComponent<CapsuleCollider2D>().size;
        GetComponent<CapsuleCollider2D>().size = new Vector2(0, 0);
        yield return new WaitForSeconds(0.5f);
        GetComponent<CapsuleCollider2D>().size = CapsuleCollider2D;
        for (int i = 0; i<10; i++)
        {
            if (isPassGround || isNoPassGround)
            {
                downJump = false;
                isJump = false;
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator AttackTimer()//
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle"))
            {
                isAttack = false;
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator KnockBackTimer()//
    {
        yield return new WaitForSeconds(1f);
        knockBack = false;
        superArmor = false;
    }

    public void SetBuf(int num, int type, float crease, float time)//버프류
    {
        transform.parent.transform.Find("BufferUI").GetComponent<BufferUI>().StartBuf(gameObject, true, num, type, crease, time);
    }
    
}

