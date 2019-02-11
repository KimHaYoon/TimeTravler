using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class equipslot : MonoBehaviour
{

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
            Inventory.instance.Add(this.gameObject.GetComponent<Item_string>().code);
            this.GetComponent<Image>().sprite = Inventory.instance.defaultImage;
            item.code = null;
        }


    }

}