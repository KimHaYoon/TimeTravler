using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Itemslot : MonoBehaviour
{
    public static Itemslot instance = null;
    public static List<GameObject> slots = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        slots = new List<GameObject>();

        for (int i = 0, max = transform.childCount; i < max;i++)
        {
            slots.Add(transform.GetChild(i).gameObject);
            slots[i].GetComponent<Item_string>().code = null;
        }
    }

    public static void Add(GameObject input)
    {
        Inventory inventory = Inventory.instance;
        string input_string = string.Copy(input.GetComponent<Item_string>().code);
        Item_string temp = null;

        for (int j = 0, max = slots.Count; j < max; j++)
            if (slots[j].GetComponent<Item_string>().code != null)
                if (slots[j].GetComponent<Item_string>().code.Equals(input_string))
                    return;

        for (int j = 0, max = slots.Count; j < max; j++)
        {
            temp = slots[j].GetComponent<Item_string>();
            if(temp.code != null)
                if (temp.code.Equals(input_string))
                    return;

            if (temp.code == null)
            {
                temp.code = input_string;// == clone
                slots[j].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + temp.code.Substring(0, 4));
                slots[j].GetComponentInChildren<Text>().text = int.Parse(temp.code.Substring(5, 2)) > 0 ? " " + int.Parse(temp.code.Substring(5, 2)) : " ";
                return;
            }
        }


        //빈자리가 없을 때
        int i = 0;
        for (int max = slots.Count-1 ; i < max; i++)
        {
            temp = slots[i].GetComponent<Item_string>();
            inventory.clone(slots[i+1].GetComponent<Item_string>(), slots[i].GetComponent<Item_string>());
            slots[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + temp.code.Substring(0, 4));
            slots[i].GetComponentInChildren<Text>().text = int.Parse(temp.code.Substring(5, 2)) > 0 ? " " + int.Parse(temp.code.Substring(5, 2)) : " ";
        }
        temp = slots[i].GetComponent<Item_string>();
        slots[i].GetComponent<Item_string>().code = input_string;// == clone
        slots[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + temp.code.Substring(0, 4));
        slots[i].GetComponentInChildren<Text>().text = int.Parse(temp.code.Substring(5, 2)) > 0 ? " " + int.Parse(temp.code.Substring(5, 2)) : " ";


    }
}



