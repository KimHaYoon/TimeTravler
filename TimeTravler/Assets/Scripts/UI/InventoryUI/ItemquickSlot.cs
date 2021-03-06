﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemquickSlot : MonoBehaviour
{
    public void OnPointerClick(BaseEventData Data)
    {
        Item_string item = this.GetComponent<Item_string>();
        PointerEventData eventData = Data as PointerEventData;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item.code != null)
            {
                Inventory.instance.player.Consume(false, item.code.Substring(0, 4), InventorySlot.GetType(item.code),
                                ItemManager.instance.GetOpt1_1(int.Parse(item.code.Substring(0, 5))),
                                ItemManager.instance.GetOpt2_1(int.Parse(item.code.Substring(0, 5))));
                Inventory.instance.minus_item(item.code);
                minus_item(item);
            }
        }

        if (eventData.button == PointerEventData.InputButton.Right && item.code != null)
        {
            if (Inventory.window_show == true)
            {
                item.code = null;
                this.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
                this.GetComponentInChildren<Text>().text = " ";
            }

        }
    }

    public void minus_item(Item_string item)
    {
        int count = int.Parse(item.code.Substring(5, 2)) - 1;
        if (count <= 0)
        {
            item.code = null;
            this.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
            this.GetComponentInChildren<Text>().text = " ";
            return;
        }
        string count_string = ((count < 10 ? "0" + count : "" + count));
        item.code = string.Copy(item.code.Substring(0, 5) + count_string);
        //해당 슬롯의 갯수출력변경
        this.GetComponentInChildren<Text>().text = (int.Parse(item.code.Substring(5, 2)) > 0 ?
             " " + int.Parse(item.code.Substring(5, 2)) : " ");
    }

    public void consumeItem() {
        Item_string item = this.GetComponent<Item_string>();
        if (item.code != null)
        {
            Inventory.instance.player.Consume(false, item.code.Substring(0, 4), InventorySlot.GetType(item.code),
                            ItemManager.instance.GetOpt1_1(int.Parse(item.code.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(item.code.Substring(0, 5))));
            Inventory.instance.minus_item(item.code);
            minus_item(item);
        }


    }
}
