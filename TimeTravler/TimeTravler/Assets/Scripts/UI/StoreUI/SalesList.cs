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
    public int stage;

    //private int iItemTag;

    void Awake()
    {
        //storeTitleText = GameObject.Find("Canvas/storeui/title/titleText").GetComponent<Text>().text;
        stage = 1;
        GetEquipment();
        GetGrocery();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ItemTag(string strTag)
    {
        if ("장비" == strTag)
        {
            salesList.Clear();
            Debug.Log(salesList.Count);
            for (int i = 0; i < EquipmentSales.Count; ++i)
                salesList.Add(EquipmentSales[i]);
        }

        else if ("잡화" == strTag)
        {
            salesList.Clear();
            Debug.Log(salesList.Count);
            for (int i = 0; i < GrocerySales.Count; ++i)
                salesList.Add(GrocerySales[i]);
        }
    }

    private void GetEquipment()
    {
        // 모자
        int num = 3;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(110, num)[i]);
        }

        // 갑옷
        num = 3;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(120, num)[i]);
        }

        // 신발
        num = 3;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(130, num)[i]);
        }

        // 롱소드
        num = 2;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(140, num)[i]);
        }

        // 대검
        num = 2;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(141, num)[i]);
        }

        // 창
        num = 2;
        for (int i = 0; i < num; ++i)
        {
            EquipmentSales.Add(itemManager.GetItem(142, num)[i]);
        }

    }

    private void GetGrocery()
    {
        for (int i = 0; i < 11; ++i)
            GrocerySales.Add(itemManager.GetItems(21010, 23050)[i]);
    }

    public List<ItemData> GetSalesList()
    {
        if (salesList.Count == 0)
            return new List<ItemData>();

        else
            return salesList;
    }
}