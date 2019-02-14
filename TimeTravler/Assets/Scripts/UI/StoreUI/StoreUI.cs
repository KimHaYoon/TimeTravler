using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public Transform slotRoot;
    public List<Slot> slotList;
    public SalesList salesList;
    public ItemInfo info;
    private bool salesListUpdate;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        slotList = new List<Slot>();
        salesListUpdate = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }

        if(salesListUpdate)
        {
            slotList.Clear();

            //int listCnt = salesList.GetSalesList().Count;
            int listCnt = slotRoot.childCount;

            for (int i = 0; i < listCnt; ++i)
            {
                var slot = slotRoot.GetChild(i).GetComponent<Slot>();

                if (salesList.GetSalesList().Count > i)
                {
                    ItemData data = salesList.GetSalesList()[i];
                    slot.SetItem(data);
                }

                else
                {
                    slot.SetItem(new ItemData());
                }

                slotList.Add(slot);
            }

            salesListUpdate = false;
        }
    }
    
    public void SalesListUpdate()
    {
        salesListUpdate = true;
    }
}
