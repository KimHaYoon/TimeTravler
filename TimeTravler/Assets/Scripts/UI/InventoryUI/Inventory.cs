using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public ItemInfo info;
    public static Inventory instance = null;
    GameObject inventory_window = null;
    GameObject status_window = null;
    const int inventory_max = 30;
    public int current_count;

    public Sprite defaultImage = null;
    public Sprite closeImage;
    public static bool window_show;
    public bool isSHOP;
    public List<GameObject> slots;
    public GameObject equipment_Head = null;
    public GameObject equipment_weapon = null;
    public GameObject equipment_Shield = null;
    public GameObject equipment_Armor = null;
    public GameObject equipment_Shoes = null;


    //클래스를 하나만 두고 초기화하는 함수
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
        //UI 싱글톤 찾아보기

        window_show = false;
        isSHOP = false;
        inventory_window = GameObject.Find("item_bg");
        status_window = GameObject.Find("status_bg");
        slots = new List<GameObject>();
        equipment_Head = GameObject.Find("equipment_Head");
        equipment_weapon = GameObject.Find("equipment_weapon");
        equipment_Shield = GameObject.Find("equipment_Shield");
        equipment_Armor = GameObject.Find("equipment_Armor");
        equipment_Shoes = GameObject.Find("equipment_Shoes");
        equipment_Head.GetComponent<Item_string>().code = null;
        equipment_weapon.GetComponent<Item_string>().code = null;
        equipment_Shield.GetComponent<Item_string>().code = null;
        equipment_Armor.GetComponent<Item_string>().code = null;
        equipment_Shoes.GetComponent<Item_string>().code = null;

        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("Inventory_slot"))
        {
            slots.Add(slot);
            slot.GetComponent<Item_string>().code = null;
        }
        defaultImage = slots[0].GetComponentInChildren<Image>().sprite;
        current_count = 0;
        inventory_window.SetActive(false);
        status_window.SetActive(false);
        

    }

    // i 버튼을 이용해 아이템창을 여는 함수
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (window_show == false)
            {
                inventory_window.SetActive(true);
                status_window.SetActive(true);
                window_show = true;
            }
            else
            {
                inventory_window.SetActive(false);
                status_window.SetActive(false);
                window_show = false;
                info.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            pop_list(1);
        }

        }

    //새로운 아이템을 슬롯에 넣기전에 하나씩 밀어서 공간을 확보하는 함수
    void push_list(int start)
    {
        Item_string to;
        for (int pos = current_count; pos >= start; pos--)
        {
            if (pos + 1 < inventory_max)
            {
                to = slots[pos + 1].GetComponent<Item_string>();
                clone(slots[pos].GetComponent<Item_string>(), to);
                if (to.code != null)
                {
                    slots[pos + 1].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + to.code.Substring(0, 4));
                    slots[pos + 1].GetComponentInChildren<Text>().text = int.Parse(to.code.Substring(5, 2)) > 0 ? " " + int.Parse(to.code.Substring(5, 2)) : " ";
                }
            }
        }

        current_count++;
    }

    //아이템을 슬롯에서 빼고나서 하나씩 끌어서 공간을 확보하는 함수
    public void pop_list(int start)
    {
        Debug.Log("pop 실행");
        Item_string to;
        int pos;
        start--;

        for (pos = start; pos <= current_count-2; pos++)
        {
            to = slots[pos].GetComponent<Item_string>();
            clone(slots[pos + 1].GetComponent<Item_string>(), to);
            slots[pos].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + to.code.Substring(0, 4));
            slots[pos].GetComponentInChildren<Text>().text = (int.Parse(to.code.Substring(5, 2)) > 0 ? " " + int.Parse(to.code.Substring(5, 2)) : " ");
        }

        Item_string LastSlot = slots[pos].GetComponent<Item_string>();
        LastSlot.code = null;
        LastSlot.GetComponentInChildren<Image>().sprite = defaultImage;
        LastSlot.GetComponentInChildren<Text>().text = " ";

        current_count--;
    }

    //새로운 아이템을 추가하기 위한 함수
    public void Add(string item)
    {
        if (current_count >= inventory_max)
            return;
        if (item == null)
            return;
        string Code = item; // 전체코드
        string ID = item.Substring(0, 5); // 5자리 코드
        bool Part = int.Parse(Code.Substring(0, 1)) > 1; //part가 장비가 아닌지 확인
        bool Push = false;

        for (int i =0; i< current_count; i++)
        {
        
            if (Part && ID.Equals(slots[i].GetComponent<Item_string>().code.Substring(0, 5)) && ItemManager.instance.GetMaxcount(int.Parse(ID)) >= int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) + int.Parse(Code.Substring(5, 2)))
            { // 기존 코드에 수량추가
                Debug.Log("아이템추가");
                int count = int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) + int.Parse(Code.Substring(5, 2));
                string count_string = ((count < 10 ? "0" + count : "" + count));
                slots[i].GetComponent<Item_string>().code = copy(slots[i].GetComponent<Item_string>().code.Substring(0, 5) + count_string);
                //해당 슬롯의 갯수출력변경
                slots[i].GetComponentInChildren<Text>().text = (int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) > 0 ? 
                    " " + int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) : " "); 
                //아이템 추가가 완료되었다고 알림
                Push = true;
                break;
            }
            else if (int.Parse(slots[i].GetComponent<Item_string>().code.Substring(0, 5)) > int.Parse(ID))
            {
                //아이템을 삽입할 자리마련
                push_list(i);
                //새로운 아이템 삽입
                slots[i].GetComponent<Item_string>().code = copy(Code);
                slots[i].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + ID.Substring(0, 4));
                slots[i].GetComponentInChildren<Text>().text = (int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) > 0 ? 
                    " " + int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) : " ");
                Push = true;
                Debug.Log("아이템삽입");
                break;
            }
        }
        if(Push != true)
        {// for문이 다 돌아도 아이템을 넣지 못했을 때
            slots[current_count].GetComponent<Item_string>().code = copy(Code);
            slots[current_count].GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + ID.Substring(0, 4));
            slots[current_count].GetComponentInChildren<Text>().text = (int.Parse(slots[current_count].GetComponent<Item_string>().code.Substring(5, 2)) > 0 ?
                " " + int.Parse(slots[current_count].GetComponent<Item_string>().code.Substring(5, 2)) : " ");
            current_count++;
            Push = true;
            Debug.Log("아이템마지막추가");
        }
        
               
    }

    public void minus_item(string code)
    {
        for (int i = 0; i < current_count; i++)
            if (slots[i].GetComponent<Item_string>().code.Equals(code))
            {
                int count = int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) - 1;
                if (count <= 0) {
                    pop_list(i+1);
                    return;
                }
                string count_string = ((count < 10 ? "0" + count : "" + count));
                slots[i].GetComponent<Item_string>().code = copy(slots[i].GetComponent<Item_string>().code.Substring(0, 5) + count_string);
                //해당 슬롯의 갯수출력변경
                slots[i].GetComponentInChildren<Text>().text = (int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) > 0 ?
                    " " + int.Parse(slots[i].GetComponent<Item_string>().code.Substring(5, 2)) : " ");
            }


    }

    public void clone(Item_string from, Item_string to)
    {
        to.code = copy(from.code);
}

    public string copy(string Word)
    {
        if (Word != null)
            return string.Copy(Word);
        else
            return null;
    }










}