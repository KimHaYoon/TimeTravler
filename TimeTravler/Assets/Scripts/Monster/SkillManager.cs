using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // 보스 몬스터 체력 변경
    // 몬스터 hp 600, 1page -> 600~301 2page -> 300~1


    public int randomSkill; //랜덤하게 뽑은 스킬번호가 담길 변수
    public int randomEffect; //랜덤하게 뽑은 이펙트 번호가 담길 변수
    public int randomSubMonsterNum; // 랜덤하게 뽑은 몬스터 번호가 담길 변수 중보는 3종류 중 하나 최보는 6종류 중 하나 뽑음
    private string[,] effectPos;

    //MonsterManager에서 설정
    public int monsterNum; // 몬스터 스크립트에서 받아온 현재 보스 몬스터의 번호
    public int monsterBoss; //0은 일반 몬스터 1은 중간 보스 2는 최종보스 



    //스킬 이펙트
    public GameObject bufferSkill; //스킬번호 0
    public GameObject targeting; //1
    public GameObject[] wideArea; //2
    public GameObject callMonster; //3
    public Vector3 dir; //광역 스킬 생성 간격
    public Vector3 widePos; //광역 스킬 생성 위치

    public GameObject player;
    private Player ScPlayer;
    private Monster monsterBS;
    
    void Awake()
    {
        effectPos = new string[7, 5]
        {//pos,xscale,yscale
            { "center,0.5,0.5", "center,1,1", "center,1,1", "center,1.5,1.5", "bottom,0.7,2" },
            { "center,3,3", "center,1,1", "center,1.5,1.5", "center,1,1", "bottom,1,2" },
            { "center,1,1", "center,0.5,0.5", "bottom,1,1", "center,1,1", "bottom,1,3" },
            { "center,2,2", "center,3,3", "center,1,1", "center,1,1", "bottom,2,3" },
            { "center,1,1", "center,1,1", "bottom,1,2", "center,1,1", "bottom,1,2" },
            { "center,3,3", "center,1,1", "bottom,1,1", "center,3,3", "bottom,2,4" },
            { "bottom,1,1", "center,1,1", "center,3,3", "bottom,2,4", "0,0,0"} 
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        monsterBS = GetComponent<Monster>();
       
        player = GameObject.Find("Player");
        ScPlayer = player.GetComponent<Player>();
    }
    public void MiddleBoss(int pageNum)
    {
        if (pageNum == 4 && pageNum == 3) //empty
            pageNum = 2;
        else //full
            pageNum = 1;

        randomSkill = UnityEngine.Random.Range(0, 4); //0~3번까지 스킬 선택
        randomEffect = UnityEngine.Random.Range(0, pageNum); // 해당 스킬의 effect 선택 0,1
        //Debug.Log(randomSkill);
        //Debug.Log(randomEffect);
        switch (randomSkill)
        {
            case 0:
                UsingBuffer(randomEffect);
                break;
            case 1:
                UsingTargeting(randomEffect);
                break;
            case 2:
                UsingWideArea();
                break;
            case 3:
                randomSubMonsterNum = UnityEngine.Random.Range(monsterNum - 3, monsterNum);
                //Debug.Log(randomSubMonsterNum);
                CallMonster(randomSubMonsterNum, pageNum);
                break;
        }


    }

    public int LastBossSelect(int pageNum)
    {
        if (pageNum == 4) //empty
            randomEffect = UnityEngine.Random.Range(0, pageNum + 1); //0~4번 까지 공격에 맞는 effect선택
        else //full
            randomEffect = UnityEngine.Random.Range(0, pageNum); //0~4번 까지 공격에 맞는 effect선택
        Debug.Log(randomEffect);
        return randomEffect;
    }

    public void LastBoss(int radomEffect, int pageNum)
    {

        switch (randomEffect)
        {
            case 0:
                UsingTargeting(randomEffect);
                break;
            case 1:
                UsingTargeting(randomEffect);
                break;
            case 2:
                UsingTargeting(randomEffect);
                break;
            case 3:
                UsingWideArea();
                break;
            case 4:
                int[] middleBossNum = { 4, 8, 12, 16, 20, 24 };
                randomSubMonsterNum = middleBossNum[UnityEngine.Random.Range(0, middleBossNum.Length)];
                //Debug.Log(randomSubMonsterNum);
                CallMonster(randomSubMonsterNum, pageNum);
                break;
        }
    }
    

    public void UsingBuffer(int effectNum) //버퍼 스킬 오브젝트 생성
    {
        bool tmp;
        string[] transfrom = effectPos[GetComponent<Monster>().monsterNum / 4 - 1, randomEffect].Split(',');
        bufferSkill = Instantiate(Resources.Load("Monster/Prefabs/SkillBuffer")) as GameObject;
        bufferSkill.transform.parent = transform;
        bufferSkill.GetComponent<SkillBuffer>().monsterNum = GetComponent<Monster>().monsterNum;
        bufferSkill.GetComponent<SkillBuffer>().effectNum = randomEffect; // 스크립트에 접근, 리소스 에니메이터에 4단위로 애니메이터 집어 넣어 놓기
                                                                          // effectNum이 0이면 버퍼, 1이면 디버퍼  
        if (effectNum == 0) tmp = false;
        else tmp = true;
        bufferSkill.GetComponent<SkillBuffer>().setPos = SetMonAttackEffectPos(tmp, transfrom[0]);//몬스터 AttackEffect 위치

        bufferSkill.GetComponent<SkillBuffer>().xScale = (float)Convert.ToDouble(transfrom[1]);
        bufferSkill.GetComponent<SkillBuffer>().yScale = (float)Convert.ToDouble(transfrom[2]);
        if (randomEffect == 0)
            GetComponent<Monster>().SetBuf(UnityEngine.Random.Range(0, 3), 0, 0.5f, 5f);
        else
            player.GetComponent<Player>().SetBuf(UnityEngine.Random.Range(0, 3), 1, 0.5f, 5f);
    }


    public void UsingTargeting(int effectNum) //타겟팅 스킬 오브젝트 생성
    {

        string[] transfrom;
        if(monsterBoss == 1)
            transfrom = effectPos[GetComponent<Monster>().monsterNum / 4 - 1, 2 + randomEffect].Split(',');
        else
            transfrom = effectPos[6, randomEffect].Split(',');

        targeting = Instantiate(Resources.Load("Monster/Prefabs/SkillTargeting")) as GameObject;
        targeting.transform.parent = transform;
        targeting.GetComponent<SkillTargeting>().monsterBoss = monsterBoss;
        targeting.GetComponent<SkillTargeting>().monsterNum = GetComponent<Monster>().monsterNum;
        targeting.GetComponent<SkillTargeting>().effectNum = effectNum;
        targeting.GetComponent<SkillTargeting>().setPos = SetMonAttackEffectPos(true, transfrom[0]);
        targeting.GetComponent<SkillTargeting>().xScale = (float)Convert.ToDouble(transfrom[1]);
        targeting.GetComponent<SkillTargeting>().yScale = (float)Convert.ToDouble(transfrom[2]);
    }


    public void UsingWideArea() // 광역 스킬 오브젝트 생성
    {
        string[] transfrom;

        if (monsterBoss == 1)
            transfrom = effectPos[GetComponent<Monster>().monsterNum / 4 - 1, 4].Split(',');
        else
            transfrom = effectPos[6,3].Split(',');
         
        wideArea = new GameObject[5];
        widePos = transform.position + new Vector3(-9, 0, 0);
        dir = new Vector3(3,0,0);
        for (int i = 0; i < wideArea.Length; i++) //5개 생성
        {
            wideArea[i] = Instantiate(Resources.Load("Monster/Prefabs/SkillWideAreaArrow"), dir, Quaternion.identity) as GameObject;
            wideArea[i].GetComponent<SkillWideAreaArrow>().player = player;
            wideArea[i].GetComponent<SkillWideAreaArrow>().monster = gameObject;
            //wideArea[i].GetComponent<SkillWideArea>().monsterBoss = monsterBoss;
            wideArea[i].GetComponent<SkillWideAreaArrow>().monsterNum = GetComponent<Monster>().monsterNum;
            wideArea[i].GetComponent<SkillWideAreaArrow>().setPos = SetMonAttackEffectPos(false, transfrom[0]);
            wideArea[i].GetComponent<SkillWideAreaArrow>().xScale = (float)Convert.ToDouble(transfrom[1]);
            wideArea[i].GetComponent<SkillWideAreaArrow>().yScale = (float)Convert.ToDouble(transfrom[2]);
            widePos = widePos + dir;
            wideArea[i].GetComponent<SkillWideAreaArrow>().widePos = widePos;
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

    private Vector3 SetMonAttackEffectPos(bool target, string position)//target true player false monster
    {
        if (!target)//monster
        {
            switch (position)
            {
                case "center":
                    return new Vector3(0, 0.8f, 0);
                case "bottom":
                    return new Vector3(0, -0.2f, 0);//미정
            }
        }
        else//player
        {
            switch (position)
            {
                case "center":
                    return new Vector3(0, 0.2f, 0);
                case "bottom":
                    return new Vector3(0, -0.6f, 0);
            }
        }
        return new Vector3(0, 0, 0);
    }
}
