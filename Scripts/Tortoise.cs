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
    public bool isDied = false;

    private float currentTime;
    private float doBeTreadUpdate;

    //xx秒后爬出龟壳定时器
    private float beginCrawlUpdate;

    public float moveSpeed = 2;

    //触碰周身
    public bool isGrounded;
    public bool isHeaded;
    public bool isLeft;
    public bool isRight;

    //放大倍数
    public float magnification;

    //动画状态机
    private Animator m_Animator;

    private Rigidbody2D m_rigidbody;

    //飞龟上下目标点
    private Vector3 UpTargetPos;
    private Vector3 DownTargetPos;

    //死亡自转开关
    bool ownRotateSwitch = false;
    int rotateAngle;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        if (TortoiseStatus == Status.isFly)
            TortoiseDirection = direction.rise;
        else if (TortoiseStatus == Status.isOnFoot)
            TortoiseDirection = direction.right | direction.left;
    }

    // Use this for initialization
    void Start()
    {
        var pos = transform.position;
        UpTargetPos = new Vector3(pos.x, pos.y + 10, pos.z);
        DownTargetPos = new Vector3(pos.x, pos.y - 10, pos.z);

        wingControl();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;

        if (!isDied)
        {
            animatorControler();

            Move();

            changeDirection();

            RayCollisionDetection();

            changeCollider();

            tortoiseCrawl();

        }

        //死亡自转
        ownRotate(rotateAngle);
    }

    public enum direction
    {
        left,
        right,
        rise,
        fall
    }
    public direction TortoiseDirection;

    public enum Status
    {
        isFly,
        isOnFoot,
        isShellStatic,
        isShellMove,
        isCrawl
    }
    public Status TortoiseStatus;

    //动画状态机
    private void animatorControler()
    {
        switch (TortoiseStatus)
        {
            case Status.isFly:
                {
                    m_Animator.SetBool("isFly", true);
                    m_Animator.SetBool("isCrawl", false);
                    m_Animator.SetBool("isShellMove", false);
                    m_Animator.SetBool("isShellStatic", false);
                    break;
                }
            case Status.isOnFoot:
                {
                    m_Animator.SetBool("isFly", false);
                    m_Animator.SetBool("isCrawl", false);
                    m_Animator.SetBool("isShellMove", false);
                    m_Animator.SetBool("isShellStatic", false);
                    break;
                }

            case Status.isShellMove:
                {
                    m_Animator.SetBool("isFly", false);
                    m_Animator.SetBool("isCrawl", false);
                    m_Animator.SetBool("isShellMove", true);
                    m_Animator.SetBool("isShellStatic", false);
                    break;
                }
            case Status.isShellStatic:
                {
                    m_Animator.SetBool("isFly", false);
                    m_Animator.SetBool("isCrawl", false);
                    m_Animator.SetBool("isShellMove", false);
                    m_Animator.SetBool("isShellStatic", true);
                    break;
                }
            case Status.isCrawl:
                {
                    m_Animator.SetBool("isFly", false);
                    m_Animator.SetBool("isCrawl", true);
                    m_Animator.SetBool("isShellMove", false);
                    m_Animator.SetBool("isShellStatic", false);
                    break;
                }
        }
    }

    //移动
    public void Move()
    {

        if (TortoiseStatus == Status.isFly)
        {
            m_rigidbody.gravityScale = 0;
            m_rigidbody.mass = 0;
            m_rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            if (TortoiseDirection == direction.rise)
            {
                transform.position = Vector2.MoveTowards(transform.position, UpTargetPos, 0.02f);
                transform.localScale = new Vector3(magnification, magnification, magnification);
                if (transform.position == UpTargetPos)
                {
                    TortoiseDirection = direction.fall;
                }
            }
            else if (TortoiseDirection == direction.fall)
            {
                transform.position = Vector2.MoveTowards(transform.position, DownTargetPos, 0.02f);
                transform.localScale = new Vector3(magnification, magnification, magnification);
                if (transform.position == DownTargetPos)
                {
                    TortoiseDirection = direction.rise;
                }
            }
        }
        else if (TortoiseStatus == Status.isOnFoot)
        {
            m_rigidbody.gravityScale = 3;
            m_rigidbody.mass = 1;
            m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (TortoiseDirection == direction.right)
            {
                m_rigidbody.velocity = new Vector2(moveSpeed, m_rigidbody.velocity.y);
                transform.localScale = new Vector3(-magnification, magnification, magnification);
            }
            else if (TortoiseDirection == direction.left)
            {
                m_rigidbody.velocity = new Vector2(-moveSpeed, m_rigidbody.velocity.y);
                transform.localScale = new Vector3(magnification, magnification, magnification);
            }
        }
        else if (TortoiseStatus == Status.isShellMove)
        {
            m_rigidbody.gravityScale = 30;
            m_rigidbody.mass = 1;
            m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (TortoiseDirection == direction.left)
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 1.1f * moveSpeed, m_rigidbody.velocity.y);
            else
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x + 1.1f * moveSpeed, m_rigidbody.velocity.y);
        }
        else if (TortoiseStatus == Status.isCrawl)
        {
            if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                TortoiseStatus = Status.isOnFoot;
            }
        }

    }

    //转向
    public void changeDirection()
    {
        if (TortoiseStatus == Status.isFly)
        {
            if (isGrounded)
            {
                TortoiseDirection = direction.rise;
            }
            if (isHeaded)
            {
                TortoiseDirection = direction.fall;
            }
        }
        else if (TortoiseStatus == Status.isShellMove || TortoiseStatus == Status.isOnFoot)
        {
            if (isLeft)
            {
                TortoiseDirection = direction.right;
            }
            if (isRight)
            {
                TortoiseDirection = direction.left;
            }
        }
    }

    //翅膀控制
    public void wingControl()
    {
        if (TortoiseStatus == Status.isFly)
        {
            var pos = transform.position;
            var wingPos = transform.localScale.x > 0 ? new Vector2(pos.x + 0.4f, pos.y + 0.1f) : new Vector2(pos.x - 0.4f, pos.y + 0.1f);
            Instantiate(Resources.Load("Prefab/Enemy/Wing"), wingPos, new Quaternion(), transform);
        }
        else
        {
            if (GetComponentInChildren<Wing>())
            {
                GetComponentInChildren<Wing>().ownRotateSwitch = true;
                GetComponentInChildren<Wing>().gameObject.AddComponent<Rigidbody2D>();
                GetComponentInChildren<Wing>().gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;

                Destroy(GetComponentInChildren<Wing>().gameObject, 3);
            }
        }
    }

    public void RayCollisionDetection()
    {
        #region isGrounded

        var CapColl2D = GetComponent<CapsuleCollider2D>();
        var size = CapColl2D.size;
        var pos = CapColl2D.transform.localPosition;

        //获取胶囊碰撞器底部左右侧两端的pos
        float left_x = transform.localScale.x > 0 ? pos.x - size.x / 2 + CapColl2D.offset.x : pos.x - size.x / 2 - CapColl2D.offset.x;
        float left_y = transform.position.y - size.y / 2;
        float right_x = transform.localScale.x > 0 ? pos.x + size.x / 2 + CapColl2D.offset.x : pos.x + size.x / 2 - CapColl2D.offset.x;
        float right_y = transform.position.y - size.y / 2;

        var pos_left = new Vector2(left_x, left_y);
        var pos_right = new Vector2(right_x, right_y);

        //通过两点，向下发射线
        var vector1 = new Vector2(pos_left.x, pos_left.y - 0.01f);
        var direction1 = vector1 - pos_left;

        var vector2 = new Vector2(pos_right.x, pos_right.y - 0.01f);
        var direction2 = vector2 - pos_right;

        var collider_left = Physics2D.Raycast(pos_left, direction1, 0.3f, 1 << LayerMask.NameToLayer("MapBlock")).collider;
        var collider_right = Physics2D.Raycast(pos_right, direction2, 0.3f, 1 << LayerMask.NameToLayer("MapBlock")).collider;

        //Debug.Log("colliderName1: " + collider_left);
        //Debug.Log("colliderName2: " + collider_right);
        //Debug.DrawRay(pos_left, direction1, Color.red, 0.05f);
        //Debug.DrawRay(pos_right, direction2, Color.red, 0.05f);

        if (collider_left == null & collider_right == null)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
        }

        #endregion

        #region isHeaded

        float left_head_x = transform.localScale.x > 0 ? pos.x - size.x / 2 + CapColl2D.offset.x : pos.x - size.x / 2 - CapColl2D.offset.x;
        float left_head_y = pos.y + size.y / 2 + CapColl2D.offset.y;
        float right_head_x = transform.localScale.x > 0 ? pos.x + size.x / 2 + CapColl2D.offset.x : pos.x + size.x / 2 - CapColl2D.offset.x;
        float right_head_y = pos.y + size.y / 2 + CapColl2D.offset.y;

        var pos_head_left = new Vector2(left_head_x, left_head_y);
        var pos_head_right = new Vector2(right_head_x, right_head_y);

        //通过两点，向上发射线
        var vectorHeadLeft = new Vector2(pos_head_left.x, pos_head_left.y + 0.01f);
        var directionHeadLeft = vectorHeadLeft - pos_head_left;

        var vectorHeadRight = new Vector2(pos_head_right.x, pos_head_right.y + 0.01f);
        var directionHeadRight = vectorHeadRight - pos_head_right;

        var collider_Head_Left = Physics2D.Raycast(pos_head_left, directionHeadLeft, 0.3f, 1 << LayerMask.NameToLayer("MapBlock")).collider;
        var collider_Head_Right = Physics2D.Raycast(pos_head_right, directionHeadRight, 0.3f, 1 << LayerMask.NameToLayer("MapBlock")).collider;

        //Debug.Log("collider1: " + collider_Head_Left);
        //Debug.Log("collider2: " + collider_Head_Right);
        //Debug.DrawRay(pos_head_left, directionHeadLeft, Color.red, 0.05f);
        //Debug.DrawRay(pos_head_right, directionHeadRight, Color.red, 0.05f);

        if (collider_Head_Left == null & collider_Head_Right == null)
        {
            isHeaded = false;
        }
        else
        {
            isHeaded = true;
        }

        #endregion

        #region bodyLeftRight

        var circlePos = GetComponent<CircleCollider2D>().transform.position;
        var posBodyLeft = new Vector2(circlePos.x - GetComponent<CircleCollider2D>().radius, circlePos.y);
        var targetBodyLeft = new Vector2(posBodyLeft.x - 1, posBodyLeft.y);
        var DirectionBodyLeft = targetBodyLeft - posBodyLeft;
        var posBodyRight = new Vector2(circlePos.x + GetComponent<CircleCollider2D>().radius, circlePos.y);
        var targetBodyRight = new Vector2(posBodyRight.x + 1, posBodyRight.y);
        var DirectionBodyRight = targetBodyRight - posBodyRight;

        //Debug.DrawRay(posBodyLeft, DirectionBodyLeft, Color.red, 0.2f);
        //Debug.DrawRay(posBodyRight, DirectionBodyRight, Color.red, 0.2f);

        var rayDistance = TortoiseStatus == Status.isShellMove ? 0.1f : 0.2f;
        var colliderBodyLeft = Physics2D.Raycast(posBodyLeft, DirectionBodyLeft, rayDistance, 1 << LayerMask.NameToLayer("MapBlock")).collider;
        var colliderBodyRight = Physics2D.Raycast(posBodyRight, DirectionBodyRight, rayDistance, 1 << LayerMask.NameToLayer("MapBlock")).collider;

        if (colliderBodyLeft)
        {
            if (!AudioControler.getInstance().SE_Hit_Block.isPlaying && TortoiseStatus == Status.isShellMove)
                AudioControler.getInstance().SE_Hit_Block.Play();
            isLeft = true;
        }
        else
            isLeft = false;

        if (colliderBodyRight)
        {
            if (!AudioControler.getInstance().SE_Hit_Block.isPlaying && TortoiseStatus == Status.isShellMove)
                AudioControler.getInstance().SE_Hit_Block.Play();

            isRight = true;
        }
        else
            isRight = false;

        #endregion

    }

    public void doBeTread(GameObject player)
    {
        if (currentTime - doBeTreadUpdate > 0.2f)
        {
            StartCoroutine(GameControler.getInstance().ScoreUIControl(100, transform.localPosition, 0.1f));

            //Debug.Log("doBeTread()");
            doBeTreadUpdate = Time.time;

            if (TortoiseStatus == Status.isFly)
            {
                AudioControler.getInstance().SE_Emy_Down.Play();
            }
            else
            {
                AudioControler.getInstance().SE_Emy_Fumu.Play();
            }


            //角色受力
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0);  //清空player竖直线速度
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, player.GetComponent<Rigidbody2D>().velocity.y + 10);

            switch (TortoiseStatus)
            {
                case Status.isFly:
                    {
                        TortoiseStatus = Status.isOnFoot;
                        wingControl();

                        if (player.transform.position.x > transform.position.x)
                            TortoiseDirection = direction.left;
                        else
                            TortoiseDirection = direction.right;

                        break;
                    }

                case Status.isOnFoot:
                    {
                        //龟壳爬行计时
                        beginCrawlUpdate = Time.time;

                        TortoiseStatus = Status.isShellStatic;
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
                        break;
                    }

                case Status.isShellStatic:
                    {
                        TortoiseStatus = Status.isShellMove;
                        pushOrTreadShell(player);
                        break;
                    }

                case Status.isShellMove:
                    {
                        //龟壳爬行计时
                        beginCrawlUpdate = Time.time;

                        TortoiseStatus = Status.isShellStatic;
                        break;
                    }

                case Status.isCrawl:
                    {
                        //龟壳爬行计时
                        beginCrawlUpdate = Time.time;

                        TortoiseStatus = Status.isShellStatic;
                        break;
                    }
                    
                default:
                    break;
            }

        }
    }

    //乌龟不同形态切换不同碰撞体
    public void changeCollider()
    {
        if (TortoiseStatus == Status.isFly || TortoiseStatus == Status.isOnFoot)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (TortoiseStatus == Status.isCrawl)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (TortoiseStatus == Status.isShellMove || TortoiseStatus == Status.isShellStatic)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    //推壳|踩壳
    public void pushOrTreadShell(GameObject player)
    {
        if (player.transform.position.x > transform.position.x)
        {
            TortoiseDirection = direction.left;
            TortoiseStatus = Status.isShellMove;
        }
        else
        {
            TortoiseDirection = direction.right;
            TortoiseStatus = Status.isShellMove;
        }


    }

    //乌龟爬行逻辑
    public void tortoiseCrawl()
    {
        if(currentTime - beginCrawlUpdate > 3 && TortoiseStatus == Status.isShellStatic)
        {
            TortoiseStatus = Status.isCrawl;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (TortoiseStatus == Status.isShellMove)
        {
            switch (collision.collider.tag)
            {
                case "Goomba":
                    {
                        collision.collider.GetComponent<Goomba>().die(gameObject);
                        break;
                    }
                case "Tortoise":
                    {
                        collision.collider.GetComponent<Tortoise>().die(collision.collider.gameObject);
                        break;
                    }
                default:
                    break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (TortoiseStatus == Status.isShellMove)
        {
            switch (collision.collider.tag)
            {
                case "Goomba":
                    {
                        collision.collider.GetComponent<Goomba>().die(gameObject);
                        break;
                    }
                case "Tortoise":
                    {
                        collision.collider.GetComponent<Tortoise>().die(collision.collider.gameObject);
                        break;
                    }
                default:
                    break;
            }
        }
    }

    //只有两种情况会死：任何状态下 => 1.被炮弹击中 2.被别的龟壳击中
    public void die(GameObject ob)
    {
        isDied = true;

        StartCoroutine(GameControler.getInstance().ScoreUIControl(200, transform.localPosition, 0.1f));

        AudioControler.getInstance().SE_Emy_Down.Play();

        GetComponent<Rigidbody2D>().gravityScale = 3;
        GetComponent<Rigidbody2D>().mass = 1;

        if (GetComponent<CircleCollider2D>())
            Destroy(GetComponent<CircleCollider2D>());
        if (GetComponent<BoxCollider2D>())
            Destroy(GetComponent<BoxCollider2D>());
        if (GetComponent<CapsuleCollider2D>())
            Destroy(GetComponent<CapsuleCollider2D>());

        var obPos = ob.transform.position;

        if (obPos.x > transform.position.x)
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

    //空中死亡旋转动画
    private void ownRotate(int angle)
    {
        if (ownRotateSwitch)
        {
            transform.Rotate(Vector3.forward, angle);
        }
    }
    
}
