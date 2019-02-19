using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWideAreaArrow : MonoBehaviour
{
    SkillWideArea ScSkillwideArea;

    public GameObject player;
    public GameObject monster;
    private GameObject start;

    public int monsterNum;//몬스터 번호
    public Vector3 setPos;
    public Vector3 widePos;
    public float xScale;
    public float yScale;

    private void Start()
    {
        start = transform.Find("SkillWideAreaStart").gameObject;
        transform.position = widePos + setPos + new Vector3(0, 4, 0);
    }

    private void EndSkillWideAreaStart()
    {
        GameObject SkillWideArea = Instantiate(Resources.Load("Monster/Prefabs/SkillWideArea")) as GameObject;
        SkillWideArea.GetComponent< SkillWideArea>().player = player;
        SkillWideArea.GetComponent<SkillWideArea>().monster = monster;
        SkillWideArea.GetComponent<SkillWideArea>().xScale = xScale;
        SkillWideArea.GetComponent<SkillWideArea>().yScale = yScale;
        SkillWideArea.transform.position = start.transform.position - new Vector3(0, 0.2f, 0);
        SkillWideArea.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "WideArea.") as RuntimeAnimatorController;
        Destroy(gameObject);
    }
}
