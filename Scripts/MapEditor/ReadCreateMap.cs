/*
 * 时间：2018年1月17日02:48:04
 * 作者：vszed
 * 功能：利用LitJson库生成JSON格式地图
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class ReadCreateMap : MonoBehaviour
{
    public InputField inputFiledName;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createMap(GameObject obj)
    {
        //Debug.Log("Create Map");
        //var x = JsonMapper.ToJson(obj);

        if (inputFiledName.text == "")
        {
            inputFiledName.text = "随机命名：" + Random.Range(1, 99.9f).ToString();
        }

        string path = Application.dataPath + "//Resources/MapConfig/" + inputFiledName.text + ".json";
        DirectoryInfo myDirectoryInfo = new DirectoryInfo(path);
        File.Create(path);

        if (myDirectoryInfo.Exists)
            print("this file already exists!");
        else print("create success!");
    }

    public void readMap()
    {
        //Debug.Log("Read Map");
        var JsonFile = Resources.Load(@"MapConfig/mapConfig") as TextAsset;
        var JsonObj = JsonMapper.ToObject(JsonFile.text);
        var JsonItems = JsonObj["MapBlocks"];

        foreach (JsonData item in JsonItems)
        {
            Debug.Log("x:" + item["position.x"]);
            Debug.Log("y:" + item["position.y"]);
            Debug.Log("type:" + item["type"]);
        }
    }

}
