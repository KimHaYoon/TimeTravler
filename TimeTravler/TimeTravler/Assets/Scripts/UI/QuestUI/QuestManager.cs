using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance = null;
    public static QuestManager GetInstance()
    {
        if (instance == null)
            instance = new QuestManager();

        return instance;
    }
    
    public List<QuestData>  MainQuestList = new List<QuestData>();

    void Awake()
    {
        instance = this;

        AddMainQuest("소탕하기", "10", "StageClear", "뱀");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddMainQuest(string QuestName, string Goal, string Reward, string ObjName = "NONE")
    {
        QuestData Quest = new QuestData();

        if (ObjName == "None")
            Quest.name = QuestName;

        else
            Quest.name = ObjName + " " + QuestName;

        Quest.count = "0";
        Quest.goal = Goal;
        Quest.reward = Reward;
        Quest.clear = false;

        MainQuestList.Add(Quest);
    }

    public QuestData GetQuest(string QuestName)
    {
        QuestData Quest = new QuestData();

        for (int i = 0; i < MainQuestList.Count; ++i)
        {
            if (MainQuestList[i].name == QuestName)
            {
                Quest = MainQuestList[i];
                break;
            }
        }

        return Quest;
    }
}
