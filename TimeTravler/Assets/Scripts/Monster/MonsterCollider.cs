using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{

    private Player player;
    private Monster monster;
    private Collider2D playerCollider;
    public bool destroy = false;

    
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerCollider = player.GetComponent<Collider2D>();
        monster = transform.parent.GetComponent<Monster>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!destroy && !monster.superArmor)
            {
                player.GetComponent<Player>().KnockBackHurt(monster.gameObject, false, 1f);//충돌 데미지
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!destroy && !monster.superArmor)
            {
                player.GetComponent<Player>().KnockBackHurt(monster.gameObject, false, 1f);//충돌 데미지
            }
        }
    }

    public void DestroyObject()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>(), true);//Player과 충돌무시
    }
}
