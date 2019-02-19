using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWideArea : MonoBehaviour
{
    public GameObject player;
    public GameObject monster;
    //public int monsterBoss;

    float x, y, Objx, Objy;

    public float xScale;
    public float yScale;

    private void Start()
    {
        transform.position = transform.position + new Vector3(0, -0.2f, 0);
        Objx = transform.position.x;
        Objy = transform.position.y;
        transform.localScale = new Vector3(xScale, yScale, 1);
    }
    private void SetDamage()
    {
        x = player.GetComponent<Player>().transform.position.x;
        y = player.GetComponent<Player>().transform.position.y;
        if (x > Objx - 1f && x < Objx + 1f && y > Objy - 10f && y < Objy + 10f)
            player.GetComponent<Player>().Hurt(monster, true, 1.5f);//충돌 데미지
    }
    private void EndEffect()
    {
        Destroy(gameObject);
    }
}
