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
            slots[i].GetComponent<skill_ob>().skill = 0;
            slots[i].GetComponent<skill_ob>().skill_img = null;
        }
    }


    public void update()
    {
        GameObject[] skill_slot = Skill_window.instance.slot_now;
        for (int i =0; i < skillslot.slotcount; i++)
        {
            skillslot.slots[i].GetComponent<skill_ob>().skill = skill_slot[i].GetComponent<skill_ob>().skill;
            skillslot.slots[i].GetComponent<skill_ob>().skill_img = skill_slot[i].GetComponent<skill_ob>().skill_img;

            if (skillslot.slots[i].GetComponent<skill_ob>().skill == 0)
            skillslot.slots[i].GetComponent<Image>().sprite = Inventory.instance.defaultImage;
            else
            skillslot.slots[i].GetComponent<Image>().sprite = skillslot.slots[i].GetComponent<skill_ob>().skill_img;
        }
    }
}




