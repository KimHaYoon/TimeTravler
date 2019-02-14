using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MonsterInfo : MonoBehaviour
{

    private GameObject player;
    public MonsterItemManager monsterItemManager;

    void Start()
    {
        monsterItemManager = MonsterItemManager.instance;
    }

    string SetDropItem()
    {
        return "";
    }
    public void SetMonster(GameObject monster, GameObject monsterUI)//몬스터 정보 설정
    {
        int[] dropItemCode;
        string dropItem ="";
        Vector3 uiPos = Vector3.zero;
        int num = monster.GetComponent<Monster>().monsterNum;//몬스터 번호
        Monster mon = monster.GetComponent<Monster>();
        player = GameObject.Find("Player");
        monster.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Monster/AnimationControllers/" + Convert.ToString(num) + "/" + Convert.ToString(num) + "Body") as RuntimeAnimatorController;//애니메니터 몬스터 번호에 맞춰서 설정
        monsterUI.transform.Find("Canvas").transform.Find("BackgroundBar").transform.Find("Name").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Monster/MonsterNames/" + Convert.ToString(num) + "Name", typeof(Sprite));//몬스터 이름 번호에 맞춰서 설정

        /*
        moveType;//true 이동 false 고정
        firstAttack;//true 선공 false 비선공
        SetMonCollider(monster, -0.00653702f, 0.42f, 1.266574f, 0.81f);//MonsterCollider
        SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);//MonsterSightCollider
        SetMonAttackSight(monster, 0f, 1.16f, 5f, 2.62f);//MonsterAttackSightCollider
        mon.attackEffectPos = new Vector3(1, 1, 1);//AttackEffectPos
        attackTime;//공격시간 공격애니메이션 없으면 0f
        hp;//최대체력
        power;//공격력
        defence;//방어력
        moveSpeed;//이동속도
        */
        uiPos = new Vector3(0, -0.7f, 0);
        switch (num)
        {
            case 1://DarkSnake
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, -0.00653702f, 0.42f, 1.266574f, 0.81f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 5f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 20;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 2://DeserSnake
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, -0.00653702f, 0.42f, 1.266574f, 0.81f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 5f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 3://Dwarf
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, 0f, 0.42f, 0.7f, 0.81f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 2f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 4://Giant
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, 0f, 0.592597f, 0.7f, 1.155194f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 9f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                uiPos = new Vector3(0, -0.5f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 5://Lucida
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0f, 0.29f, 0.58f, 0.57f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 5f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 6://MuscleStone
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, 0f, 0.49f, 1.05f, 0.96f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 7f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 7://Berserkie
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, 0f, 0.49f, 1.05f, 0.96f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 3f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 8://FlyStone
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, 0f, 0.52f, 1.05f, 0.66f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 7f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                uiPos = new Vector3(0, -0.5f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 9://Zombie
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0f, 0.4477812f, 0.6f, 0.8831121f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 2f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 10://Skeleton
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0.04363362f, 0.4430751f, 0.7479211f, 0.8661501f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 2f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 11://Ghost
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0f, 0.4865929f, 0.6f, 0.839336f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 7f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 12://GhostWizard
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0f, 0.7072063f, 0.72f, 1.130712f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 10f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                uiPos = new Vector3(0, 0.2f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 13://Yeti
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, 0f, 0.5211064f, 0.91f, 1.032213f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 4f, 2.62f);
                SetMonAttackEffect(monster, "bottom", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 14://SnowMan  **Attack, AttackEffect
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0.02328157f, 0.3738607f, 0.5434368f, 0.7377214f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0.1f, 1.16f, 0.1f, 2.62f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 0f;
                mon.hp = 50;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 15://Pepe  **Attack, AttackEffect
                mon.transform.localScale = new Vector3(4f, 4f, 1);
                SetMonCollider(monster, 0f, 0.1541149f, 0.3f, 0.29822f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 0.1f, 2.62f);
                uiPos = new Vector3(0, -0.8f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 0f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 16://Lycanthrope
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, -0.15f, 0.8108106f, 0.87f, 1.611621f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 5f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                uiPos = new Vector3(0, 0.2f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 17://Rabbit  **AttackEffect
                mon.transform.localScale = new Vector3(4f, 4f, 1);
                SetMonCollider(monster, 0.03f, 0.1882393f, 0.29f, 0.3664786f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 4f, 2.62f);
                uiPos = new Vector3(0, -0.6f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 18://Bear
                mon.transform.localScale = new Vector3(4f, 4f, 1);
                SetMonCollider(monster, 0f, 0.1882393f, 0.29f, 0.3664786f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 2f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 19://Jester
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, -0.009816766f, 0.518099f, 0.72f, 1.04738f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 3f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                uiPos = new Vector3(0, -0.3f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 20://TwistedJester
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, -0.009816766f, 0.3775415f, 0.6826694f, 0.745083f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 10f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.currentHp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 21://Mushroom  **Attack, AttackEffect
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0f, 0.3094977f, 0.6826694f, 0.6089953f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 0.1f, 2.62f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 0f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 22://Wyvern
                mon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
                SetMonCollider(monster, -0.18f, 0.4892032f, 1.08f, 0.9684062f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 7f, 2.62f);
                SetMonAttackEffect(monster, "bottom", 2f, 1.5f);
                uiPos = new Vector3(0, 0f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 23://Drake
                mon.transform.localScale = new Vector3(2f, 2f, 1);
                SetMonCollider(monster, 0f, 0.4124529f, 1.08f, 0.8149057f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 5f, 2.62f);
                SetMonAttackEffect(monster, "center", 2f, 2f);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
            case 24://Dragon
                mon.transform.localScale = new Vector3(1f, 1f, 1);
                SetMonCollider(monster, -0.1000729f, 1.058178f, 1.533021f, 2.106355f);
                SetMonSight(monster, 0f, 1.16f, 12.47f, 2.62f);
                SetMonAttackSight(monster, 0f, 1.16f, 5f, 2.62f);
                SetMonAttackEffect(monster, "center", 1f, 1f);
                uiPos = new Vector3(0, 0.3f, 0);
                mon.moveType = true;
                mon.firstAttack = true;
                mon.attackTimeValue = 3f;
                mon.hp = 100;
                mon.power = 10;
                mon.defence = 10;
                mon.moveSpeed = 2;
                break;
        }
        monsterUI.GetComponent<MonsterUI>().SetPos(uiPos);
        monster.transform.parent.Find("BufferUI").GetComponent<BufferUI>().SetPos(uiPos);
        mon.dex = 10;
        dropItemCode = monsterItemManager.GetItemCodesProbability(num);
        for (int i = 0; i < dropItemCode.Length; i++)
        {
            dropItem += Convert.ToString(dropItemCode[i]).Substring(0, 4);
            dropItem += Convert.ToString(UnityEngine.Random.Range(0, monsterItemManager.GetItemCount(num, dropItemCode[i] / 100) + 1));//7
            dropItem += Convert.ToString(dropItemCode[i]).Substring(4, 2);
            if(i != dropItemCode.Length - 1)
                dropItem += ",";
        }
        mon.dropItem = dropItem;
    }

    void SetMonCollider(GameObject mon, float offsetX, float offsetY, float sizeX, float sizeY)
    {
        CapsuleCollider2D monCollider = mon.GetComponent<CapsuleCollider2D>();
        BoxCollider2D monInCollider = mon.transform.Find("MonsterCollider").GetComponent<BoxCollider2D>();
        monCollider.offset = new Vector2(offsetX, offsetY);
        monCollider.size = new Vector2(0.25f, sizeY);
        monInCollider.offset = new Vector2(offsetX, offsetY);
        monInCollider.size = new Vector2(sizeX + 0.1f, sizeY + 0.1f);
    }

    void SetMonSight(GameObject mon, float offsetX, float offsetY, float sizeX, float sizeY)
    {
        BoxCollider2D monSight = mon.transform.parent.Find("MonsterSight").GetComponent<BoxCollider2D>();
        monSight.offset = new Vector2(offsetX, offsetY);
        monSight.size = new Vector2(sizeX, sizeY);
    }
    void SetMonAttackSight(GameObject mon, float offsetX, float offsetY, float sizeX, float sizeY)
    {
        BoxCollider2D monAttackSight = mon.transform.parent.Find("MonsterAttackSight").GetComponent<BoxCollider2D>();
        monAttackSight.offset = new Vector2(offsetX, offsetY);
        monAttackSight.size = new Vector2(sizeX, sizeY);
    }

    void SetMonAttackEffect(GameObject mon, string position, float xScale, float yScale)
    {
        switch (position)
        {
            case "center":
                mon.GetComponent<Monster>().attackEffectPos = new Vector3(0, 0.2f, 0);
                break;
            case "bottom":
                mon.GetComponent<Monster>().attackEffectPos = new Vector3(0, -0.4f, 0);
                break;
        }
        mon.GetComponent<Monster>().attackEffectXScale = xScale;
        mon.GetComponent<Monster>().attackEffectYScale = yScale;
    }
}
