/*
 * 时间：2018年1月25日11:15:07
 * 作者：vszed
 * 功能：游戏流程总控制器：倒计时、游戏结束等
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    //存放图块的transform
    public Transform TransformMapBlock;

    //倒计时时间label
    public GameObject LabelTimeLeft;

    //定时器
    private float currentTime;
    private float countDownUpdate;  //倒计时

    //游戏结束开关
    public bool GameOver;

    // Use this for initialization
    void Start()
    {
        countDownUpdate = Time.time;
        loadMap();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        if (currentTime - countDownUpdate >= 1 && GameOver == false)
        {
            countDownUpdate = Time.time;
            countDown();
        }
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
            ((GameObject)prefabBlock).GetComponent<MapBlock>().type = type;
        }
    }

    //游戏倒计时
    public void countDown()
    {
        //Debug.Log(LabelTimeLeft.GetComponent<Text>().text);
        var str_timeLeft = LabelTimeLeft.GetComponent<Text>().text;
        int result = 0;
        int.TryParse(str_timeLeft, out result);
        result = result <= 0 ? 0 : --result;
        str_timeLeft = result.ToString();

        switch (str_timeLeft.Length)
        {
            case 1:
                str_timeLeft = "00" + str_timeLeft;
                break;
            case 2:
                str_timeLeft = "0" + str_timeLeft;
                break;
            default:
                break;
        }

        LabelTimeLeft.GetComponent<Text>().text = str_timeLeft;

        if (result <= 100 && !AudioControler.getInstance().BGM_Ground_Hurry.isPlaying)
        {
            //BGM
            AudioControler.getInstance().BGM_Ground.Stop();
            AudioControler.getInstance().BGM_Ground_Hurry.PlayDelayed(2.5f);

            //SE
            AudioControler.getInstance().SE_HurryUp.Play();
        }
        else if (result == 0)
        {
            gameOver();
        }
    }

    //游戏结束 TODO：具体逻辑没写
    public void gameOver()
    {
        Debug.Log("GameOver");
        GameOver = true;

        //BGM
        AudioControler.getInstance().BGM_Ground.Stop();
        AudioControler.getInstance().BGM_Ground_Hurry.Stop();

        //SE
        AudioControler.getInstance().SE_Die1.Play();
        AudioControler.getInstance().SE_Die2.PlayDelayed(0.5f);
        AudioControler.getInstance().SE_OhNo.PlayDelayed(0.5f);        
    }
}