using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skillslot : MonoBehaviour
{
    public static skillslot instance = null;
    public static List<GameObject> slots = null;
    public static int slotcount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        slots = new List<GameObject>();
        slotcount = transform.childCount;
        for (int i = 0, max = transform.childCount; i < max;i++)
        {
            slots.Add(transform.GetChild(i).gameObject);
            slots[i].GetComponent<skill_ob>().skill = null;
        }
    }

    public static void Add(GameObject input)
    {
        Inventory inventory = Inventory.instance;
        GameObject input_skill = input.GetComponent<skill_ob>().skill;
        skill_ob temp = null;
        //slots를 다 Skill_window.instance.slot_now 로 바꿔야함
        for (int j = 0, max = Skill_window.instance.slot_now.Length; j < max; j++)
            if (slots[j].GetComponent<skill_ob>().skill != null)
                if (slots[j].GetComponent<skill_ob>().skill == input_skill)
                    return;

        for (int j = 0, max = slots.Count; j < max; j++)
        {
            temp = slots[j].GetComponent<skill_ob>();

            if (temp.skill == null)
            {
                temp.skill = input_skill;// == clone
                //slots[j].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/ItemStandard/" + temp.code.Substring(0, 4));
                //slots[j].GetComponentInChildren<Text>().text = int.Parse(temp.code.Substring(5, 2)) > 0 ? " " + int.Parse(temp.code.Substring(5, 2)) : " ";
                return;
            }
        }


        /*//빈자리가 없을 때
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

        */
    }

    public void update()
    {
        GameObject[] skill_slot = Skill_window.instance.slot_now;
        for (int i =0; i < skillslot.slotcount; i++)
        {
            slots[i].GetComponent<skill_ob>().skill = skill_slot[i].GetComponent<skill_ob>().skill;
            if (slots[i].GetComponent<skill_ob>().skill == null)
                slots[i].GetComponent<Image>().sprite = Inventory.instance.defaultImage;
            else
                slots[i].GetComponent<Image>().sprite = slots[i].GetComponent<skill_ob>().skill.GetComponent<Image>().sprite;
        }
    }
}



