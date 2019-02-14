using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gg : MonoBehaviour
{
    private SpriteRenderer BodySpriteRenderer;
    private SpriteRenderer HeadSpriteRenderer;
    private SpriteRenderer HelmetSpriteRenderer;
    private SpriteRenderer SwordSpriteRenderer;
    private SpriteRenderer ShieldSpriteRenderer;
    private SpriteRenderer LeftLegSpriteRenderer;
    private SpriteRenderer RightLegSpriteRenderer;

    private int weapon = 1;//1 칼 2 대검 3 창

    public int Hp = 20000;
    public int currentHp;

    public float power;
    public float defence;
    public float dex;
    public float _power;
    public float _defence;
    public float _dex;

    private float[,] buf;//공방치


    public void Consume(bool onoff, string item, int type, int opt1, int opt2)
    {
        if (onoff)//착용
        {
            switch (type)
            {
                case 1://모자
                    HelmetSpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    //stat
                    
                    //opt1 방어력
                    defence += opt1 * (buf[1, 0] + buf[1, 1]);
                    _defence += opt1;
                    return;
                case 2://갑옷
                    BodySpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    //stat

                    _defence += opt1;
                    //opt1 방어력
                    return;
                case 3://신발
                       //stat

                    _defence += opt1;
                    //opt1 방어력
                    return;
                case 4://무기
                    SwordSpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    weapon = Convert.ToInt32(item.Substring(2, 1));//무기변경함
                                                                   //퀵슬롯 바껴야됨


                    _power += opt1;
                    _dex += opt2;
                    //opt1 공격력
                    //opt2 치명타
                    //stat
                    return;
                case 5://방패
                    ShieldSpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/" + item, typeof(Sprite));
                    //stat

                    _defence += opt1;
                    //opt1 방어력
                    return;
                case 6://회복물약
                    currentHp += opt1;
                    if (currentHp > Hp)
                        currentHp = Hp;

                    //opt1 체력회복량
                    return;




                //opt1 체력회복량
                //opt2 회복시간

                case 7://버프
                    return;
                case 8://버프
                    return;
                case 9://버프
                    return;
                case 10://버프
                    return;
                case 11://버프
                    return;

            }
        }
        else//제거
        {
            switch (type)
            {
                case 1://모자
                    HelmetSpriteRenderer.sprite = (Sprite)Resources.Load("None", typeof(Sprite));
                    //stat
                    _defence -= opt1;
                    return;
                case 2://갑옷
                    BodySpriteRenderer.sprite = (Sprite)Resources.Load("Item/ItemUse/1201", typeof(Sprite));
                    //stat
                    _defence -= opt1;
                    return;
                case 3://신발
                    //stat
                    _defence -= opt1;
                    return;
                case 4://무기
                    SwordSpriteRenderer.sprite = (Sprite)Resources.Load("None", typeof(Sprite));
                    //stat
                    _power -= opt1;
                    _dex -= opt2;
                    return;
                case 5://방패
                    ShieldSpriteRenderer.sprite = (Sprite)Resources.Load("None", typeof(Sprite));
                    //stat
                    _defence -= opt1;
                    return;
            }
        }
        /*
         * onoff (true = 장착, false = 탈착)
         * item (4자리 코드)
         * type(1 = 모자, 2 = 갑옷, 3 = 신발, 4 = 무기, 5 = 방패, 6 = 회복물약(2101,2201) 7 >>이후 버프물약)
         * opt1, opt2 itemData에 적힌 순서대로 없으면 0으로
         */
    }
}
