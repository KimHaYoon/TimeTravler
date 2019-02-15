using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BufferUI : MonoBehaviour
{
    float amount = 0;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Monster monster;

    public float[,] buf;//공방치
    private GameObject[,] ObjBuffer;
    private List<GameObject> PosList;
    public Vector3 pos = Vector3.zero;

    private bool[] checkCor;

    private void Awake()
    {
        ObjBuffer = new GameObject[14, 2];
        buf = new float[14, 2];
        PosList = new List<GameObject>();
        checkCor = new bool[3];
    }
    
    private void Update()
    {
        if (monster != null)
        {
            transform.position = monster.transform.position + pos;
        }
    }

    
    public void StartBuf(GameObject gameObject, bool who, int num, int type, float crease, float time)//버프류
    {
        //who = true player false monster;
        int pm = 1;//+-
        switch (type)//0버프 1디버프
        {
            case 0:
                pm = 1;
                break;
            case 1:
                pm = -1;
                break;
        }
        if (who)//player
        {
            if (buf[num, type] != 0)
            {
                ObjBuffer[num, type].GetComponent<Buffer>().time = time;
                ObjBuffer[num, type].GetComponent<Buffer>().amount = 0;
            }
            else
            {
                ObjBuffer[num, type] = Instantiate(Resources.Load("UI/Prefabs/Buffer")) as GameObject;
                PosList.Add(ObjBuffer[num, type]);
                ObjBuffer[num, type].transform.parent = transform.Find("Canvas");
                if (num < 3)
                    ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num * 2 + type, time);
                else
                    ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num + 3, time);
                ObjBuffer[num, type].GetComponent<Buffer>().SetPlayerPos(PosList.Count - 1);
            }

            switch (num)
            {
                case 0://공격력
                    player.power += (int)(player._power * pm * crease);
                    break;
                case 1://방어력
                    player.defence += (int)(player._defence * pm * crease);
                    break;
                case 2://치명타
                    player.dex += (int)(player._dex * pm * crease);
                    break;


                //버프물약

                case 3://회복지속물약1
                case 4://회복지속물약2
                case 5://회복지속물약3
                    if(!checkCor[num - 3])
                    {
                        StartCoroutine(ObjBuffer[num, type].GetComponent<Buffer>().Heal(crease, time));
                        checkCor[num - 3] = true;
                    }
                    break;
                case 6://공격력버프물약
                    player.power += (int)(player._power * pm * crease);
                    break;
                case 7://방어력버프물약
                    player.defence += (int)(player._defence * pm * crease);
                    break;
                case 8://치명타버프물약
                    player.dex += (int)(player._dex * pm * crease);
                    break;
                case 9://체력버프물약
                    float rate = player.currentHp / player.Hp;
                    player.Hp = (int)(player._Hp * pm * crease);
                    player.currentHp = (int)(player.Hp * rate);
                    break;
                case 10://점프물약
                    player.extraJumpsValue = 2;
                    break;
            }
            
            //buf[num, type] += crease;
        }
        else//monster
        {
            if (buf[num, type] != 0)
            {
                ObjBuffer[num, type].GetComponent<Buffer>().time = time;
                ObjBuffer[num, type].GetComponent<Buffer>().amount = 0;
            }
            else
            {
                ObjBuffer[num, type] = Instantiate(Resources.Load("UI/Prefabs/Buffer")) as GameObject;
                PosList.Add(ObjBuffer[num, type]);
                ObjBuffer[num, type].transform.parent = transform.Find("Canvas");
                ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num * 2 + type, time);
                ObjBuffer[num, type].GetComponent<Buffer>().SetMonsterPos(PosList.Count - 1);
            }
            switch (num)
            {
                case 0://공격력
                    monster.power += (int)(monster._power * pm * crease);
                    break;
                case 1://방어력
                    monster.defence += (int)(monster._defence * pm * crease);
                    break;
                case 2://치명타
                    monster.dex += (int)(monster._dex * pm * crease);
                    break;
            }
            

            //buf[num, type] += crease;
        }
    }

    public void EndBuf(bool target, int num)//true player false monster
    {
        int type = num % 2;
        int pm = 1;
        if (num < 6)
            num /= 2;
        else
            num -= 3;
        switch (type)//0버프 1디버프
        {
            case 0:
                pm = 1;
                break;
            case 1:
                pm = -1;
                break;
        }
        if (target)//플레이어
        {
            switch (num)//원상태로 복구
            {
                case 0://공격력
                    player.power -= (int)(player._power * pm * buf[num, type]);
                    break;
                case 1://방어력
                    player.defence -= (int)(player._defence * pm * buf[num, type]);
                    break;
                case 2://치명타
                    player.dex -= (int)(player._dex * pm * buf[num, type]);
                    break;

                case 3://회복지속물약1
                case 4://회복지속물약2
                case 5://회복지속물약3
                        checkCor[num - 3] = false;
                    break;
                case 6://공격력버프물약
                    player.power -= (int)(player._power * pm * buf[num, type]);
                    break;
                case 7://방어력버프물약
                    player.defence -= (int)(player._defence * pm * buf[num, type]);
                    break;
                case 8://치명타버프물약
                    player.dex -= (int)(player._dex * pm * buf[num, type]);
                    break;
                case 9://체력버프물약
                    float rate = player.currentHp / player.Hp;
                    player.Hp = player._Hp;
                    player.currentHp = (int)(player.Hp * rate);
                    break;
                case 10://점프물약
                    player.extraJumpsValue = 1;
                    break;



            }
        }
        else//몬스터
        {
            switch (num)//원상태로 복구
            {
                case 0://공격력
                    monster.power -= (int)(monster._power * pm * buf[num, type]);
                    break;
                case 1://방어력
                    monster.defence -= (int)(monster._defence * pm * buf[num, type]);
                    break;
                case 2://치명타
                    monster.dex -= (int)(monster._dex * pm * buf[num, type]);
                    break;
            }
        }
        buf[num, type] = 0;
        PosList.Remove(ObjBuffer[num, type]);//끝난놈 지우기
        for(int i = 0; i< PosList.Count; i++)
        {
            if(monster == null)
                PosList[i].GetComponent<Buffer>().SetPlayerPos(i);
            else
                PosList[i].GetComponent<Buffer>().SetMonsterPos(i);
        }
    }


    public void SetPos(Vector3 pos)
    {
        this.pos = pos;
    }
}
