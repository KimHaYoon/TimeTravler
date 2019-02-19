using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyInput : MonoBehaviour
{
    public Text storeTitleText;
    public StoreUI storeUI;
    public QuestUI QuestUI;
    //public GameObject Inventory;
    //public GameObject Skillwindow;

    void Update()
    {
        //UI Input Key

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (QuestUI.GetActive() == false)
            {
                QuestUI.SetActive(true);
            }

            else
            {
                QuestUI.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(touchPosition, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);

                if (hit.collider.name == "EquipmentStore")
                {
                    storeTitleText.text = "장비";
                    storeTitleText.name = "장비";
                    storeUI.gameObject.SetActive(true);
                    storeUI.salesList.ItemTag("장비");
                    storeUI.SalesListUpdate();

                }

                else if (hit.collider.name == "AccessoriesStore")
                {
                    storeTitleText.text = "잡화";
                    storeTitleText.name = "잡화";
                    storeUI.gameObject.SetActive(true);
                    storeUI.salesList.ItemTag("잡화");
                    storeUI.SalesListUpdate();
                }
            }
        }

        //// i 버튼을 이용해 아이템창을 여는 함수
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    if (Inventory.window_show == false)
        //    {
        //        Inventory.SetActive(true);
        //        Inventory.window_show = true;
        //    }
        //    else
        //    {
        //        Inventory.SetActive(false);
        //        Inventory.window_show = false;
        //        Inventory.instance.info.gameObject.SetActive(false);
        //    }
        //}

        //// k 버튼을 이용해 스킬창을 여는 함수
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    if (Skill_window.skill_window_show == false)
        //    {
        //        Skillwindow.SetActive(true);
        //        Skill_window.instance.button0();
        //        Skill_window.skill_window_show = true;
        //    }
        //    else
        //    {
        //        Skillwindow.SetActive(false);
        //        Skill_window.skill_window_show = false;
        //    }
        //}


    }
}
