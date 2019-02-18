using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillquickSlot : MonoBehaviour
{
    public void OnPointerClick(BaseEventData Data)
    {
        Debug.Log("클릭");
        PointerEventData eventData = Data as PointerEventData;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("왼쪽클릭");

        }


        if (eventData.button == PointerEventData.InputButton.Right)
        {
            //오른쪽클릭
            Debug.Log("오른쪽 클릭");
            if (Skill_window.skill_window_show == true)
            {
                Skill_window.instance.slot_now[this.GetComponent<index>().Index - 1].GetComponent<skill_ob>().skill = null;
                skillslot.instance.update();
            }
        }
    }

   
}
