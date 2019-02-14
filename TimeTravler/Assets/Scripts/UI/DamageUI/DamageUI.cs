using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DamageUI : MonoBehaviour
{
    private bool Probability(int probability)//확률함수
    {
        if (probability >= UnityEngine.Random.Range(0, 100))
            return true;
        else
            return false;
    }

    public void SetDamage(GameObject target, GameObject owner, bool who, bool knockBack, bool cri, float damagePump)
    {
        //who false player true monster
        //knockBack false no true yes
        //cir false no true yes
        Player player;
        Monster monster;
        int damage;
        string color = "YELLOW";
        bool direction;
        if (who)
        {
            player = owner.GetComponent<Player>();
            monster = target.GetComponent<Monster>();
            damage = (int)(player.power - monster.defence);//공-방
            damage = (int)(damage * damagePump);
            if (Probability((int)player.dex) && cri)
            {
                damage *= 2;
                color = "RED";
            }
            if (damage <= 0)
            {
                damage = 1;
                color = "YELLOW";
            }
            monster.currentHp -= damage;
            if (player.transform.position.x - monster.transform.position.x > 0)
                direction = true;
            else
                direction = false;
            transform.position = monster.transform.position;
        }
        else
        {
            player = target.GetComponent<Player>();
            monster = owner.GetComponent<Monster>();
            damage = (int)(monster.power - player.defence);//공-방
            damage = (int)(damage * damagePump);
            if (Probability(monster.dex) && cri)
            {
                damage *= 2;
                color = "RED";
            }
            if (damage <= 0)
            {
                damage = 1;
                color = "YELLOW";
            }
            player.currentHp -= damage;
            if (monster.transform.position.x - player.transform.position.x > 0)
                direction = true;
            else
                direction = false;
            transform.position = player.transform.position;
        }
        if (knockBack)
        {
            if (direction)
            {
                target.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 5f), ForceMode2D.Impulse); 
            }
            else
            {
                target.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f, 5f), ForceMode2D.Impulse);
            }
        }
        transform.Find("Canvas").transform.Find("Damage").GetComponent<Damage>().damage = damage;
        transform.Find("Canvas").transform.Find("Damage").GetComponent<Damage>().colorName = color;
        transform.Find("Canvas").transform.Find("Damage").GetComponent<Damage>().MoveDamageUI(direction);
    }
}