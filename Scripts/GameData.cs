﻿/*
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

    //===========================游戏数据===========================//

    [SerializeField] public static string mapName = "map2";   //地图关卡索引
    [SerializeField] public static int blockNum = 247;        //地图图块数目

}
