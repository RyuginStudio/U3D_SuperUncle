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

        //根据type分发
        switch (type)
        {
            case 0:  //砖块
                {
                    collideBrick();
                    break;
                }
            case 1:  //问号
                {
                    collideQuestion();
                    break;
                }
            default:
                break;
        }
    }

    #region BlockCollision

    void collideBrick()
    {
        GetComponent<Animator>().SetBool("isHit", true);
    }

    void collideQuestion()
    {
        if (canDoEvent)
        {
            canDoEvent = false;

            GetComponent<Animator>().SetBool("isHit", true);

            Destroy(gameObject, 0.6f);

            var stone = Instantiate(Resources.Load("Prefab/BlockPrefab/Ground_5"), transform.position, new Quaternion(), TransformMapPack);

            ((GameObject)stone).GetComponent<MapBlock>().type = 5;
        }
    }

    #endregion

    //根据某些具体图块决定是否可重置
    private void resetCanDoEvent()
    {
        canDoEvent = true;
    }
}
