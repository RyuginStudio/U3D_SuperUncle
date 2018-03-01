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
using System.Text;
using System;

public class ReadCreateMap : MonoBehaviour
{
    private static ReadCreateMap instance;
    public static ReadCreateMap getInstance()
    {
        return instance;
    }

    public InputField inputFiledName;

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createMap()  //写入地图
    {
        MapEditor.getInstance().createMapBlock2MapStruct();

        if (inputFiledName.text == "")
        {
            inputFiledName.text = "随机命名：" + UnityEngine.Random.Range(1, 99.9f).ToString();
        }

        string path = Application.dataPath + "//Resources/MapConfig/" + inputFiledName.text + ".json";

        //检查是否存在该名字的地图
        if (IsFileExists(Application.dataPath + "//Resources/MapConfig/" + inputFiledName.text + ".json"))
        {
            var MessageBox = Instantiate(Resources.Load("Prefab/MessageBox"), GameObject.Find("Canvas").transform);

            ((GameObject)MessageBox).GetComponentInChildren<Text>().text = "您所设定的地图名称与本地地图库中现有地图的名称发生重复！是否要保存新地图从而覆盖掉旧地图？？？";

            var btnPack = ((GameObject)MessageBox).GetComponentsInChildren<Button>();

            foreach (var btn in btnPack)
            {
                if (btn.name == "ConfirmBtn")
                {
                    btn.onClick.AddListener(delegate ()       //委托：http://blog.csdn.net/cocos2der/article/details/42705885
                    {
                        doCreateMap(path);

                        ((GameObject)MessageBox).GetComponentInChildren<Text>().text = "新地图已完成覆盖！！！";

                        foreach (var item in btnPack)
                        {
                            GameObject.Destroy(item);
                        }

                        GameObject.Destroy(MessageBox, 2);
                    });
                }
                else if (btn.name == "CancelBtn")
                {
                    btn.onClick.AddListener(delegate ()
                    {
                        GameObject.Destroy(MessageBox);
                    });
                }
            }
        }
        else
        {
            doCreateMap(path);
        }
    }

    private void doCreateMap(string path)
    {
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteObjectStart();

        writer.WritePropertyName("MapBlocks");

        writer.WriteArrayStart();

        foreach (var item in MapEditor.getInstance().MapStructList)
        {
            //Debug.Log(MapEditor.getInstance().MapStructList.Count);
            writer.WriteObjectStart();
            writer.WritePropertyName("position.x");
            writer.Write(item.x);
            writer.WritePropertyName("position.y");
            writer.Write(item.y);
            writer.WritePropertyName("type");
            writer.Write(item.type);
            writer.WritePropertyName("blockEvent");
            writer.Write(item.blockEvent);
            writer.WritePropertyName("doEventTimes");
            writer.Write(item.doEventTimes);
            writer.WriteObjectEnd();
        }

        writer.WriteArrayEnd();

        writer.WriteObjectEnd();


        JsonData jd = JsonMapper.ToObject(sb.ToString());

        JsonData jdItems = jd["MapBlocks"];

        StreamWriter sw;
        sw = File.CreateText(path);

        //写入  
        sw.WriteLine(sb);
        //关闭  
        sw.Close();
    }

    public void readMap()  //读取已有地图
    {
        //Debug.Log("Read Map");
        //ps: var JsonFile = Resources.Load(@"MapConfig/mapConfig") as TextAsset;
        var JsonFile = Resources.Load(@"MapConfig/" + inputFiledName.text) as TextAsset;
        var JsonObj = JsonMapper.ToObject(JsonFile.text);
        var JsonItems = JsonObj["MapBlocks"];

        foreach (JsonData item in JsonItems)
        {
            //Debug.Log("x:" + item["position.x"]);
            //Debug.Log("y:" + item["position.y"]);
            //Debug.Log("type:" + item["type"]);

            var x = Convert.ToSingle(item["position.x"].ToString());
            var y = Convert.ToSingle(item["position.y"].ToString());
            var type = int.Parse(item["type"].ToString());
            var blockEvent = item["blockEvent"].ToString();
            var doEventTimes = int.Parse(item["doEventTimes"].ToString());

            MapEditor.getInstance().drawBlock(new Vector3(x, y, 0), type, blockEvent, doEventTimes);
        }
    }

    // 检测文件是否存在Application.dataPath目录
    public static bool IsFileExists(string fileName)
    {
        if (fileName.Equals(string.Empty))
        {
            return false;
        }

        return File.Exists(Path.GetFullPath(fileName));
    }
}
