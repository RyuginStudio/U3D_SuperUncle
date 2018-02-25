/*
 * 更改时间：2018年2月25日12:21:22
 * 作者：vszed
 * 功能：地图图块相关 => 图块type、碰撞事件
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    //图块种类
    public int type;

    private static MapBlock instance;

    public static MapBlock getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //图块碰撞事件：根据type值匹配分发
    public void collisionEvent()
    {
        switch (type)
        {
            case 0:  //砖块
                {
                    brickEvent();
                    break;
                }
            default:
                break;
        }
    }

    #region BlockEvent

    void brickEvent()
    {
        Debug.Log("brickEvent");
    }

    #endregion
}
