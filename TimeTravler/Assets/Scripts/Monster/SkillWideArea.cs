using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWideArea : MonoBehaviour
{
    public Player player;
    public GameObject monster;

    float x, y, Objx, Objy;


    private void Start()
    {
        x = player.transform.position.x;
        y = player.transform.position.y;
        Objx = transform.position.x;
        Objy = transform.position.y;
        StartCoroutine(DamageCor());
    }

    private IEnumerator DamageCor()
    {
        if (x > Objx - 1f && x < Objx + 1f && y > Objy - 10f && y < Objy + 10f)
            player.GetComponent<Player>().Hurt(monster, true, 1.5f);//충돌 데미지
        yield return new WaitForSeconds(0.2f);
        
        StartCoroutine(DamageCor());
    }
    private void EndEffect()
    {
        Destroy(gameObject);
    }
}
