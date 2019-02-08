using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 데미지 변경 -> player의 AttackCollider
 * defence, power 변경 ->MonsterInfo
 * 버퍼 오브젝트가 파괴되고 새로운 스킬이 발동 되기전까지 지속
 */

public class SkillBuffer : MonoBehaviour
{
    public int monsterNum;//몬스터 번호
    public int effectNum; //0이 버퍼 1이 디버퍼
    private GameObject player;
    private GameObject monsterBossOb;
    private Monster monsterBossCs;
    private RuntimeAnimatorController runtimeAnimatorController;
    public Vector3 setPos;//AttackEffectPosition, 중점에서 시작, 바닥에서 시작 정해주는 애, 몬스터 인포에 있음
                          //나중에 여기서 지정해 줘야함 버퍼, 디퍼버 는 센터일 것 같음
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        monsterBossCs = GetComponent<Monster>();
        if (effectNum == 0)//버퍼
        {
            monsterBossOb = transform.parent.transform.Find("Monster").gameObject;
            
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "Buffer" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
            //애니매이션 컨트롤러만 변경
            transform.position = monsterBossOb.transform.position + setPos;
            //보스몬스터 currenthp, damage, power(공격력), defence(방어력) 변경
            /*
             * 
             * 
             */

            //monsterBossCs.currentHp += 30; //체력 상승
            //monsterBossCs.power += 30; //공격력 상승

        }
        else//디버퍼
        {
            player = GameObject.Find("Player");
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "Buffer" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
            //애니매이션 컨트롤러만 변경
            transform.position = player.transform.position + setPos;//AttackEffectPosition 
            //플레이어 currenthp, damage 변경
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (effectNum == 0)
        {
            transform.position = monsterBossOb.transform.position + setPos; // buffer attack effect position == bossmonster
        }
        else if (effectNum == 1)
        {
            transform.position = player.transform.position + setPos;// debuffer Attack Effect Position == player
        }
        
    }

    private void EndEffect()
    {
        Destroy(gameObject);
    }

}