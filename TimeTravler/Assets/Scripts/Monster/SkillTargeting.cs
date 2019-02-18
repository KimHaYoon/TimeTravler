
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTargeting : MonoBehaviour
{
    public int monsterNum;//몬스터 번호
    public int monsterBoss;
    public int effectNum;
    private GameObject player;
    private GameObject monster;
    private Monster monsterBossCS;
    private RuntimeAnimatorController runtimeAnimatorController;
    public Vector3 setPos;
    public float xScale;
    public float yScale;


    // Start is called before the first frame update
    void Start()
    {
        monster = transform.parent.gameObject;
        monsterBossCS = GetComponent<Monster>();
        player = GameObject.Find("Player");
        transform.position = player.transform.position + setPos;
        GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "Targeting" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
        transform.localScale = new Vector3(xScale, yScale, 1);
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

    private void TargetDamage()
    {
        CreateDamageUI(player, monster, false, false, true, 3f);
    }

    private void CreateDamageUI(GameObject target, GameObject owner, bool who, bool knockBack, bool cri, float damagePump)//false player true monster
    {
        GameObject damageUI = Instantiate(Resources.Load("UI/Prefabs/DamageUI")) as GameObject;//데미지UI 오브젝트생성
        damageUI.GetComponent<DamageUI>().SetDamage(target, owner, who, knockBack, cri, damagePump);//false player true monster
    }
}
