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
    private Player player;

    public int buf;
    public float time;
    public bool target;
    public float amount = 0;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
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
        BufferBackground.sizeDelta = new Vector2(1, 1);
    }

    public IEnumerator BufferCount()
    {
        ImBuffer.sprite = (Sprite)Resources.Load("UI/Buffer/Buffer" + Convert.ToString(buf), typeof(Sprite));
        while (amount <= time)
        {
            amount += Time.deltaTime;
            ImBufferBackground.fillAmount = amount / time;
            yield return null;
        }
        transform.parent.transform.parent.GetComponent<BufferUI>().EndBuf(target, buf);
        Destroy(gameObject);
    }

    public IEnumerator Heal(float heal, float time)
    {
        while(amount <= time)
        {
            player.currentHp += (int)heal;
            if (player.currentHp > player.Hp)
                player.currentHp = player.Hp;
            yield return new WaitForSeconds(1f);
        }
    }
}
