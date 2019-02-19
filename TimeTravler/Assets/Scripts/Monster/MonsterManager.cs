using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterManager : MonoBehaviour
{
    
    [SerializeField]
    public int monsterNum;//몬스터 번호
    [SerializeField]
    public int monsterBoss;//0일반몹 1중보 2막보
    [SerializeField]
    public int monsterCount;//몬스터 생성 숫자
    [SerializeField]
    public string[] monsterPos;//몬스터 생성 위치[x y](float)(가운데 ' ')
    [SerializeField]
    public float responseTime;//부활시간 0이면 부활x
    [SerializeField]
    public bool dropItem;//아이템드랍 0 안함 1 함

    private GameObject[] mon;//몬스터 프리팹
    
    // Start is called before the first frame update
    void Start()
    {
        InitMonster();//몬스터 생성
    }
    
    private void InitMonster()//몬스터 생성
    {
        mon = new GameObject[monsterCount];//몬스터 생성 숫자만큼 배열 할당
        
        for (int i = 0; i < monsterCount; i++)//몬스터 생성
        {
            string[] pos = monsterPos[i].Split(' ');//몬스터 위치 split
            if (monsterBoss == 0) //일반 몬스터
            {
                mon[i] = Instantiate(Resources.Load("Monster/Prefabs/MonsterDummy")) as GameObject;//몬스터 더미(일반몬스터) 오브젝트생성
            }
            else if (monsterBoss == 1)// 중간 보스
            {
                mon[i] = Instantiate(Resources.Load("Monster/Prefabs/MonsterBoss")) as GameObject;//몬스터 보스(보스) 오브젝트생성
                mon[i].transform.Find("Monster").GetComponent<SkillManager>().SkillInfo(monsterBoss);//몬스터 번호
            }
            mon[i].transform.parent = transform;//MonsterManager의 자식으로 설정
            mon[i].transform.Find("Monster").GetComponent<Monster>().monsterNum = monsterNum;//몬스터 번호
            mon[i].transform.Find("Monster").GetComponent<Monster>().monsterBoss = monsterBoss;//몬스터 종류(서브, 중간, 최종)
            mon[i].transform.Find("Monster").GetComponent<Monster>().mNum = i;//몬스터 개인번호
            mon[i].transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), 0);//몬스터 해당위치로 이동
        }
    }

    public void DestroyMonster(int num)//몬스터 파괴(Monster.sc)에서 호출
    {
        StartCoroutine(ResponeMonster(num));//부활 코루틴
    }
    
    IEnumerator ResponeMonster(int num)//부활 코루틴
    {

        Destroy(mon[num]);//죽은 몬스터 오브젝트 제거

        if (dropItem)//잡몹들
        {
            //mon[num].name 몬스터이름 quest에 전달
        }
        else//보스가 소환한 몬스터들
        {
            monsterCount--;
            Debug.Log(monsterCount);
            if (monsterCount == 0) Destroy(gameObject);//해당 매니저 삭제
        }

        if (responseTime == 0) yield break;//부활시간 0이면 부활x

        
        mon[num] = null;

        yield return new WaitForSeconds(responseTime);//부활시간만큼 대기

        string[] pos = monsterPos[num].Split(' ');//몬스터 위치 split
        if (monsterBoss == 0)
        {
            mon[num] = Instantiate(Resources.Load("Monster/Prefabs/MonsterDummy")) as GameObject;//몬스터 더미(일반몬스터) 오브젝트생성
            mon[num].transform.parent = transform;//MonsterManager의 자식으로 설정
            mon[num].transform.Find("Monster").GetComponent<Monster>().monsterNum = monsterNum;//몬스터 번호
            mon[num].transform.Find("Monster").GetComponent<Monster>().mNum = num;//몬스터 개인번호
            mon[num].transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), 0);//몬스터 해당위치로 이동
        }
    }
}
