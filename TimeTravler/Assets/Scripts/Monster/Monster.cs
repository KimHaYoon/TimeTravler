using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Player player;
    public GameObject DropItem;
    private MonsterInfo monsterInfo;
    private GameObject monsterUI;
    private SkillManager skillManager;

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private SpriteRenderer monsterSpriteRenderer;
    
    Coroutine corMove;
    Coroutine corAttack;
    Coroutine corHurt;

    //MonsterManager
    public int monsterNum;//몬스터 번호
    public int mNum;//몬스터 개인번호
    public int monsterBoss; //몬스터의 종류 확인 0 일반 1 중간 2 최종

    //MonsterInfo
    public bool moveType;//true 이동 false 고정
    public bool firstAttack;//true 선공 false 비선공
    public float attackTimeValue;//공격시간
    public int hp;//최대체력
    public int power;//공격력
    public int defence;//방어력
    public int dex;//치명타
    public float moveSpeed = 1f;//이동속도
    public Vector3 attackEffectPos;//몬스터 AttackEffect 위치
    public string dropItem;//1,44,2,33,5,6~
    
    private Vector2 colliderSize;//플레이어 점프시 MonsterSight콜라이더 변환용
    public int currentHp;//현재체력
    public float attackTime;//공격시간
    private bool first_Attack;//선공타입
    private bool move_Type;//이동타입
    public int movementFlag = 0;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
    public bool direction = true;//몬스터 방향 true = Left, false = Right
    public bool superArmor = false;//몬스터 무적true = 무적, false = 무적해제
    private bool move = true;//이동중여부 true = 이동, false = 이동x
    private bool attack = false;//공격여부 true = 공격, false = 공격x
    private bool hurt = false;//피격여부 ture = 피격, false = 피격x
    private bool knockBack = false;
    private bool noMove = true;//이동가능여부 true = 이동x false = 이동
    public bool die = false;//죽음여부 ture = 죽음, false = 살아있음
    private bool checkMoveCor = false;//ChangeMovement실행여부 true = 실행, false = 실행x
    private bool checkAttackCor = false;//AttackMovement실행여부 true = 실행, false = 실행x
    public bool attackSight = false;//공격사정거리 true = 범위o, false = 범위밖
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        monsterSpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        skillManager = GetComponent<SkillManager>();


        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>(), true);//몬스터와 플레이어 충돌무시
        Physics2D.IgnoreLayerCollision(11, 11);//몬스터끼리 충돌무시

        monsterUI = transform.parent.Find("MonsterUI").gameObject;
        monsterInfo = GameObject.Find("MonsterInfo").GetComponent<MonsterInfo>();
        monsterInfo.SetMonster(gameObject, monsterUI);//몬스터 정보 설정
        
        colliderSize = transform.parent.Find("MonsterSight").GetComponent<BoxCollider2D>().size;//MonsterSight콜라이더박스 기억
        monsterSpriteRenderer.color = new Color32(255, 255, 255, 0);//시작시 투명도 0
        StartCoroutine(FadeIn());//몬스터 생성시 슈퍼아머 FadeIn코루틴 (2초)
        currentHp = hp;//현재 체력을 최대체력으로 맞춤
        first_Attack = firstAttack;//선공타입 기억
        move_Type = moveType;//이동타입 기억
        attackTime = attackTimeValue;
        if (move_Type)//이동몹
        {
            corMove = StartCoroutine(ChangeMovement());//몬스터 이동AI (3초)
            checkMoveCor = true;
        } 
        else//고정몹
            movementFlag = UnityEngine.Random.Range(3, 5);//(3,4) 0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
    }


    void FixedUpdate()
    {
        if (noMove) movementFlag = 0;
        Move();//이동
        CheckHp();//체력확인
    }
    
    void Move()//이동
    {
        Vector3 moveVelocity = Vector3.zero;
        Vector3 theScale = transform.localScale;//현재 scale기억
        switch (movementFlag)//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
        {
            case 0:
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
                moveVelocity = Vector3.zero;//(0, 0, 0)
                break;
            case 1:
                myRigidbody.velocity = new Vector2(-1, myRigidbody.velocity.y);
                moveVelocity = Vector3.left;//(-1, 0, 0)
                direction = true;//몬스터 방향 true = Left, false = Right
                break;
            case 2:
                myRigidbody.velocity = new Vector2(1, myRigidbody.velocity.y);
                moveVelocity = Vector3.right;//(1, 0, 0)
                direction = false;//몬스터 방향 true = Left, false = Right
                break;
            case 3:
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
                moveVelocity = Vector3.zero;//(0, 0, 0)
                direction = true;//몬스터 방향 true = Left, false = Right
                break;
            case 4:
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
                moveVelocity = Vector3.zero;//(0, 0, 0)
                direction = false;//몬스터 방향 true = Left, false = Right
                break;
        }
        if (direction)//몬스터 방향 true = Left, false = Right
        {
            if (theScale.x < 0)//양수면 왼쪽, 음수면 오른쪽
            {
                theScale.x *= -1;//왼쪽방향인데 이미지가 오른쪽일때
            }
        }
        else
        {
            if (theScale.x > 0)//양수면 왼쪽, 음수면 오른쪽
            {
                theScale.x *= -1;//오른쪽방향인데 이미지가 왼쪽일때
            }
        }
        transform.localScale = theScale;//scale 수정(방향맞춰서)
        transform.position += moveVelocity * moveSpeed * Time.deltaTime;//좌표이동
    }
    
    public void TargetIn(Collider2D other)//MonsterSight안에 들어왔을때
    {
        if (other.gameObject.tag == "Player")
        {
            if (first_Attack)//선공 or 체력달았을 때
            {
                if (player.transform.position.x < transform.position.x)
                {
                    if (move_Type)//이동
                        movementFlag = 1;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                    else
                        movementFlag = 3;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                }
                else
                {
                    if (move_Type)//이동
                        movementFlag = 2;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                    else
                        movementFlag = 4;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                }
                if (move_Type)//이동
                {
                    if (!checkMoveCor && !noMove)
                    {
                        corMove = StartCoroutine(ChangeMovement());//몬스터 이동AI (3초)
                        checkMoveCor = true;
                    }
                }
                attack = false;//공격여부 true = 공격, false = 공격x
            }
        }
    }

    public void TargetStay(Collider2D other)//MonsterSight안에 있을때
    {
        if (other.gameObject.tag == "Player")
        {
            if (first_Attack)//선공 or 체력달았을 때
            {
                BoxCollider2D monster = transform.parent.Find("MonsterSight").GetComponent<BoxCollider2D>();

                if (!player.isPassGround && !player.isNoPassGround)//플레이어 점프시
                    monster.size = new Vector2(colliderSize.x, colliderSize.y * 4);//MonsterSight y축만 4배
                else//플레이어 땅에있음
                    monster.size = colliderSize;//MonsterSight 원래대로
                
                //트리거 계속 실행시킬 꼼수
                Vector3 pos = transform.position;
                pos.y += 0.0001f;
                transform.position = pos;
                //트리거 계속 실행시킬 꼼수

                if (attackSight)//사정거리 안
                {
                    if (player.transform.position.x < transform.position.x)//위치  플레이어 - 몬스터
                        movementFlag = 3;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                    else//위치  몬스터 - 플레이어
                        movementFlag = 4;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                    if (!checkAttackCor)//AttackMovement실행여부 true = 실행, false = 실행x
                    {
                        attack = true;//공격여부 true = 공격, false = 공격x
                        checkAttackCor = true;//AttackMovement실행여부 true = 실행, false = 실행x

                        if (monsterBoss == 0)
                            corAttack = StartCoroutine(AttackTimer());//공격 딜레이 코루틴(attackTime)
                        else
                            corAttack = StartCoroutine(BossAttackEffect());
                        if (checkMoveCor)
                        {
                            StopCoroutine(corMove);
                            checkMoveCor = false;
                        }
                    }
                }
                else if (!checkAttackCor)//사정거리 밖
                {
                    if (move)
                    {
                        if (player.transform.position.x < transform.position.x)//위치  플레이어 - 몬스터
                        {
                            if (move_Type)//이동식
                                movementFlag = 1;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                            else
                                movementFlag = 3;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                        }
                        else//위치  몬스터 - 플레이어
                        {
                            if (move_Type)//이동식
                                movementFlag = 2;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                            else
                                movementFlag = 4;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                        }
                        if (move_Type)//이동식
                        {
                            if (!checkMoveCor && !noMove)
                            {
                                corMove = StartCoroutine(ChangeMovement());//몬스터 이동AI (3초)
                                checkMoveCor = true;
                            }
                        }
                    }
                    attack = false;//공격여부 true = 공격, false = 공격x
                }
            }
        }
    }

    public void TargetOut(Collider2D other)//MonsterSight 나갔을때
    {
        if (other.gameObject.tag == "Player")
        {
            if (moveType)//이동식
            {
                if (currentHp < hp && !checkMoveCor)//체력 달아있음
                {
                    corMove = StartCoroutine(ChangeMovement());//몬스터 이동AI (3초)
                    checkMoveCor = true;//ChangeMovement실행여부 true = 실행, false = 실행x
                }
                if (!firstAttack)//체력 달아있는 상태였다면
                    first_Attack = firstAttack;//원래 선공타입으로 변경
            }
            currentHp = hp;//체력 회복
            if (checkAttackCor)//AttackTimer실행여부 true = 실행, false = 실행x
            {
                if (monsterBoss == 0)
                    StopCoroutine(AttackTimer());//AttackTimer코루틴 종료
                else
                    StopCoroutine(BossAttackEffect());
                attack = false;//공격여부 true = 공격, false = 공격x
                checkAttackCor = false;//AttackTimer실행여부 true = 실행, false = 실행x
            }
        }
    }

    private void CheckHp()//체력확인
    {
        if (!die)//죽음여부 ture = 죽음, false = 살아있음
        {
            if (currentHp <= 0)//현재 체력이 0이하(사망)
            {
                monsterSpriteRenderer.color = new Color32(255, 255, 255, 255);//투명도 원상복귀
                die = true;//죽음여부 ture = 죽음, false = 살아있음
                hurt = false;//피격여부 ture = 피격, false = 피격x
                move = false;
                movementFlag = 0;//0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
                first_Attack = false;
                move_Type = false;
                attack = false;//공격여부 true = 공격, false = 공격x
                knockBack = false;
                checkMoveCor = true;//ChangeMovement실행여부 true = 실행, false = 실행x
                checkAttackCor = true;//AttackMovement실행여부 true = 실행, false = 실행x
                
                if (move_Type)//이동식
                    StopCoroutine(corMove);//ChangeMovement코루틴 종료
                StopCoroutine(corHurt);//HurtTimer코루틴 종료
                if (attackTime != 0)
                {
                    if (monsterBoss == 0)
                        StopCoroutine(AttackTimer());//AttackTimer코루틴 종료
                    else
                        StopCoroutine(BossAttackEffect());
                }
                myAnimator.Play("Die");
                transform.Find("MonsterCollider").GetComponent<MonsterCollider>().DestroyObject();//MonsterCollider 삭제
                monsterUI.SetActive(false);//몬스터UI안보이게
            }
            if (currentHp < hp)//체력이 달아있으면
                first_Attack = true;//선공으로 변경
        }
        else
        {
            if (move_Type)//이동식
                StopCoroutine(corMove);//ChangeMovement코루틴 종료
            movementFlag = 0;
            myAnimator.Play("Die");
        }
    }

    public void Hurt(GameObject owner, bool knockBack, bool cri)//Player에서 호출
    {
        if (!hurt)//피격여부 ture = 피격, false = 피격x
        {
            CreateDamageUI(gameObject, owner, true, false, cri);
            move_Type = false;
            hurt = true;//피격여부 ture = 피격, false = 피격x
            if(!attack && !die)
                myAnimator.Play("Hurt");
            corHurt = StartCoroutine(HurtTimer());//피격시 투명도 코루틴 (0.5초)
        }
    }
    
    public void KnockBackHurt(GameObject owner, bool cri)
    {
        if (!knockBack)
        {
            Hurt(owner, true, cri);
            knockBack = true;
            StartCoroutine(KnockBackTimer());
        }
        else
        {
            Hurt(owner, false, cri);
        }
    }
    

    private bool Probability(int probability)//드랍아이템 확률함수
    {
        if (probability >= UnityEngine.Random.Range(0, 100))
            return true;
        else
            return false;
    }

    private void DestroyMonster()//Monster_Die애니메이션에서 호출
    {
        transform.parent.transform.parent.GetComponent<MonsterManager>().DestroyMonster(mNum);//MonsterManager에 본인 번호 전송
        if (!transform.parent.transform.parent.GetComponent<MonsterManager>().dropItem) return;
        string[] stringItem = dropItem.Split(',');//','단위로 분할
        for (int i = 0; i< stringItem.Length; i++)
        {
            if (Probability(Convert.ToInt32(stringItem[i].Substring(5, 2))))
            {
                GameObject item = Instantiate(Resources.Load("Item/Prefabs/DropItem")) as GameObject;
                item.GetComponent<DropItem>().item = stringItem[i].Substring(0, 5) + "01";
                item.GetComponent<Transform>().position = new Vector3(transform.position.x + UnityEngine.Random.Range(0f, 0.05f), transform.position.y + 0.2f, 0);//몬스터 위치로 이동
            }
        }
    }

    private void CreateAttackEffect()//AttackEffect 애니메이션에서 호출
    {
        if(monsterBoss == 0)
        {
            GameObject AttackEffect = Instantiate(Resources.Load("Monster/Prefabs/MonsterAttackEffect")) as GameObject;//몬스터공격이펙트 오브젝트생성
            AttackEffect.GetComponent<MonsterAttackEffect>().monsterNum = monsterNum;//몬스터 번호 동기화
            AttackEffect.GetComponent<MonsterAttackEffect>().setPos = attackEffectPos;//몬스터 AttackEffect 위치
            CreateDamageUI(player.gameObject, gameObject, false, false, true);

        }
        else
        {
            switch (skillManager.randomSkill)
            {
                case 0:
                    skillManager.UsingBuffer(skillManager.randomEffect);
                    break;

                case 1:
                    skillManager.UsingTargeting(skillManager.randomEffect);
                    break;

                case 2:
                    skillManager.UsingWideArea(skillManager.randomEffect);
                    break;

                case 3:
                    skillManager.CallMonster(skillManager.randomSubMonsterNum, skillManager.subMonMakeAmount);
                    break;

            }
        }
    }

    private void EndAttack()
    {
        attack = false;
        StartCoroutine(GoMove());//몬스터 공격후 이동까지 걸리는 GoMove코루틴 (0.5초)
    }

    private void CreateDamageUI(GameObject target, GameObject owner, bool who, bool knockBack, bool cri)//false player true monster
    {
        GameObject damageUI = Instantiate(Resources.Load("UI/Prefabs/DamageUI")) as GameObject;//데미지UI 오브젝트생성
        damageUI.GetComponent<DamageUI>().SetDamage(target, owner, who, knockBack, cri);//false player true monster
    }

    IEnumerator GoMove()//몬스터 공격후 이동까지 걸리는 GoMove코루틴 (0.5초)
    {
        yield return new WaitForSeconds(0.5f);
        move = true;
    }

    IEnumerator FadeIn()//몬스터 생성시 슈퍼아머 FadeIn코루틴 (2초)
    {
        int time = 1;
        noMove = true;
        superArmor = true;//슈퍼아머 설정
        checkAttackCor = true;//몬스터 공격2초간 x
        attack = true;
        while (time <= 20)
        {
            monsterSpriteRenderer.color = new Color32(255, 255, 255, (byte)(time * 12));//투명도 12 * (1~20)
            yield return new WaitForSeconds(0.1f);
            time++;
        }
        monsterSpriteRenderer.color = new Color32(255, 255, 255, 255);//투명도 원상복귀
        superArmor = false;//슈퍼아머 해제
        checkAttackCor = false;//몬스터 공격2초간 x
        attack = false;
        noMove = false;
    }

    IEnumerator ChangeMovement()//몬스터 이동AI (3초)
    {
        
        yield return new WaitForSeconds(3f);
        movementFlag = UnityEngine.Random.Range(0, 3);//(0, 1, 2) 0 Idle, 1 Left이동, 2 Right이동, 3 LeftIdle, 4 RightIdle
        if (noMove)
        {
            movementFlag = 0;
        }
        if (movementFlag == 0)
            myAnimator.Play("Idle");
        else
            myAnimator.Play("Move");
        corMove = StartCoroutine(ChangeMovement());//몬스터 이동AI (3초)
    }

    IEnumerator AttackTimer()//공격 딜레이 코루틴(attackTimeValue)
    {
        if (attackTime == 0)//AttackEffect가 없으면
        {
            checkAttackCor = false;
            yield break;
        }
        move = false;
        if (attack)//공격여부 true = 공격, false = 공격x
            myAnimator.Play("Attack");

        if (!(Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "AttackEffect") as RuntimeAnimatorController))//AttackEffect존재여부 확인
            CreateDamageUI(player.gameObject, gameObject, false, false, true);

        yield return new WaitForSeconds(attackTimeValue);
        checkAttackCor = false;
    }

    IEnumerator BossAttackEffect()//공격 딜레이 코루틴(attackTimeValue)
    {
        move = false;

        if (attack)//공격여부 true = 공격, false = 공격x
            myAnimator.Play("Attack");
        skillManager.SelectattackTime();
        if (attack)//공격여부 true = 공격, false = 공격x
            myAnimator.Play("Attack");
        yield return new WaitForSeconds(attackTime);

        checkAttackCor = false;
    }

    IEnumerator HurtTimer()//피격시 투명도 코루틴 (0.5초)
    {
        if (die) yield break;
        for(int i = 0; i<10; i++)
        {
            if(i % 2 == 0)
                monsterSpriteRenderer.color = new Color32(255, 255, 255, 100);//투명도100
            else
                monsterSpriteRenderer.color = new Color32(255, 255, 255, 255);//투명도255
            yield return new WaitForSeconds(0.05f);
        }
        hurt = false;//피격여부 ture = 피격, false = 피격x
        move_Type = moveType;
        myAnimator.Play("Idle");
    }

    IEnumerator KnockBackTimer()//
    {
        yield return new WaitForSeconds(1f);
        knockBack = false;
    }
}
