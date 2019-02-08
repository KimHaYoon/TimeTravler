using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MonsterItemManager : MonoBehaviour
{
    public static MonsterItemManager instance = null;

    public static MonsterItemManager GetInstance()
    {
        if (instance == null)
            instance = new MonsterItemManager();

        return instance;
    }
    public TextAsset csvFile;
    protected string[] data;
    protected string[] stringList;
    protected string text;

    [SerializeField]
    private List<MonsterItemData> itemList = new List<MonsterItemData>();

    private StringReader reader;
    private string strLine;
    private int lineCount;

    [SerializeField]
    private ItemManager itemM = ItemManager.instance;

    void Awake()
    {
        instance = this;

        // 문자 구분을 위한 변수 선언
        text = csvFile.text;
        stringList = text.Split('\n');
    }

    // Start is called before the first frame update
    void Start()
    {
        reader = new StringReader(csvFile.text);
        strLine = reader.ReadLine();

        int i = 0;
        while (strLine != null)
        {
            strLine = reader.ReadLine();
            i++;

        }

        // 최상단은 컬럼명이므로 카운트에서 제외
        lineCount = i - 1;

        for (int j = 1; j <= lineCount; j++)
        {
            MonsterItemData item = new MonsterItemData();

            item.number = getNumber(j);
            item.name = getName(j);
            int count = getCount(j);

            item.codes = getCodes(j, count);

            itemList.Add(item);
        }

        // GetItemCodesProbability 예시
        int size = GetItemCodesProbability("뱀").Length;

        for (int k = 0; k < size; ++k)
        {
            //Debug.Log(GetItemCodesProbability("뱀")[k]);
        }

        // GetItemCodes 예시
        //int size = GetItemCodes("뱀").Length;

        //for (int k = 0; k < size; ++k)
        //{
        //    Debug.Log(GetItemCodes("뱀")[k]);
        //}

        // GetProbability(name) 예시
        //int size = GetProbability("뱀").Length;

        //for (int k = 0; k < size; ++k)
        //{
        //    Debug.Log(GetProbability("뱀")[k]);
        //}

        // GetProbability(name, code) 예시
        //Debug.Log(GetProbability("뱀", 11001));

        // GetItemCount 예시
        //Debug.Log(GetItemCount(1, 1100));
    }
    

    int getNumber(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[0]);
    }

    string getName(int index)
    {
        data = stringList[index].Split(',');
        return data[1];
    }

    int getCount(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[2]);
    }

    int[] getCodes(int index, int count)
    {
        data = stringList[index].Split(',');

        int[] codes = new int[count];
        for(int i = 0; i < count; ++i)
        {
            codes[i] = int.Parse(data[i + 3]);

            //Debug.Log(codes[i]);
        }

        return codes;
    }

    // 몬스터이름을 넣으면 아이템 코드 + 확률 배열이 리턴
    int[] GetItemCodesProbability(string name)
    {
        for(int i = 0; i < itemList.Count; ++i)
        {
            if(itemList[i].name == name)
            {
                return itemList[i].codes;
            }
        }

        return new int[] { -1 } ;
    }

    

    // 몬스터이름 -> 아이템 코드 배열
    int[] GetItemCodes(int num)
    {
        for(int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].name == name)
            {
                int count = itemList[i].codes.Length;
                int[] codes = new int[count];

                for (int j = 0; j < count; ++j)
                {
                    int code;
                    code = itemList[i].codes[j];

                    code /= 100;

                    codes[j] = code;                    
                }
                Debug.Log(codes);
                return codes;
            }
        }

        return new int[] { -1 };
    }

    // 몬스터 이름 -> 아이템 드랍확률 배열
    int[] GetProbability(string name)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].name == name)
            {
                int count = itemList[i].codes.Length;
                int[] pros = new int[count];

                for (int j = 0; j < count; ++j)
                {
                    int pro;
                    pro = itemList[i].codes[j];

                    pro %= 100;

                    pros[j] = pro;
                }

                return pros;
            }
        }

        return new int[] { -1 };
    }

    // 몬스터 이름, 아이템 코드 -> 해당 아이템 드랍 확률
    int GetProbability(string name, int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].name == name)
            {
                int count = itemList[i].codes.Length;
                
                for (int j = 0; j < count; ++j)
                {
                    int itemcode = itemList[i].codes[j] / 100;

                    if(itemcode == code)
                    {
                        int pro = itemList[i].codes[j] % 100;

                        return pro;
                    }
                }
            }
        }

        return -1;
    }

    // 몬스터 번호 -> 아이템 코드 + 확률 배열
    public int[] GetItemCodesProbability(int num)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].number == num)
            {
                return itemList[i].codes;
            }
        }

        return new int[] { -1 };
    }


    // 몬스터 번호, 아이템 코드 -> 해당 아이템 개수
    public int GetItemCount(int num, int code)
    {
        for(int i = 0; i < itemList.Count; ++i)
        {
            if(itemList[i].number == num)
            {
                int count = itemM.GetItemCount(code);
                return count;
            }
        }
        return -1;
    }
}
