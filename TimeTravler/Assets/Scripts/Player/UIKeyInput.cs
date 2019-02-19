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
    public GameObject Player;
    private Player player;

    void Start()
    {
        player = Player.GetComponent<Player>();
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

        //스킬 단축키 Input
        if (Input.GetKeyDown(KeyCode.X))
            QuickSkill(0);
        if (Input.GetKeyDown(KeyCode.C))
            QuickSkill(1);
        if (Input.GetKeyDown(KeyCode.V))
            QuickSkill(2);
        if (Input.GetKeyDown(KeyCode.B))
            QuickItem(0);
        if (Input.GetKeyDown(KeyCode.N))
            QuickItem(1);


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


    private void QuickSkill(int num)
    {
        int skill = skillslot.slots[num].GetComponent<skill_ob>().skill;
        Debug.Log(skillslot.slots[num].GetComponent<skill_ob>().skill);
        //쿨타임체크
        switch (skill)
        {
            case 1:
                if (player.isPierceSpear) return;
                break;
            case 2:
                if (player.isFlareBall) return;
                break;
            case 3:
                if (player.isSplashForce) return;
                break;
            case 4:
            case 5:
            case 6:
                if (player.isCurrentSkill) return;
                break;
        }
        //쿨타임 아니면
        switch (skill)
        {
            case 1:
                if (player.weapon == 1)
                {
                    player.myRigidbody.velocity = new Vector2(0, player.myRigidbody.velocity.y);//제자리 정지
                    player.myAnimator.Play("Player_Skill4_1");
                }
                break;
            case 2:
                if (player.weapon == 2)
                {
                    player.myRigidbody.velocity = new Vector2(0, player.myRigidbody.velocity.y);//제자리 정지
                    player.myAnimator.Play("Player_Skill4_2");
                }
                break;
            case 3:
                if (player.weapon == 3)
                {
                    player.myRigidbody.velocity = new Vector2(0, player.myRigidbody.velocity.y);//제자리 정지
                    player.myAnimator.Play("Player_Skill4_3");
                }
                break;
            case 4:
                player.myRigidbody.velocity = new Vector2(0, -10);//제자리 정지
                player.myAnimator.Play("Player_Skill1");
                break;
            case 5:
                player.myRigidbody.velocity = new Vector2(0, player.myRigidbody.velocity.y);//제자리 정지
                player.myAnimator.Play("Player_Skill2");
                break;
            case 6:
                player.myRigidbody.velocity = new Vector2(0, player.myRigidbody.velocity.y);//제자리 정지
                player.myAnimator.Play("Player_Skill3");
                break;
        }
        if (skill > 3) skill = 4;
        player.skill = skill;
        player.isAttack = true;
    }

    private void QuickItem(int num)
    {
        Itemslot.instance.slots[num].GetComponent<ItemquickSlot>().consumeItem();
        Debug.Log(Itemslot.instance.slots[num].GetComponent<Item_string>().code);
    }
}
