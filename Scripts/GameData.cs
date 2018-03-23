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

    }

    // Update is called once per frame
    void Update()
    {

    }

    //刷新数据
    public static void dataUpdate()
    {
        alreadyTeaching = false;
        mapName = "map1";
        missionTotalNum = 30;
        currentMissionNum = 1;
        MarioLives = 12;

    }

    //===========================游戏数据===========================//

    public static bool alreadyTeaching = false;  //完成新手教学
    public static string mapName = "map1";       //地图关卡索引
    public static int missionTotalNum = 30;      //地图关卡总数
    public static int currentMissionNum = 1;     //当前关卡数目

    //===========================角色数据===========================//

    public static int MarioLives = 12;
}
