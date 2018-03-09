/*
 * 时间：2018年3月9日17:53:42
 * 作者：vszed
 * 功能：
 *     1.陆龟 踩后变成壳 可推动 
 *     2.飞龟 踩后翅膀消失变成陆龟
 *     3.一段时间不推动 => 乌龟复活
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tortoise : MonoBehaviour, IEnemy
{
    private float currentTime;
    private float directionUpdate;
    private float doBeTreadUpdate;

    //默认为陆龟
    public bool isFlyTortoise = false;

    public float moveSpeed = 1;

    //放大倍数
    public float magnification = 1;

    //0.1秒之前的坐标
    private Vector3 previousPos;

    //动画状态机
    private Animator m_Animator;

    private Rigidbody2D m_rigidbody;


    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        initWing();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        changeDirection();
        Move();

    }

    private void initWing()
    {
        if (isFlyTortoise)
        {
            m_Animator.SetBool("isFlyTortoise", true);
            var pos = transform.position;
            Instantiate(Resources.Load("Prefab/Enemy/Wing"), new Vector2(pos.x + 0.4f, pos.y + 0.1f), new Quaternion(), transform);
        }
    }

    public enum direction
    {
        left,
        right
    }
    public direction TortoiseDirection;

    //移动
    public void Move()
    {
        if (isFlyTortoise)
        {

        }
        else
        {
            if (TortoiseDirection == direction.right)
            {
                m_rigidbody.velocity = new Vector2(moveSpeed, m_rigidbody.velocity.y);
                transform.localScale = new Vector3(-magnification, magnification, magnification);
            }
            else
            {
                m_rigidbody.velocity = new Vector2(-moveSpeed, m_rigidbody.velocity.y);
                transform.localScale = new Vector3(magnification, magnification, magnification);
            }
        }      
    }

    //转向 => (0.1秒前的坐标与当前坐标相同时视为障碍物)
    public void changeDirection()
    {
        if (currentTime - directionUpdate > 0.1f)
        {
            if (previousPos == transform.position)
                TortoiseDirection = TortoiseDirection == direction.right ? direction.left : direction.right;

            previousPos = transform.position;
            directionUpdate = Time.time;
        }
    }

    public void doBeTread()
    {
        throw new NotImplementedException();
    }
}
