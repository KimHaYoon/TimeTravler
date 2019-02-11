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

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        slotList = new List<Slot>();
 
        int listCnt = salesList.GetSalesList().Count;

        for (int i = 0; i < listCnt; ++i)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            slot.SetItem(salesList.GetSalesList()[i]);
            slotList.Add(slot);
        }

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
