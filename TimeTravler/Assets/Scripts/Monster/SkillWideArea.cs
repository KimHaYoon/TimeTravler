using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWideArea : MonoBehaviour
{

    public int monsterNum;//몬스터 번호
    public int effectNum;
    private GameObject monsterBossOb;
    private Monster monsterBossCs;
    private SkillManager skillManager;
    private RuntimeAnimatorController runtimeAnimatorController;
    public Vector3 setPos;
    public Vector3 widePos;

    // Start is called before the first frame update
    void Start()
    {

        monsterBossCs = GetComponent<Monster>();
        skillManager = GetComponent<SkillManager>();
        
        if (effectNum == 0)
        {
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "WideArea" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
            //애니매이션 컨트롤러만 변경
            transform.position = widePos + setPos;
        }
        else
        {
            GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(monsterNum) + "/" + Convert.ToString(monsterNum) + "WideArea" + Convert.ToString(effectNum)) as RuntimeAnimatorController;
            //애니매이션 컨트롤러만 변경
            transform.position = widePos + setPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = widePos + setPos;
    }

    private void EndEffect()
    {
        Destroy(gameObject);
    }
}
