using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Damage : MonoBehaviour
{
    private Text text;
    public int damage;
    public string colorName = "YELLOW";
    private string color = "#ffff0000";
    private Vector3 pos;
    // Start is called before the first frame update\
    void Awake()
    {
        text = GetComponent<Text>();
        text.text = "<color=#ffff0000>0</color>";
    }
    void Start()
    {
        SetColor(colorName);
        StartCoroutine(DamageTimer());//알파값 코루틴
    }
    

    public void MoveDamageUI(bool direction)
    {
        if (direction)//left
            GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2f, -1f), 7);
        else//right
            GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(2f, 1f), 7);
    }

    private void SetColor(string colorName)
    {
        string strTmp = "";
        switch (colorName)
        {
            case "RED":
                strTmp = "#ff0000";
                break;
            case "YELLOW":
                strTmp = "#ffff00";
                break;
            case "GREEN":
                strTmp = "#00ff00";
                break;
            case "BLACK":
                strTmp = "#ffffff";
                break;
        }
        color = strTmp + text.text.Substring(14, 2);
        text.text = "<color=" + color + ">" + damage + "</color>";
    }

    IEnumerator DamageTimer()//알파값 코루틴
    {
        string tmp;
        for(int i = 0; i<20; i++)
        {
            tmp = Convert.ToString(Convert.ToByte(20+ i*10), 16);
            color = color.Substring(0, 7) + tmp;
            text.text = "<color=" + color + ">" + Convert.ToString(damage) + "</color>";
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.transform.parent.gameObject);
    }
}
