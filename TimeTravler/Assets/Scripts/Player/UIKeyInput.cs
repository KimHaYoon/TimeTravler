using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyInput : MonoBehaviour
{
    public Text storeTitleText;
    public StoreUI storeUI;
    public QuestUI QuestUI;
    public GameObject Inventorywindow;
    private Player player;
    //public GameObject Skillwindow;

<<<<<<< HEAD
    private void Start()
    {
        Inventorywindow.SetActive(false);
=======

    private void Start()
    {
        player = GetComponent<Player>();
>>>>>>> 811e4cb3e983f9db7b0e1c38ee3ad1cdcaa4d932
    }

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
                    storeUI.InventoryEnable(true);
                }

                else if (hit.collider.name == "AccessoriesStore")
                {
                    storeTitleText.text = "잡화";
                    storeTitleText.name = "잡화";
                    storeUI.gameObject.SetActive(true);
                    storeUI.salesList.ItemTag("잡화");
                    storeUI.SalesListUpdate();
                    storeUI.InventoryEnable(true);
                }
            }
        }

        // i 버튼을 이용해 아이템창을 여는 함수
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Inventory.window_show == false)
            {
                Inventorywindow.SetActive(true);
                Inventory.window_show = true;
            }
            else
            {
                Inventorywindow.SetActive(false);
                Inventory.window_show = false;
                Inventory.instance.info.gameObject.SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.X))//퀵슬롯 스킬1
            QuickSkill(0);

        if (Input.GetKey(KeyCode.C))//퀵슬롯 스킬2
            QuickSkill(1);

        if (Input.GetKey(KeyCode.C))//퀵슬롯 스킬3
            QuickSkill(2);

        if (Input.GetKey(KeyCode.V))//퀵슬롯 아이템1
            QuickItem(0);

        if (Input.GetKey(KeyCode.B))//퀵슬롯 아이템2
            QuickItem(0);



        // k 버튼을 이용해 스킬창을 여는 함수
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

    private void QuickSkill(int num)//num 몇번째 슬롯 호출하는지
    {
        if (player.isAttack) return;//플레이어가 공격중(스킬 or 평타)이라면 종료
        int skill = skillslot.slots[num].GetComponent<skill_ob>().skill;
        //쿨타임체크먼저해야됨 안되면 return 해서 종료;
        switch (skill)
        {
            case 1:
                if (player.isCurrentSkill)
                    return;
                break;
            case 2:
                if (player.isCurrentSkill)
                    return;
                break;
            case 3:
                if (player.isCurrentSkill)
                    return;
                break;
            case 4:
                if (player.isSplashForce)
                    return;
                break;
            case 5:
                if (player.isFlareBall)
                    return;
                break;
            case 6:
                if (player.isPierceSpear)
                    return;
                break;
        }
        //쿨타임아님 스킬실행
        player.myRigidbody.velocity = new Vector2(0, player.myRigidbody.velocity.y);

        
        player.skill = skill;
        if (skill > 3)
            player.skill = 4;
        //플레이어 skill변경 후 스킬애니메이션 설정

        switch (skill)
        {
            case 1:
                player.myRigidbody.velocity = new Vector2(0, -10);//제자리 정지
                player.myAnimator.Play("Player_Skill4_1");
                break;
            case 2:
                player.myAnimator.Play("Player_Skill4_2");
                break;
            case 3:
                player.myAnimator.Play("Player_Skill4_3");
                break;
            case 4:
                if (player.weapon == 1)
                    player.myAnimator.Play("Player_Skill1");
                break;
            case 5:
                if (player.weapon == 2)
                    player.myAnimator.Play("Player_Skill2");
                break;
            case 6:
                if (player.weapon == 3)
                    player.myAnimator.Play("Player_Skill3");
                break;
        }
        player.isAttack = true;
        //공격중으로 체크
    }

    private void QuickItem(int num)//num 몇번째 슬롯 호출하는지
    {
        Itemslot.instance.slots[num].GetComponent<ItemquickSlot>().consumeItem();
    }

}
