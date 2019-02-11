﻿using System.Collections;
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
    private int Hp = 200;
    public int currentHp;
    public int power = 20;
    public int defence;
    public int dex;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = transform.Find("PlayerPart").GetComponent<Animator>();

        InitSpriteRenderer();
        InitStat();

        extraJumps = extraJumpsValue;

        StartCoroutine(FadeIn());

    }
    
    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }

    void FixedUpdate()
    {
        InputKey();
        CheckHp();
        //currentHp--;
        
    }
    private void InitStat()
    {
        currentHp = Hp;
        power = 20;
        defence = 10;
        dex = 50;
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
                        if (!isJump)
                        {
                            if (horizontal == 0)
                                myAnimator.Play("Player_Idle");
                            else
                            {
                                myAnimator.Play("Player_Move");
                                myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);//방향키 눌렀을때 가속도설정(이동)
                            }
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.LeftShift) && extraJumps > 0)//점프키를 눌렀을때 extraJumps가 0이상이면
                    {
                        myAnimator.Play("Player_Jump");
                        extraJumps--;
                        myRigidbody.velocity = Vector2.up * jumpForce;
                        isJump = true;
                    }
                }


                

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

    private void CreateDamageUI(GameObject target, GameObject owner, bool who, bool knockBack, bool cri)//false player true monster
    {
        GameObject damageUI = Instantiate(Resources.Load("UI/Prefabs/DamageUI")) as GameObject;//데미지UI 오브젝트생성
        damageUI.GetComponent<DamageUI>().SetDamage(target, owner, who, knockBack, cri);//false player true monster
    }

    public void Hurt(GameObject owner, bool cri)
    {
        CreateDamageUI(gameObject, owner, false, false, cri);
    }

    public void KnockBackHurt(GameObject owner, bool cri)
    {
        if (!knockBack)
        {
            CreateDamageUI(gameObject, owner, false, true, cri);
            knockBack = true;
            superArmor = true;
            StartCoroutine(KnockBackTimer());
        }
        else if(!superArmor)
        {
            CreateDamageUI(gameObject, owner, false, false, cri);
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
}
