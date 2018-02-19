using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //移动速度
    public float MoveSpeed = 8;
    //正常转弯速度
    public float RotateSpeed = 10;

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
            Animator anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            anim.SetBool("isRun", true);
            anim.SetBool("isIdle", false);
        }
        else if (!Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            characStatus = Status.idle;
            Animator anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            anim.SetBool("isIdle", true);
            anim.SetBool("isRun", false);
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

        GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * MoveSpeed, 0));

        //切换动画状态
        changeStatus(horizontal);

        //切换角色朝向
        changeDirection(horizontal);
    }

}