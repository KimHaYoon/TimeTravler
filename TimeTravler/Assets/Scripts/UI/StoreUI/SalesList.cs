using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalesList : MonoBehaviour
{
    [SerializeField]
    public ItemManager itemManager = ItemManager.instance;
    
    public List<ItemData> salesList = new List<ItemData>();
    public List<ItemData> EquipmentSales = new List<ItemData>();
    public List<ItemData> GrocerySales = new List<ItemData>();

    public Text storeTitleText;

    //private int iItemTag;

    void Awake()
    {
        //storeTitleText = GameObject.Find("Canvas/storeui/title/titleText").GetComponent<Text>().text;
        GetWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemTag(string strTag)
    {
        if("장비" == strTag)
        {
            salesList.Clear();
            for(int  i =0; i < EquipmentSales.Count; ++i)
                salesList.Add(EquipmentSales[i]);
        }

        else if("잡화" == strTag)
        {
            salesList.Clear();
            for (int i = 0; i < GrocerySales.Count; ++i)
                salesList.Add(GrocerySales[i]);
        }
    }

    private void GetWeapons()
    {
        //List<ItemData> item = new List<ItemData>();

        // 모자
        int num = 3;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(110, num)[i]);
        }

        // 롱소드
        num = 3;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(140, num)[i]);
        }

        // 대검
        num = 3;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(141, num)[i]);
        }

        // 창
        //num = 3;
        //for (int i = 0; i < num; ++i)
        //{
        //    EquipmentSales.Add(itemManager.GetItem(142, num)[i]);
        //}
    }

    public List<ItemData>   GetSalesList()
    {
        if (salesList.Count == 0)
            return new List<ItemData>();

        else
            return salesList;
    }

    public List<ItemData> GetEquipmentSales()
    {
        if (EquipmentSales.Count == 0)
            return new List<ItemData>();

        else
            return EquipmentSales;
    }
}
