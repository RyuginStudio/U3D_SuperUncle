/*
 * 时间：2018年3月2日18:47:31
 * 作者：vszed
 * 功能：板栗 最初级敌人
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    private float currentTime;
    private float directionUpdate;

    //0.1秒之前的坐标
    private Vector3 previousPos;

    public float moveSpeed = 1;

    //放大倍数
    public float magnification = 1;

    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
        previousPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        changeDirection();
        Move();
    }

    public enum direction
    {
        left,
        right
    }
    public direction GoombaDirection;

    //移动
    public void Move()
    {
        if (GoombaDirection == direction.right)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(-magnification, magnification, magnification);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            transform.localScale = new Vector3(magnification, magnification, magnification);
        }

    }

    //转向 => (0.1秒前的坐标与当前坐标相同时视为障碍物)
    public void changeDirection()
    {
        if (currentTime - directionUpdate > 0.1f)
        {
            if (previousPos == transform.position)
                GoombaDirection = GoombaDirection == direction.right ? direction.left : direction.right;

            previousPos = transform.position;
            directionUpdate = Time.time;
        }
    }

    public void die()
    {
        //在地上死
        if (GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            Destroy(this);
            GetComponent<Animator>().SetBool("isDie", true);
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 1);
        }
        else
        {
            //TODO:在空中死的逻辑
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Character")
        {
            die();
        }
    }
}
