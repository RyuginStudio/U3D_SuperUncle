/*
 * 时间：2018年3月2日18:47:31
 * 作者：vszed
 * 功能：板栗 最初级敌人
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour, IEnemy
{
    private float currentTime;
    private float directionUpdate;
    private float doBeTreadUpdate;

    //自转动画开关
    private bool ownRotateSwitch;

    //动画旋转角度
    private int rotateAngle;

    //0.1秒之前的坐标
    private Vector3 previousPos;

    public float moveSpeed = 1;

    //放大倍数
    public float magnification = 1;

    //是否站在地上
    public bool isGrounded;

    private Rigidbody2D m_rigidbody;

    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
        previousPos = transform.position;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        changeDirection();
        Move();

        RayCollisionDetection();

        ownRotate(rotateAngle);
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
            m_rigidbody.velocity = new Vector2(moveSpeed, m_rigidbody.velocity.y);
            transform.localScale = new Vector3(-magnification, magnification, magnification);
        }
        else
        {
            m_rigidbody.velocity = new Vector2(-moveSpeed, m_rigidbody.velocity.y);
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

    //被踩执行内容
    public void doBeTread()
    {
        if (currentTime - doBeTreadUpdate > 0.2f)
        {
            Debug.Log("doBeTread()");
            doBeTreadUpdate = Time.time;
            die();
        }
    }

    public void die()
    {
        //在地上死
        if (isGrounded)
        {
            AudioControler.getInstance().SE_Emy_Fumu.Play();

            //角色受力
            GameObject.FindWithTag("Player").GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(0, 350));

            Destroy(this);
            GetComponent<Animator>().SetBool("isDieGround", true);
            Destroy(m_rigidbody);
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(gameObject, 1.5f);
        }
        else
        {
            AudioControler.getInstance().SE_Emy_Down.Play();

            Destroy(GetComponent<CircleCollider2D>());

            var charPos = GameObject.FindWithTag("Player").transform.position;

            if (charPos.x > transform.position.x)
            {
                rotateAngle = 10;
                m_rigidbody.AddForce(new Vector2(-400, 400), ForceMode2D.Force);
            }
            else
            {
                rotateAngle = -10;
                m_rigidbody.AddForce(new Vector2(400, 400), ForceMode2D.Force);
            }

            ownRotateSwitch = true;

            Destroy(gameObject, 5);
        }
    }

    //射线检测
    private void RayCollisionDetection()
    {
        if (!ownRotateSwitch)
        {
            var circleCollider = GetComponent<CircleCollider2D>();

            var pos = new Vector2(transform.position.x, transform.position.y - circleCollider.radius);  //检测坐标

            var targetPos = new Vector2(pos.x, pos.y - 0.1f);

            var direction = targetPos - pos;

            var collider = Physics2D.Raycast(pos, direction, 0.1f, 1 << LayerMask.NameToLayer("MapBlock")).collider;

            if (collider != null)
                isGrounded = true;
            else
                isGrounded = false;
        }
    }

    //空中死亡旋转动画
    private void ownRotate(int angle)
    {
        if (ownRotateSwitch)
        {
            transform.Rotate(Vector3.forward, angle);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.collider.gameObject.name == "Character")
    //    {           
    //        GameControler.getInstance().gameOver();
    //        Debug.Log("Goomba");
    //        Debug.Log(collision.collider.transform.position);            
    //    }
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.collider.gameObject.name == "Character")
    //    {
    //        GameControler.getInstance().gameOver();
    //        Debug.Log("GoombaStay");
    //        Debug.Log(collision.collider.transform.position);
    //    }
    //}
}