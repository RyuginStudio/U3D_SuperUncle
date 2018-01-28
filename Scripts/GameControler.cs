/*
 * 时间：2018年1月25日11:15:07
 * 作者：vszed
 * 功能：游戏流程总控制器
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;

public class GameControler : MonoBehaviour
{
    //存放图块的transform
    public Transform TransformMapBlock;

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
        string mapName = GameData.mapName;
        //Debug.Log(mapName);

        var JsonFile = Resources.Load(@"MapConfig/" + mapName) as TextAsset;
        var JsonObj = JsonMapper.ToObject(JsonFile.text);
        var JsonItems = JsonObj["MapBlocks"];

        foreach (JsonData item in JsonItems)
        {
            var x = Convert.ToSingle(item["position.x"].ToString());
            var y = Convert.ToSingle(item["position.y"].ToString());
            var type = int.Parse(item["type"].ToString());

            
            //MapEditor.getInstance().drawBlock(new Vector3(x, y, 0), type);

            var prefabBlock = Instantiate(Resources.Load("Prefab/BlockPrefab/Ground_" + type.ToString()), new Vector3(x, y, 0), new Quaternion(), TransformMapBlock);

            //坐标作为图块Name
            prefabBlock.name = new Vector3(x, y, 0).ToString();
            //prefabBlock.GetComponent<BlockType>().type = type;
        }
    }
}
