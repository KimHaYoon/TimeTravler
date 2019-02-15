using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class skillChoiceslot : MonoBehaviour
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
            insert(this.GetComponent<skill_ob>());
            skillslot.instance.update();
        }
    }

    void insert(skill_ob choice_skill)
    {
        GameObject[] slot_now = Skill_window.instance.slot_now;
        for (int i = 0, count = skillslot.slotcount ; i < count; i++)
        {
            if (slot_now[i].GetComponent<skill_ob>().skill == null)
            {
                slot_now[i].GetComponent<skill_ob>().skill = choice_skill.skill;
                return;
            }
        }
    }

}
