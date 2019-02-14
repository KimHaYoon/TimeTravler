using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class equipslot : MonoBehaviour
{
    public ItemInfo info;

    public void OnPointerClick(BaseEventData Data)
    {
        Item_string item = this.GetComponent<Item_string>();
        PointerEventData eventData = Data as PointerEventData;

        if (eventData.button == PointerEventData.InputButton.Left) ;
        //왼쪽클릭


        if (eventData.button == PointerEventData.InputButton.Right && item.code != null)
        {
            //오른쪽클릭 && 아이템 장착해제
            Debug.Log("뺀다");
            if (Inventory.instance.current_count >= Inventory.instance.inventory_max)
                return;
            if ((item.code.Substring(0, 3) == "141" || item.code.Substring(0, 3) == "142"))
            {
                Inventory.instance.equipment_Shield.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
                InventorySlot.sheild = true;
            }
            Inventory.instance.player.Consume(false, item.code.Substring(0, 4), InventorySlot.GetType(item.code),
                            ItemManager.instance.GetOpt1_1(int.Parse(item.code.Substring(0, 5))),
                            ItemManager.instance.GetOpt2_1(int.Parse(item.code.Substring(0, 5))));
            Inventory.instance.Add(this.gameObject.GetComponent<Item_string>().code);
            this.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
            item.code = null;
        }


    }

    public void OnTriggerEnter2D()
    {
        if (this.GetComponent<Item_string>().code != null)
        {
            info.SetItem(ItemManager.instance.GetItemInfo(int.Parse(this.GetComponent<Item_string>().code.Substring(0, 5))));
            info.gameObject.SetActive(true);
        }
    
        //info.gameObject.GetComponent<RectTransform>().position = 

        Vector3 position = gameObject.GetComponent<RectTransform>().position;

        //info.gameObject.GetComponent<RectTransform>().position = new Vector3(position.x - 120, position.y - 40);
    }

    public void OnTriggerExit2D()
    {
        info.gameObject.SetActive(false);
    }

}