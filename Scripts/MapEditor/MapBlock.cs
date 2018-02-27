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
    //存放预制体的transform
    public Transform TransformMapPack;

    //图块种类
    public int type;

    //能否触发事件
    public bool canDoEvent = true;

    //图块附带事件种类
    public enum EventType
    {
        coin,
        mushRoom,

    }
    public EventType BlockEvent;

    private static MapBlock instance;

    public static MapBlock getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;

        TransformMapPack = GameObject.FindGameObjectWithTag("MapPack").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BlockCollision()
    {
        //播放碰撞音效
        if (!AudioControler.getInstance().SE_Hit_Block.isPlaying)
        {
            AudioControler.getInstance().SE_Hit_Block.Play();
        }

        //碰撞动画控制(如果有动画的话：需要有isHit参数)
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetBool("isHit", true);
        }

        if (canDoEvent)
        {
            DoEvent();
        }       
    }

    //图块事件
    public void DoEvent()
    {
        switch (BlockEvent)
        {
            case EventType.coin:
                break;
            case EventType.mushRoom:
                break;
            default:
                break;
        }
    }

    //void collideQuestion()
    //{
    //    if (canDoEvent)
    //    {
    //        canDoEvent = false;

    //        Destroy(gameObject, 0.6f);

    //        var stone = Instantiate(Resources.Load("Prefab/BlockPrefab/Ground_5"), transform.position, new Quaternion(), TransformMapPack);

    //        ((GameObject)stone).GetComponent<MapBlock>().type = 5;
    //    }
    //}

    //根据某些具体图块决定是否可重置
    private void resetCanDoEvent()
    {
        canDoEvent = true;
    }
}
