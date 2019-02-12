using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buffer : MonoBehaviour
{

    private Image ImBuffer;
    private Image ImBufferBackground;
    private RectTransform BufferBackground;

    public int buf;
    public float time;
    public bool target;
    public float amount = 0;


    private void Awake()
    {
        
        ImBuffer = GetComponent<Image>();
        ImBufferBackground = transform.Find("BufferBackground").GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(BufferCount());
    }
    



    public void Init(bool who, int buf, float time)
    {
        target = who;
        this.buf = buf;
        this.time = time;
    }

    public void SetPlayerPos(int num)
    {
        GetComponent<RectTransform>().localPosition = new Vector3(-30 - num * 35, -30, 0);
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }
    public void SetMonsterPos(int num)
    {
        BufferBackground = transform.Find("BufferBackground").GetComponent<RectTransform>();
        GetComponent<RectTransform>().localPosition = new Vector3(0.5f -num * 0.5f, 0f, 0);
        GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 0); ;
        GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        //BufferBackground.localScale = new Vector3(0.5f, 0.5f, 0); ;
        BufferBackground.sizeDelta = new Vector2(1, 1);
    }

    public IEnumerator BufferCount()
    {
        ImBuffer.sprite = (Sprite)Resources.Load("UI/Buffer/Buffer" + Convert.ToString(buf), typeof(Sprite));
        while (amount < time)
        {
            amount += 0.01f;
            ImBufferBackground.fillAmount = amount / time;
            yield return new WaitForSeconds(0.01f);
        }
        transform.parent.transform.parent.GetComponent<BufferUI>().EndBuf(target, buf);
        Destroy(gameObject);
    }
}
