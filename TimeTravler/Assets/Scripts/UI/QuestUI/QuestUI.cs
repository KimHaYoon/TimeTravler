using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public QuestData MainQuest;
    //public List<QuestData> SubQuest = new List<QuestData>();

    public Text QuestName;
    public Text QuestGoal;
    public Text QuestReward;

    private bool Active;

    // Start is called before the first frame update
    void Start()
    {
        SetMainQuest("멍청이", 1, "으엑", "나닛");
        gameObject.SetActive(false);
        Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(MainQuest != null)
        {
            QuestName.text = MainQuest.name;
            QuestGoal.text = MainQuest.count + " / " + MainQuest.goal;
            QuestReward.text = MainQuest.reward;
        }

        MainQuestCountCheck();
        MainQuestClearCheck();
    }

    private void MainQuestClearCheck()
    {
        if (MainQuest.clear == true)
        {
            MainQuest = new QuestData();
        }
    }

    public void SetMainQuest(string QuestName, int Goal, string Reward, string ObjName = "NONE")
    {
        MainQuest = new QuestData();

        if (ObjName == "NONE")
            MainQuest.name = QuestName;

        else
            MainQuest.name = ObjName + " " + QuestName;

        MainQuest.objname = ObjName;
        MainQuest.count = 0;
        MainQuest.goal = Goal;
        MainQuest.reward = Reward;
        MainQuest.clear = false;
    }

    private void MainQuestCountCheck()
    {
        if (MainQuest.count == MainQuest.goal)
            MainQuest.clear = true;
    }

    public bool GetActive()
    {
        return Active;
    }

    public void SetActive(bool active)
    {
        Active = active;
        gameObject.SetActive(Active);
    }

    public void SetCount(int count)
    {
        MainQuest.count = count;
    }

    public void Check(string objName)
    {
        if (MainQuest.objname == objName)
        {
            MainQuest.count++;
        }
    }

    //public void AddSubQuest(string QuestName, string Goal, string Reward)
    //{
    //    QuestData subQuest = new QuestData();

    //    subQuest.name = QuestName;
    //    subQuest.count = "0";
    //    subQuest.goal = Goal;
    //    subQuest.reward = Reward;
    //    subQuest.clear = false;

    //    SubQuest.Add(subQuest);
    //}


    //private void SubQuestClearCheck()
    //{
    //    for(int i = 0; i <  SubQuest.Count; ++i)
    //    {
    //        if(SubQuest[i].clear == true)
    //        {
    //            SubQuest.Remove(SubQuest[i]);
    //        }
    //    }
    //}
}
