
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargeting : MonoBehaviour
{
    public int monsterNum;//몬스터 번호
    public int effectNum;
    private GameObject player;
    private Monster monsterBossCS;
    private RuntimeAnimatorController runtimeAnimatorController;
    public Vector3 setPos;


    // Start is called before the first frame update
    void Start()
    {
        monsterBossCS = GetComponent<Monster>();
        player = GameObject.Find("Player");
        transform.position = player.transform.position + setPos;
        if (effectNum == 0)//스킬1
        {
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "Targeting" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
            //애니매이션 컨트롤러만 변경
        }
        else//스킬2
        {
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllesrs/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "Targeting" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
            //애니매이션 컨트롤러만 변경
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + setPos;
    }

    private void EndEffect()
    {
        Destroy(gameObject);
    }
}
