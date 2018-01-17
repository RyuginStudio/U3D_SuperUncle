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

    public void createMap()
    {
        MapEditor.getInstance().createMapBlock2MapStruct();

        if (inputFiledName.text == "")
        {
            inputFiledName.text = "随机命名：" + Random.Range(1, 99.9f).ToString();
        }

        string path = Application.dataPath + "//Resources/MapConfig/" + inputFiledName.text + ".json";


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
            writer.WriteObjectEnd();
            writer.WriteObjectStart();
            writer.WritePropertyName("position.y");
            writer.Write(item.y);
            writer.WriteObjectEnd();
            writer.WriteObjectStart();
            writer.WritePropertyName("type");
            writer.Write(item.type);
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
