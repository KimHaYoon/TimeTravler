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

    private float[,] buf;//공방치
    private GameObject[,] ObjBuffer;
    private void Awake()
    {
        ObjBuffer = new GameObject[3, 2];
        buf = new float[3, 2];
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
        if (who)
        {
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
            }
            if (buf[num, type] != 0)
            {
                ObjBuffer[num, type].GetComponent<Buffer>().time += time;
            }
            else
            {
                ObjBuffer[num, type] = Instantiate(Resources.Load("UI/Prefabs/Buffer")) as GameObject;
                ObjBuffer[num, type].transform.parent = transform.Find("Canvas");
                ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num * 2 + type, time);
            }
            buf[num, type] += crease;
        }
        else
        {
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
            buf[num, type] += crease;
            if (buf[num, type] != 0)
                ObjBuffer[num, type] = Instantiate(Resources.Load("UI/Prefabs/Buffer")) as GameObject;
            ObjBuffer[num, type].transform.parent = transform.Find("Canvas");
            ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num * 2 + type, time);
        }
    }

    public void EndBuf(bool target, int num)//true player false monster
    {
        int type = num % 2;
        int pm = 1;
        num /= 2;
        switch (type)//0버프 1디버프
        {
            case 0:
                pm = 1;
                break;
            case 1:
                pm = -1;
                break;
        }
        if (target)
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
            }
        }
        else
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
            }
        }
        buf[num, type] = 0;
    }
}
