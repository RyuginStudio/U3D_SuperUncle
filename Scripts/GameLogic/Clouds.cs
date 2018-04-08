/*
 * 时间：2018年3月24日23:48:28
 * 作者：vszed
 * 功能：控制大小云朵位移
 * 注意：大云朵距离近 => 移速更快
 * 用法：挂载到大、小云朵预制体下
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public enum state
    {
        big,
        small
    }

    public state cloudState;

    [SerializeField] private float bigSpeed = 0.35f;
    [SerializeField] private float smallSpeed = 0.1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cloudMove();
    }

    private void cloudMove()
    {
        var currtPos = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-12, currtPos.y), (cloudState == state.big ? bigSpeed : smallSpeed) * Time.deltaTime);
        if (transform.position.x <= -12)
            transform.position = new Vector2(11.96f, currtPos.y);
    }

}
