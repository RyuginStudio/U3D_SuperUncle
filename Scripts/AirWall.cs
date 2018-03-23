/*
 * 时间：2018年3月23日20:12:55
 * 作者：vszed
 * 功能：空气墙
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirWall : MonoBehaviour
{
    [SerializeField] private GameObject LeftWall;
    [SerializeField] private GameObject RightWall;
    [SerializeField] private GameObject UpWall;
    [SerializeField] private GameObject mapBlocks;

    // Use this for initialization
    void Start()
    {
        Invoke("updateAirWallViaBlockPos", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateAirWallViaBlockPos()  //获取所有图块x最大值
    {
        float max_X = 0;
        float min_X = 100;

        foreach (var item in mapBlocks.GetComponentsInChildren<Transform>())
        {
            if (item.position.x > max_X)
            {
                max_X = item.position.x;
            }
            if (item.position.x < min_X)
            {
                min_X = item.position.x;
            }
        }

        var posRightWall = RightWall.transform.position;
        RightWall.transform.position = new Vector3(max_X + 0.48f, posRightWall.y, posRightWall.z);

        var posUpWall = UpWall.transform.position;
        var sizeUpWall = UpWall.GetComponent<BoxCollider2D>().size;

        //3 => 硬编码 多覆盖一部分防止击穿
        UpWall.GetComponent<BoxCollider2D>().size = new Vector2(Mathf.Abs(max_X - min_X) + 3, sizeUpWall.y);
        UpWall.GetComponent<BoxCollider2D>().offset = new Vector2(UpWall.GetComponent<BoxCollider2D>().offset.x + (UpWall.GetComponent<BoxCollider2D>().size.x - sizeUpWall.x) / 2, UpWall.GetComponent<BoxCollider2D>().offset.y);

        UpWall.transform.position = new Vector3(min_X - 1, posUpWall.y);
    }
}
