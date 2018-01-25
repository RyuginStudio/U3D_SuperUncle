/*
 * 时间：2018年1月25日11:15:07
 * 作者：vszed
 * 功能：游戏流程总控制器
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        loadMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loadMap()  //根据GameDt加载地图
    {
        string mapName = @"map" + GameData.mapIdx.ToString();
        Debug.Log(mapName);
        
        //ReadCreateMap.getInstance().readMap(mapName);
    }
}
