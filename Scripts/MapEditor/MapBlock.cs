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

    //存放敌人的transform
    public Transform TransformEnemyPack;

    //图块种类
    public int type;

    //可执行事件的次数
    public int canDoEventTimes = 0;

    //图块附带事件种类
    public enum EventType
    {
        None,  //无事件
        Coin,
        Goomba,
        TortoiseFly,
        TortoiseLand
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
        TransformEnemyPack = GameObject.FindGameObjectWithTag("EnemyPack").transform;
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

        if (canDoEventTimes == 0 && type == 1)  //问号=>石头
        {
            --canDoEventTimes;

            Destroy(gameObject, 0.65f);

            var stone = Instantiate(Resources.Load("Prefab/BlockPrefab/Ground_4"), transform.position, new Quaternion(), TransformMapPack);

            ((GameObject)stone).GetComponent<MapBlock>().type = 5;
        }
    }

    //图块事件分发
    public void DoEvent()
    {
        switch (BlockEvent)
        {
            case EventType.Coin:
                {
                    gainCoin();
                    break;
                }
            case EventType.Goomba:
                {
                    createGoomba();
                    break;
                }
            case EventType.TortoiseFly:
                {
                    createTortoiseFly();
                    break;
                }
            case EventType.TortoiseLand:
                {
                    createTortoiseLand();
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
        var coinAnimPrefab = Instantiate(Resources.Load("Prefab/UI/GainCoinPrefab"), new Vector2(pos.x, pos.y + 0.8f), new Quaternion());
        ((GameObject)coinAnimPrefab).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
        GameObject.Destroy(coinAnimPrefab, 0.53f);

        //加分延时执行
        StartCoroutine(GameControler.getInstance().ScoreUIControl(100, transform.localPosition, 0.55f));
    }

    public void createGoomba()
    {
        AudioControler.getInstance().SE_Appear.Play();
        var pos = transform.position;
        var goomba = Instantiate(Resources.Load("Prefab/Enemy/Goomba"), new Vector2(pos.x, pos.y + 0.2f), new Quaternion(), TransformEnemyPack);
        ((GameObject)goomba).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
    }

    public void createTortoiseLand()
    {
        AudioControler.getInstance().SE_Appear.Play();
        var pos = transform.position;
        var tortoise = Instantiate(Resources.Load("Prefab/Enemy/TortoiseLand"), new Vector2(pos.x, pos.y + 0.5f), new Quaternion(), TransformEnemyPack);
        ((GameObject)tortoise).GetComponent<Tortoise>().TortoiseStatus = Tortoise.Status.isOnFoot;
        ((GameObject)tortoise).GetComponent<Tortoise>().TortoiseDirection = Tortoise.direction.left | Tortoise.direction.right;
        ((GameObject)tortoise).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
    }
    public void createTortoiseFly()
    {
        AudioControler.getInstance().SE_Appear.Play();
        var pos = transform.position;
        var tortoise = Instantiate(Resources.Load("Prefab/Enemy/TortoiseFly"), new Vector2(pos.x, pos.y + 1.1f), new Quaternion(), TransformEnemyPack);
        ((GameObject)tortoise).GetComponent<Tortoise>().TortoiseStatus = Tortoise.Status.isFly;
    }

    #endregion

}
