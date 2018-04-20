/*
 * 时间：2018年1月25日11:12:06
 * 作者：vszed
 * 功能：游戏数据一览
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        currentMissionNum = PlayerPrefs.GetInt("currentMissionNum", 1);
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //刷新数据
    public static void dataUpdate()
    {
        missionTotalNum = 30;
        currentMissionNum = PlayerPrefs.GetInt("currentMissionNum", 1);
        MarioLives = 12;
        Flag.alreadyGetFlag = false;
        GetScore = 0;
        CostTime = 0;
    }

    //===========================游戏数据===========================//

    public static int missionTotalNum = 30;  //地图关卡总数
    public static int currentMissionNum;     //当前关卡

    //===========================玩家数据===========================//

    public static int MarioLives = 12;
    public static int GetScore = 0;             //关卡得分
    public static int CostTime = 0;             //关卡耗时
}
