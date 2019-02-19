﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill_window : MonoBehaviour
{
    public static Skill_window instance = null;
    public GameObject one;
    public GameObject[] one_slot;
    public GameObject two;
    public GameObject[] two_slot;
    public GameObject three;
    public GameObject[] three_slot;
    public GameObject[] slot_now;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        one_slot = new GameObject[skillslot.slotcount];
        two_slot = new GameObject[skillslot.slotcount];
        three_slot = new GameObject[skillslot.slotcount];
        for (int i=0; i < skillslot.slotcount; i++)
        {
            one_slot[i] = new GameObject(i + "");
            two_slot[i] = new GameObject(i + "");
            three_slot[i] = new GameObject(i + "");
            one_slot[i].AddComponent<skill_ob>().skill = null;
            two_slot[i].AddComponent<skill_ob>().skill = null;
            three_slot[i].AddComponent<skill_ob>().skill = null;
        }
        slot_now = one_slot;


    }

        public void button1()
    {
        instance.one.SetActive(true);
        instance.two.SetActive(false);
        instance.three.SetActive(false);
        instance.slot_now = instance.one_slot;
        skillslot.instance.update();
    }
    public void button2()
    {
        instance.one.SetActive(false);
        instance.two.SetActive(true);
        instance.three.SetActive(false);
        instance.slot_now = instance.two_slot;
        skillslot.instance.update();
    }
    public void button3()
    {
        instance.one.SetActive(false);
        instance.two.SetActive(false);
        instance.three.SetActive(true);
        instance.slot_now = instance.three_slot;
        skillslot.instance.update();
    }
}