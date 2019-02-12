using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BufferUI : MonoBehaviour
{
    private Image Buffer;

    float i;
    private void Awake()
    {
        i = 0;
        Buffer = transform.Find("Canvas").transform.Find("BackgroundBar").Find("Buffer").GetComponent<Image>();
    }

    private void Update()
    {
        Buffer.fillAmount = i / 100;
        i+= 0.1f;
    }
}
