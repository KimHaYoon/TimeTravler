﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Skill_window : MonoBehaviour
{
    public static Skill_window instance = null;
    public static bool skill_window_show = false;
    public GameObject zero;
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
        int slot_size = 3;
        one_slot = new GameObject[slot_size];
        two_slot = new GameObject[slot_size];
        three_slot = new GameObject[slot_size];
        for (int i=0; i < slot_size; i++)
        {
            one_slot[i] = new GameObject(i + "");
            two_slot[i] = new GameObject(i + "");
            three_slot[i] = new GameObject(i + "");
            one_slot[i].AddComponent<skill_ob>().skill_img = null;
            two_slot[i].AddComponent<skill_ob>().skill_img = null;
            three_slot[i].AddComponent<skill_ob>().skill_img = null;
            one_slot[i].AddComponent<skill_ob>().skill = 0;
            two_slot[i].AddComponent<skill_ob>().skill = 0;
            three_slot[i].AddComponent<skill_ob>().skill = 0;
        }
        slot_now = one_slot;


    }


    public void button0()
    {
        instance.zero.SetActive(true);
        instance.one.SetActive(false);
        instance.two.SetActive(false);
        instance.three.SetActive(false);
    }
    public void button1()
    {
        instance.zero.SetActive(false);
        instance.one.SetActive(true);
        instance.two.SetActive(false);
        instance.three.SetActive(false);
        instance.slot_now = instance.one_slot;
        skillslot.instance.update();
    }
    public void button2()
    {
        instance.zero.SetActive(false);
        instance.one.SetActive(false);
        instance.two.SetActive(true);
        instance.three.SetActive(false);
        instance.slot_now = instance.two_slot;
        skillslot.instance.update();
    }
    public void button3()
    {
        instance.zero.SetActive(false);
        instance.one.SetActive(false);
        instance.two.SetActive(false);
        instance.three.SetActive(true);
        instance.slot_now = instance.three_slot;
        skillslot.instance.update();
    }
}