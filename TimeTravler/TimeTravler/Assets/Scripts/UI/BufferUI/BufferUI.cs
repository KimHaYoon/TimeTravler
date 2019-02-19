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
    private Coroutine[] CorHeal;

    private void Awake()
    {
        ObjBuffer = new GameObject[14, 2];
        buf = new float[14, 2];
        PosList = new List<GameObject>();
        checkCor = new bool[3];
        CorHeal = new Coroutine[3];
    }
    private void Start()
    {
        StartCoroutine(UpdatePos());
    }
    private void Update()
    {
        if (monster != null)
        {
            transform.position = monster.transform.position + pos;
        }
    }

    private IEnumerator UpdatePos()
    {
        int k = 0;
        for (int i = 0; i < PosList.Count; i++)
        {
            if (PosList[i] == null)
                continue;
            if (monster == null)
                PosList[i].GetComponent<Buffer>().SetPlayerPos(k);
            else
                PosList[i].GetComponent<Buffer>().SetMonsterPos(k);
            k++;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(UpdatePos());
    }
    
    public void StartBuf(GameObject gameObject, bool who, int num, int type, float crease, float time)//버프류
    {
        //who = true player false monster;
        int k = 0;
        buf[num, type] = crease;
        if (who)//player
        {
            if(ObjBuffer[num, type] != null)
            {
                PosList.Remove(ObjBuffer[num, type]);
                Destroy(ObjBuffer[num, type].gameObject);
            }
            ObjBuffer[num, type] = Instantiate(Resources.Load("UI/Prefabs/Buffer")) as GameObject;
            PosList.Add(ObjBuffer[num, type]);
            ObjBuffer[num, type].transform.parent = transform.Find("Canvas");
            if (num < 3)
                ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num * 2 + type, time);
            else
                ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num + 3, time);
            
            for (int i = 0; i < PosList.Count; i++)
            {
                if (PosList[i] == null)
                    continue;
                if (monster == null)
                    PosList[i].GetComponent<Buffer>().SetPlayerPos(k);
                else
                    PosList[i].GetComponent<Buffer>().SetMonsterPos(k);
                k++;
            }
            switch (num)
            {
                case 0://공격력
                    player.power = player._power * (1 + buf[0, 0] + buf[0, 1] + buf[6, 0]);
                    break;
                case 1://방어력
                    player.defence = player._defence * (1 + buf[1, 0] + buf[1, 1] + buf[7, 0]);
                    break;
                case 2://치명타
                    player.dex = player._dex * (1 + buf[2, 0] + buf[2, 1] + buf[8, 0]);
                    break;
                case 3://회복지속물약1
                case 4://회복지속물약2
                case 5://회복지속물약3
                    if(!checkCor[num - 3])
                        CorHeal[num - 3] = StartCoroutine(ObjBuffer[num, type].GetComponent<Buffer>().Heal(crease, time));
                    else
                    {
                        StopCoroutine(CorHeal[num - 3]);
                        CorHeal[num - 3] = StartCoroutine(ObjBuffer[num, type].GetComponent<Buffer>().Heal(crease, time));
                    }
                    checkCor[num - 3] = true;
                    break;
                case 6://공격력버프물약
                    player.power = player._power * (1 + buf[0, 0] + buf[0, 1] + buf[6, 0]);
                    break;
                case 7://방어력버프물약
                    player.defence = player._defence * (1 + buf[1, 0] + buf[1, 1] + buf[7, 0]);
                    break;
                case 8://치명타버프물약
                    player.dex = player._dex * (1 + buf[2, 0] + buf[2, 1] + buf[8, 0]);
                    break;
                case 9://체력버프물약
                    float rate = player.currentHp / player.Hp;
                    player.Hp = (int)(player._Hp * crease);
                    player.currentHp = (int)(player.Hp * rate);
                    break;
                case 10://점프물약
                    player.extraJumpsValue = 2;
                    break;
            }
        }
        else//monster
        {
            if (ObjBuffer[num, type] != null)
            {
                Destroy(ObjBuffer[num, type].gameObject);
                PosList.Remove(ObjBuffer[num, type]);
            }
            ObjBuffer[num, type] = Instantiate(Resources.Load("UI/Prefabs/Buffer")) as GameObject;
            PosList.Add(ObjBuffer[num, type]);
            ObjBuffer[num, type].transform.parent = transform.Find("Canvas");
            if (num < 3)
                ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num * 2 + type, time);
            else
                ObjBuffer[num, type].GetComponent<Buffer>().Init(who, num + 3, time);
            for (int i = 0; i < PosList.Count; i++)
            {
                if (PosList[i] == null)
                    continue;
                if (monster == null)
                    PosList[i].GetComponent<Buffer>().SetPlayerPos(k);
                else
                    PosList[i].GetComponent<Buffer>().SetMonsterPos(k);
                k++;
            }
            switch (num)
            {
                case 0://공격력
                    monster.power = monster._power * (1 + buf[0, 0] + buf[0, 1] + buf[6, 0]);
                    break;
                case 1://방어력
                    monster.defence = monster._defence * (1 + buf[1, 0] + buf[1, 1] + buf[7, 0]);
                    break;
                case 2://치명타
                    monster.dex = monster._dex * (1 + buf[2, 0] + buf[2, 1] + buf[8, 0]);
                    break;
            }
            buf[num, type] += crease;
        }
    }

    public void EndBuf(bool target, int num)//true player false monster
    {
        int type = num % 2;
        int k = 0;
        if (num < 6)
            num /= 2;
        else
            num -= 3;

        buf[num, type] = 0;
        if (target)//플레이어
        {
            switch (num)//원상태로 복구
            {
                case 0://공격력
                    player.power = player._power * (1 + buf[0, 0] + buf[0, 1] + buf[6, 0]);
                    break;
                case 1://방어력
                    player.defence = player._defence * (1 + buf[1, 0] + buf[1, 1] + buf[7, 0]);
                    break;
                case 2://치명타
                    player.dex = player._dex * (1 + buf[2, 0] + buf[2, 1] + buf[8, 0]);
                    break;

                case 3://회복지속물약1
                case 4://회복지속물약2
                case 5://회복지속물약3
                    StopCoroutine(CorHeal[num - 3]);
                    checkCor[num - 3] = false;
                    break;
                case 6://공격력버프물약
                    player.power = player._power * (1 + buf[0, 0] + buf[0, 1] + buf[6, 0]);
                    break;
                case 7://방어력버프물약
                    player.defence = player._defence * (1 + buf[1, 0] + buf[1, 1] + buf[7, 0]);
                    break;
                case 8://치명타버프물약
                    player.dex = player._dex * (1 + buf[2, 0] + buf[2, 1] + buf[8, 0]);
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
                    monster.power = monster._power * (1 + buf[0, 0] + buf[0, 1] + buf[6, 0]);
                    break;
                case 1://방어력
                    monster.defence = monster._defence * (1 + buf[1, 0] + buf[1, 1] + buf[7, 0]);
                    break;
                case 2://치명타
                    monster.dex = monster._dex * (1 + buf[2, 0] + buf[2, 1] + buf[8, 0]);
                    break;
            }
        }
        PosList.Remove(ObjBuffer[num, type]);//끝난놈 지우기
    }
    

    public void SetPos(Vector3 pos)
    {
        this.pos = pos;
    }
}
