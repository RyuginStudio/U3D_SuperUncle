/*
 * 时间：2018年3月22日19:35:31
 * 作者：vszed
 * 功能：调控任务显示界面
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionView : MonoBehaviour
{
    //踩在脚下的任务“完成point”
    [SerializeField] private GameObject prefab_stepMission;

    //prefab实例化始、末位置
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;

    //存放prefab
    [SerializeField] private Transform prefabTransform;

    //当前生命值
    [SerializeField] private Text currentLives;

    //当前已完成关卡数目
    [SerializeField] private Text FinishMissionViewNum;

    [SerializeField] private GameObject MarioUI;

    //MarioUI抬手
    [SerializeField] private Sprite MarioUIHandsUp;

    // Use this for initialization
    void Start()
    {
        currentLives.GetComponent<Text>().text = "x " + GameData.MarioLives.ToString();
        FinishMissionViewNum.GetComponent<Text>().text = (GameData.currentMissionNum - 1).ToString() + " / " + GameData.missionTotalNum;

        StartCoroutine(displayNoticeCard());
        StartCoroutine(displayAllPrefab());

    }

    // Update is called once per frame
    void Update()
    {

    }

    //存放脚下的任务“完成point”的容器
    public List<GameObject> list_stepMission = new List<GameObject>();

    //根据关卡数实例prefab
    private IEnumerator displayAllPrefab()
    {
        var startPos = start.transform.position;
        var endPos = end.transform.position;
        var eachPreSpace = (endPos.x - startPos.x) / (GameData.missionTotalNum - 1);

        for (int i = 0; i < GameData.missionTotalNum; i++)
        {
            if (!GameData.alreadyTeaching)
            {
                yield return new WaitForSeconds(0.2f);
            }

            var prefab_point = Instantiate(prefab_stepMission, new Vector3(startPos.x + i * eachPreSpace, startPos.y, startPos.z), prefab_stepMission.transform.rotation, prefabTransform);
            prefab_point.GetComponentInChildren<Text>().text = (i + 1).ToString();
            list_stepMission.Add(prefab_point);
        }

        updateprefab();
        StartCoroutine(updateMarioUIPos());
    }

    //显示"新手教学"牌
    private IEnumerator displayNoticeCard()
    {
        yield return new WaitForSeconds(0.2f * GameData.missionTotalNum + 2);

        if (!GameData.alreadyTeaching)
        {
            var pre = Instantiate(Resources.Load("Prefab/UI/NoticeCard"), prefabTransform);
            (pre as GameObject).transform.localPosition = new Vector3(-13, 556, 0);
            (pre as GameObject).GetComponent<NoticeCard>().fallToPos = new Vector3(-13, 37, 0);
            (pre as GameObject).GetComponent<NoticeCard>().str_title = "碧琪公主遇到危险了";
            (pre as GameObject).GetComponent<NoticeCard>().str_Content = "    1. W A S D 控制大叔上下左右移动     \n    2. J 控制大叔跳跃     \n    3. L 控制大叔加速运动     \n\n    拯救公主的使命就交给你了！！！";

            Destroy(pre, 10);
        }

        GameData.alreadyTeaching = true;
    }

    //刷新已打穿的关卡预制体
    private void updateprefab()
    {
        for (var i = 0; i < list_stepMission.Count && i < GameData.currentMissionNum - 1; ++i)
        {
            foreach (var item in list_stepMission[i].GetComponentsInChildren<Image>())
            {
                item.enabled = true;
            }
        }
    }

    private IEnumerator updateMarioUIPos()
    {
        if (GameData.currentMissionNum >= 2)
        {
            var pos = list_stepMission[GameData.currentMissionNum - 2].transform.position;
            MarioUI.transform.position = new Vector3(pos.x, MarioUI.transform.position.y, pos.z);
        }

        //马里奥UI向新的关卡point移动
        var waitSeconds = GameData.alreadyTeaching ? 3 : 12.5f;
        yield return new WaitForSeconds(waitSeconds);
        var currentMissionPos = list_stepMission[GameData.currentMissionNum - 1].transform.position;
        MarioUI.transform.position = new Vector3(currentMissionPos.x, MarioUI.transform.position.y, currentMissionPos.z);

        AudioControler.getInstance().SE_MoveMap.Play();

        yield return new WaitForSeconds(1);
        MarioUI.GetComponent<Image>().sprite = MarioUIHandsUp;

        //贴图问题修正pos
        var temp_pos = MarioUI.transform.position;
        MarioUI.transform.position = new Vector3(temp_pos.x - 3, temp_pos.y, temp_pos.z);

        StartCoroutine("jumpScene");
    }

    private IEnumerator jumpScene()
    {
        yield return new WaitForSeconds(0.5f);
        AudioControler.getInstance().SE_Daikettefinal.Play();
        StartCoroutine(SceneTransition.getInstance().loadScene("LivesScene", 0, 2));

    }
}
