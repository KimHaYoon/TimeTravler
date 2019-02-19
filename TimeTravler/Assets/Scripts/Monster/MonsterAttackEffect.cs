using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackEffect : MonoBehaviour
{
    
    private GameObject player;
    public int monsterNum;//몬스터 번호
    public Vector3 setPos;//AttackEffectPosition 
    public int damage;
    public bool boss;

    public float xScale;
    public float yScale;

    void Start()
    {
        player = GameObject.Find("Player");
        transform.localScale = new Vector3(xScale, yScale, 1);

        GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "AttackEffect") as RuntimeAnimatorController;//애니메니터 몬스터 번호에 맞춰서 설정
        if(boss)
            transform.position = player.transform.position + setPos + new Vector3(0, -0.5f, 0);//AttackEffectPosition 
        else
            transform.position = player.transform.position + setPos;//AttackEffectPosition 


    }
    
    void Update()
    {
        if (boss)
            transform.position = player.transform.position + setPos + new Vector3(0, -0.5f, 0);//AttackEffectPosition 
        else
            transform.position = player.transform.position + setPos;//AttackEffectPosition 
    }
    

    private void EndEffect()//이펙트 애니메이션 종료
    {
        Destroy(gameObject);//오브젝트 삭제
    }

}
