using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWideAreaStart : MonoBehaviour
{
    SkillWideArea ScSkillwideArea;

    public Player player;
    public GameObject monster;


    public int monsterNum;//몬스터 번호
    public int effectNum;
    public Vector3 setPos;
    public Vector3 widePos;

    
    private void Start()
    {
        transform.position = widePos + setPos + new Vector3(0, 2, 0);
    }

    private void EndSkillWideAreaStart()
    {
        GameObject SkillWideArea = Instantiate(Resources.Load("Monster/Prefabs/SkillWideArea")) as GameObject;
        SkillWideArea.GetComponent< SkillWideArea>().player = player;
        SkillWideArea.GetComponent<SkillWideArea>().monster = monster;
        SkillWideArea.transform.position = transform.position;
        SkillWideArea.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "WideArea" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
        Destroy(gameObject);
    }
}
