using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance = null;

    public static ItemManager GetInstance()
    {
        if (instance == null)
            instance = new ItemManager();

        return instance;
    }
    public TextAsset csvFile;
    protected string[] data;
    protected string[] stringList;
    protected string text;

    [SerializeField]
    private List<ItemData> itemList = new List<ItemData>();

    private StringReader reader;
    private string strLine;
    private int lineCount;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        // 문자 구분을 위한 변수 선언
        text = csvFile.text;
        stringList = text.Split('\n');

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
            ItemData item = new ItemData();

            item.code = getCode(j);
            item.name = getName(j);
            item.maxcount = getMaxcount(j);
            item.sellprice = getSellprice(j);
            item.buyprice = getBuyprice(j);
            item.option1 = getOpt1(j);
            item.option1_1= getOpt1_1(j);
            item.option2 = getOpt2(j);
            item.option2_1= getOpt2_1(j);
            
            itemList.Add(item);
        }
    } 

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(GetOption1(11001));
    }

    // csv에서 로드할 때 쓰는 함수들
    int getCode(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[0]);
    }

    string getName(int index)
    {
        data = stringList[index].Split(',');
        return data[1];
    }

    int getMaxcount(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[2]);
    }

    int getSellprice(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[3]);
    }

    int getBuyprice(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[4]);
    }

    string getOpt1(int index)
    {
        data = stringList[index].Split(',');
        return data[5];
    }

    int getOpt1_1(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[6]);
    }

    string getOpt2(int index)
    {
        data = stringList[index].Split(',');
        return data[7];
    }

    int getOpt2_1(int index)
    {
        data = stringList[index].Split(',');
        return int.Parse(data[8]);
    }


    // 코드번호 -> 아이템의 정보
    public ItemData GetItemInfo(int code)
    {
        for(int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                return itemList[i];
            }
        }
        
        return null;
    }


    // 코드번호 -> 아이템 이름
    public string GetItemName(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                return itemList[i].name;
            }
        }

        return null;
    }

    // 코드번호 -> 옵션1 (ex. 공격력 10), 찾는게 없으면 null 리턴
    public string GetOption1(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                return itemList[i].option1 + " " + itemList[i].option1_1.ToString();
            }
        }

        return null;
    }

    // 코드번호 -> 옵션1 (ex. 공격력 +10), 찾는게 없으면 null 리턴
    public string GetOption1Sign(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                if(itemList[i].option1_1 >= 0)
                    return itemList[i].option1 + " +" + itemList[i].option1_1.ToString();

                else
                    return itemList[i].option1 + " -" + itemList[i].option1_1.ToString();

            }
        }

        return null;
    }

    // 코드번호 -> 옵션2 (ex. 공격력 10), 찾는게 없으면 null 리턴
    public string GetOption2(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                return itemList[i].option2 + " " + itemList[i].option2_1.ToString();
            }
        }

        return null;
    }

    // 코드번호 -> 옵션1 (ex. 공격력 +10), 찾는게 없으면 null 리턴
    public string GetOption2Sign(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                if (itemList[i].option2_1 >= 0)
                    return itemList[i].option2 + " +" + itemList[i].option2_1.ToString();

                else
                    return itemList[i].option2 + " -" + itemList[i].option2_1.ToString();

            }
        }

        return null;
    }

    // 코드번호 -> maxcount, 찾는게 없으면 -1 리턴
    public int GetMaxcount(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                return itemList[i].maxcount;
            }
        }

        return -1;
    }

    // 아이템 이름 -> maxcount
    public int GetMaxcount(string name)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].name == name)
            {
                return itemList[i].maxcount;
            }
        }

        return -1;
    }

    // 코드번호 앞에서 3자리 숫자 -> 해당하는 아이템 목록 다 리턴 (ex. 121 -> 121xx인 아이템들)
    public List<ItemData> GetItems(int code)
    {
        List<ItemData> items = new List<ItemData>();

        int icode;

        for (int i = 0; i < itemList.Count; ++i)
        {
            icode = itemList[i].code / 100;

            if (icode == code )
            {
                //Debug.Log(itemList[i].code);
                items.Add(itemList[i]);
            }

        }

        return items;
    }

    // 코드번호 min ~ max 범위까지 (ex. min = 0, max = 3 이면 0, 1, 2, 3 값 리턴)
    public List<ItemData> GetItems(int min, int max)
    {
        List<ItemData> items = new List<ItemData>();

        for(int i = 0; i < itemList.Count; ++i)
        {
            if (min <= itemList[i].code && itemList[i].code <= max)
            {
                //Debug.Log(itemList[i].code);
                items.Add(itemList[i]);
            }
          
        }

        return items;
    }

    // 코드번호 앞에서 3자리 숫자, 랜덤으로 뽑을 아이템 개수
    public List<ItemData> GetItem(int code, int num)
    {
        List<ItemData> items = new List<ItemData>();
        List<ItemData> returnitems = new List<ItemData>();

        int icode;
        int[] rand = new int[num];
        bool isSame = false;

        for (int i = 0; i < itemList.Count; ++i)
        {
            icode = itemList[i].code / 100;

            if (icode == code)
            {
                items.Add(itemList[i]);
            }
        }

        for (int i = 0; i < num; ++i)
        {
            while (true)
            {
                rand[i] = Random.Range(0, items.Count);
                isSame = false;
                for (int j = 0; j < i; ++j)
                {
                    if (rand[i] == rand[j])
                    {
                        isSame = true;
                        break;
                    }
                }

                if (!isSame)
                    break;
            }
        }
        
        for(int  i = 0; i < num; ++i)
        {
            returnitems.Add(items[rand[i]]);
        }

        return returnitems;
    }

    // 코드번호(4자리수) -> 해당 아이템 코드들(5자리) 
    public List<int> GetItemCodes(int code)
    {
        List<int> itemcodes = new List<int>();

        for (int i = 0; i < itemList.Count; ++i)
        {
            int realcode = itemList[i].code / 10;

            if (realcode == code)
            {
                itemcodes.Add(itemList[i].code);
                //Debug.Log(itemList[i].code);
            }
        }

        return itemcodes;
    }

    // 코드번호(4자리) -> 해당 아이템의 개수
    public int GetItemCount(int code)
    {
        return GetItemCodes(code).Count;
    }

    // 코드번호(4자리), 몇 번째 -> 해당 아이템 정보
    public ItemData GetItemInfo(int code, int num)
    {
        ItemData item = new ItemData();

        List<int> codes = GetItemCodes(code);

        item = GetItemInfo(codes[num]);

        return item;
    }

    // 옵션 1_1
    public int GetOpt1_1(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                return itemList[i].option1_1;
            }
        }

        return -1;
    }

    // 옵션 1_1
    public int GetOpt2_1(int code)
    {
        for (int i = 0; i < itemList.Count; ++i)
        {
            if (itemList[i].code == code)
            {
                return itemList[i].option2_1;
            }
        }

        return -1;
    }
}
