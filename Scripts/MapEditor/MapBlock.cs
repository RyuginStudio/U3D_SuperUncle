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
    //防止事件被连续触发的定时器
    private float currentTime;
    private float doEventTimeInterval;

    //存放预制体的transform
    public Transform TransformMapPack;

    //图块种类
    public int type;

    //可执行事件的次数
    public int canDoEventTimes;

    //图块附带事件种类
    public enum EventType
    {
        none,  //无事件
        coin,
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
        currentTime = Time.time;

        instance = this;

        TransformMapPack = GameObject.FindGameObjectWithTag("MapPack").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
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

        if (canDoEventTimes > 0 && currentTime - doEventTimeInterval > .2f)  //有剩余次数且防止连续触发
        {
            --canDoEventTimes;

            doEventTimeInterval = Time.time;

            DoEvent();  //执行事件分发
        }
        else if (canDoEventTimes == 0 && type == 1)  //问号=>石头
        {
            --canDoEventTimes;

            Destroy(gameObject, 0.6f);

            var stone = Instantiate(Resources.Load("Prefab/BlockPrefab/Ground_4"), transform.position, new Quaternion(), TransformMapPack);

            ((GameObject)stone).GetComponent<MapBlock>().type = 5;
        }
    }

    //图块事件分发
    public void DoEvent()
    {
        switch (BlockEvent)
        {
            case EventType.coin:
                {
                    gainCoin();
                    break;
                }

            default:
                break;
        }
    }

    //具体事件的执行内容
    #region ParticularEventFunc

    public void gainCoin()
    {
        AudioControler.getInstance().SE_Gain_Coin.Play();

        //加金币
        GameControler.getInstance().coinControl(1);

        //金币动画显示
        var pos = transform.position;
        var coinAnimPrefab = Instantiate(Resources.Load("Prefab/GainCoinPrefab"), new Vector2(pos.x, pos.y + 0.8f), new Quaternion());
        ((GameObject)coinAnimPrefab).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
        GameObject.Destroy(coinAnimPrefab, 0.53f);

        //加分延时执行
        StartCoroutine(GameControler.getInstance().ScoreUIControl(100,transform.localPosition, 0.6f));
    }

    #endregion

}
