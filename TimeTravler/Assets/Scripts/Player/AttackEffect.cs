using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    private Player player;


    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void CreateAttackEffect()
    {
        transform.Find("Body").transform.Find("Weapon").transform.Find("Effect").gameObject.SetActive(true);
        if (player.weapon == 3)
            transform.Find("Body").transform.Find("Weapon").transform.Find("Effect").gameObject.SetActive(false);
        transform.parent.transform.Find("PlayerAttackCollider").gameObject.SetActive(true);
    }

    private void DestroyAttackEffect()
    {
        transform.Find("Body").transform.Find("Weapon").transform.Find("Effect").gameObject.SetActive(false);
        transform.parent.transform.Find("PlayerAttackCollider").gameObject.SetActive(false);
    }

    private void CreateSkill()
    {
        player.StartSkill();
    }


    private void AttackEnd()
    {
        player.isAttack = false;
    }


}
