using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //动画状态机
    private Animator m_animator;

    //角色刚体
    private Rigidbody2D m_Rigidbody2D;

    //移动速度
    public float MoveSpeed = 10;

    //角色状态
    public enum Status
    {
        idle,
        move,
        run,
        jump,
    }

    public Status characStatus;

    //角色朝向
    public enum Direction
    {
        left,
        right
    }

    public Direction characDirection;

    public void changeStatus(float horizontal)  //状态切换
    {
        if (horizontal != 0)
        {
            characStatus = Status.move;
            m_animator.SetBool("isRun", true);
            m_animator.SetBool("isIdle", false);
        }
        else if (!Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            characStatus = Status.idle;
            m_animator.SetBool("isIdle", true);
            m_animator.SetBool("isRun", false);
        }
    }

    public void changeDirection(float horizontal)
    {
        if (horizontal < 0 && characDirection != Direction.left)
        {
            characDirection = Direction.left;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        if (horizontal > 0 && characDirection != Direction.right)
        {
            characDirection = Direction.right;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    //角色状态初始化
    void characInit()
    {
        characDirection = Direction.right;
        characStatus = Status.idle;
    }

    private void Awake()
    {
        //设置相关引用
        m_animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start()
    {
        characInit();
    }

    // Update is called once per frame
    void Update()
    {
        keyboardControl();
    }

    void keyboardControl()
    {
        var horizontal = Input.GetAxis("Horizontal");
        //var verticle = Input.GetAxis("Vertical");

        characterMove(horizontal);
    }

    //角色移动函数
    public void characterMove(float horizontal)
    {
        //切换动画状态
        changeStatus(horizontal);

        //切换角色朝向
        changeDirection(horizontal);

        //GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * MoveSpeed, 0));   // --last version

        //直接操控刚体的线性速度
        m_Rigidbody2D.velocity = new Vector2(horizontal * MoveSpeed / 2, m_Rigidbody2D.velocity.y);
    }

}