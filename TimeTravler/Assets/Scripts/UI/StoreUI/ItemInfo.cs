using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Text Info;
    public Text Sell;
    public Text Buy;
    public Text Option1;
    public Text Option2;
    public ItemManager itemM = ItemManager.instance;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItem(ItemData item)
    {
        Info.name = item.name;
        Info.text = item.name;

        Sell.name = item.sellprice.ToString() + "s";
        Sell.text = item.sellprice.ToString() + "s";

        Buy.name = item.buyprice.ToString() + "s";
        Buy.text = item.buyprice.ToString() + "s";

        Option1.name = itemM.GetOption1Sign(item.code);
        Option1.text = itemM.GetOption1Sign(item.code);
        if (item.option2 == "NONE")
            Option2.gameObject.SetActive(false);
        else
            Option2.text = itemM.GetOption2Sign(item.code);
    }
}
