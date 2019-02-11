using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class quickSlot : MonoBehaviour
{
    public void OnPointerClick(BaseEventData Data)
    {
        Item_string item = this.GetComponent<Item_string>();
        PointerEventData eventData = Data as PointerEventData;

        if (eventData.button == PointerEventData.InputButton.Left) ;
        //왼쪽클릭

        if (eventData.button == PointerEventData.InputButton.Right && item.code != null)
        {
            Inventory.instance.minus_item(item.code);
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

            //오른쪽클릭 && 아이템 장착해제
            /*if (Inventory.window_show == true)
            {
                item.code = null;
                this.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
                this.GetComponentInChildren<Text>().text = " ";
            }*/
        }
    }
}
