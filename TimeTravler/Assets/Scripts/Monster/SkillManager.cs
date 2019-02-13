using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 보스 몬스터 체력 변경
    // 몬스터 hp 600, 1page -> 600~301 2page -> 300~1



    public int pageNum; // 몬스터 hp 페이지
    public int skillAmount = 4; // 스킬 갯수
    public int effectAmount; //페이지 마다 한 스킬당 이펙트 갯수(몬스터 소환 제외) 중보 2, 최보 4
    public int randomSkill; //랜덤하게 뽑은 스킬번호가 담길 변수
    public int randomEffect; //랜덤하게 뽑은 이펙트 번호가 담길 변수
    public int randomSubMonsterNum; // 랜덤하게 뽑은 몬스터 번호가 담길 변수 중보는 3종류 중 하나 최보는 6종류 중 하나 뽑음
    public int subMonMakeAmount; // 서브몬스터가 한 번 소환 될때 불리는 양 중보 1페이지 3, 2페이지 2
                                 // 최보 1페이지 6, 2페이지 4, 3페이지 3, 4페이지 2
    public int monsterNum; // 몬스터 스크립트에서 받아온 현재 보스 몬스터의 번호

    public int monsterBoss; //0은 일반 몬스터 1은 중간 보스 2는 최종보스 
    private string[,] effectPos;


    public GameObject player;
    public GameObject bufferSkill; //스킬번호 0
    public GameObject targeting; //1
    public GameObject[] wideArea; //2
    public GameObject callMonster; //3
    public Vector3 dir; //광역 스킬 생성 간격
    public Vector3 widePos; //광역 스킬 생성 위치
    private Player ScPlayer;
    private Monster monsterBS;
    
    void Awake()
    {
        effectPos = new string[6, 5]
        {
            { "center", "center", "center", "center", "bottom" },
            { "center", "center", "center", "center", "bottom" },
            { "center", "center", "bottom", "center", "bottom" },
            { "center", "center", "center", "center", "bottom" },
            { "center", "center", "bottom", "center", "bottom" },
            { "center", "center", "center", "center", "bottom" }
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterBS = GetComponent<Monster>();
       
        player = GameObject.Find("Player");
        ScPlayer = player.GetComponent<Player>();
    }
    public void SkillInfo(int num)
    {
        monsterBoss = num;
        if (monsterBoss == 2)
            effectAmount = 4;
        else if (monsterBoss == 1)
            effectAmount = 2;
    }

    public void SelectattackTime() // 보스들의 공격 딜레이 시간 계산
    {

        pageNum = GetPageNum(monsterBoss);
        SelectkillandEffect(pageNum);

        if (monsterBoss == 2)
        {//몬스터 어텍타이머 다 수정
            if (pageNum == 1) monsterBS.attackTime = 2f;
            else if (pageNum == 2) monsterBS.attackTime = 3f;
            else if (pageNum == 3) monsterBS.attackTime = 5f;
        }
        else if (monsterBoss == 1)
        {
            if (pageNum == 1) monsterBS.attackTime = monsterBS.attackTimeValue - 1f;
        } 
    }


    public void SelectkillandEffect(int pageNum) // 스킬과 이펙트 랜덤 선택
    {
        monsterNum = GetComponent<Monster>().monsterNum;
        randomSkill = UnityEngine.Random.Range(0, skillAmount); //0~3번까지 스킬 선택
        randomEffect = UnityEngine.Random.Range(0, effectAmount); // 해당 스킬의 effect 선택 0,1
        
        // 중보 일시 자신의 테마 서브 몬스터 4개 중 하나를 선택하고(submonnum 배열의 인덱스만 선택) 페이지에 맞는 몬스터 갯수(getpagenum함수에서) 생성
        // 최보 일시 중간 보스 몬스터 6개 중 하나를 선택하고 페이지에 맞는 몬스터 갯수 생성
        if (monsterBoss == 2)
        {
            int[] middleBossNum = {4,8,12,16,20,24};
            randomSubMonsterNum = middleBossNum[UnityEngine.Random.Range(0, middleBossNum.Length)];
        }
        else if (monsterBoss == 1)
        {
            randomSubMonsterNum = UnityEngine.Random.Range(monsterNum - 3, monsterNum);
        }


    }


    public int GetPageNum(int monsterBoss) // 보스들의 체력 페이지 구함
    {
        if (monsterBoss == 2)
        {
            if ((monsterBS.currentHp / monsterBS.hp) > 0.75)//full에 가까움  
            {
                pageNum = 4;
                subMonMakeAmount = 2;
            }
            else if ((monsterBS.currentHp / monsterBS.hp) > 0.5)
            {
                pageNum = 3;
                subMonMakeAmount = 3;
            }
            else if ((monsterBS.currentHp / monsterBS.hp) > 0.25)
            {
                pageNum = 2;
                subMonMakeAmount = 4;
            }
            else//empty에 가까움
            {
                pageNum = 1;
                subMonMakeAmount = 6;
            }
            
        }
        else if (monsterBoss == 1)
        {
            if ((monsterBS.currentHp / monsterBS.hp) > 0.5) //full에 가까움
            {
                pageNum = 2;
                subMonMakeAmount = 2;
            }

            else if ((monsterBS.currentHp / monsterBS.hp) < 0.5) //empty에 가까움
            {
                pageNum = 1;
                subMonMakeAmount = 3;
            }
        }
        
        return pageNum;
    }


    public void UsingBuffer(int effectNum) //버퍼 스킬 오브젝트 생성
    {
        bufferSkill = Instantiate(Resources.Load("Monster/Prefabs/SkillBuffer")) as GameObject;
        bufferSkill.transform.parent = transform;
        bufferSkill.GetComponent<SkillBuffer>().monsterNum = GetComponent<Monster>().monsterNum;
        bufferSkill.GetComponent<SkillBuffer>().effectNum = randomEffect; // 스크립트에 접근, 리소스 에니메이터에 4단위로 애니메이터 집어 넣어 놓기
                                                                  // effectNum이 0이면 버퍼, 1이면 디버퍼  
        bufferSkill.GetComponent<SkillBuffer>().setPos = SetMonAttackEffectPos(effectPos[GetComponent<Monster>().monsterNum / 4 -1, randomEffect]);//몬스터 AttackEffect 위치

        if (randomEffect == 0)
            GetComponent<Monster>().SetBuf(UnityEngine.Random.Range(0, 3), 0, 0.5f, 5f);
        else
            player.GetComponent<Player>().SetBuf(UnityEngine.Random.Range(0, 3), 1, 0.5f, 5f);
    }


    public void UsingTargeting(int effectNum) //타겟팅 스킬 오브젝트 생성
    {
        targeting = Instantiate(Resources.Load("Monster/Prefabs/SkillTargeting")) as GameObject;
        targeting.transform.parent = transform;
        targeting.GetComponent<SkillTargeting>().monsterNum = GetComponent<Monster>().monsterNum;
        targeting.GetComponent<SkillTargeting>().effectNum = randomEffect;
        targeting.GetComponent<SkillTargeting>().setPos = SetMonAttackEffectPos(effectPos[GetComponent<Monster>().monsterNum / 4 - 1, 2 + randomEffect]);
        
    }


    public void UsingWideArea(int effectNum) // 광역 스킬 오브젝트 생성
    {
        wideArea = new GameObject[5];
        widePos = transform.position + new Vector3(-9, 0, 0);
        dir = new Vector3(3,0,0);

        for (int i = 0; i < wideArea.Length; i++) //5개 생성
        {
            wideArea[i] = Instantiate(Resources.Load("Monster/Prefabs/SkillWideAreaStart"), dir, Quaternion.identity) as GameObject;
            wideArea[i].GetComponent<SkillWideAreaStart>().player = player.GetComponent<Player>();
            wideArea[i].GetComponent<SkillWideAreaStart>().monster = gameObject;
            wideArea[i].GetComponent<SkillWideAreaStart>().monsterNum = GetComponent<Monster>().monsterNum;
            wideArea[i].GetComponent<SkillWideAreaStart>().effectNum = 0;
            wideArea[i].GetComponent<SkillWideAreaStart>().setPos = SetMonAttackEffectPos(effectPos[GetComponent<Monster>().monsterNum / 4 -1, 4]);
            widePos = widePos + dir;
            wideArea[i].GetComponent<SkillWideAreaStart>().widePos = widePos;
        }

    }


    public void CallMonster(int ransdomSubMonsterNum, int subMonMakeAmount) // 서브 몬스터 소환 스킬 오브젝트 생성
    {
        string[] pos = new string[subMonMakeAmount];       
        callMonster = Instantiate(Resources.Load("Monster/Prefabs/MonsterManager")) as GameObject;
        callMonster.GetComponent<MonsterManager>().monsterNum = randomSubMonsterNum;

        if (monsterBoss == 2)
            callMonster.GetComponent<MonsterManager>().monsterBoss = 1;
        else if (monsterBoss == 1)
            callMonster.GetComponent<MonsterManager>().monsterBoss = 0;

        callMonster.GetComponent<MonsterManager>().monsterCount = subMonMakeAmount;


        for(int i = 0; i < subMonMakeAmount; i++)
                pos[i] = Convert.ToString(transform.position.x + UnityEngine.Random.Range(-2f, 2f)) + " " + Convert.ToString(transform.position.y);


        callMonster.GetComponent<MonsterManager>().monsterPos = pos;
        callMonster.GetComponent<MonsterManager>().responseTime = 0;
        callMonster.GetComponent<MonsterManager>().dropItem = false;

    }

    private Vector3 SetMonAttackEffectPos(string position)
    {
        switch (position)
        {
            case "center":
                return new Vector3(0, 0.2f, 0);
            case "bottom":
                return new Vector3(0, -0.4f, 0);
        }
        return new Vector3(0, 0.2f, 0);
    }
}
