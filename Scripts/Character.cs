using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //动画状态机
    [SerializeField] private Animator m_animator;

    //角色刚体
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    [SerializeField] private bool m_isGrounded;

    //移动速度
    public float MoveSpeed = 10;

    //角色朝向
    public enum Direction
    {
        left,
        right
    }

    public Direction characDirection;

    public void changeStatus(float horizontal)  //状态切换
    {
        m_animator.SetBool("isGrounded", m_isGrounded);

        if (horizontal != 0 && m_isGrounded)
        {
            m_animator.SetBool("isRun", true);
            m_animator.SetBool("isIdle", false);
            m_animator.SetFloat("verticleSpeed", 0);
        }
        else if (horizontal == 0 && m_isGrounded && !Input.GetButton("Horizontal"))
        {
            m_animator.SetBool("isRun", false);
            m_animator.SetBool("isIdle", true);
            m_animator.SetFloat("verticleSpeed", 0);
        }

        if (!m_isGrounded)
        {
            m_animator.SetBool("isIdle", false);
            m_animator.SetBool("isRun", false);
            m_animator.SetFloat("verticleSpeed", m_Rigidbody2D.velocity.y);
        }

    }

    public void changeDirection(float horizontal)
    {
        if (horizontal < 0 && characDirection != Direction.left)
        {
            characDirection = Direction.left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (horizontal > 0 && characDirection != Direction.right)
        {
            characDirection = Direction.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //角色状态初始化
    void characInit()
    {
        characDirection = Direction.right;
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
        CharacterRayCollisionDetection();
    }

    void keyboardControl()
    {
        var horizontal = Input.GetAxis("Horizontal");

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

        float lineSpeed = Input.GetButton("Sprint") ? horizontal * MoveSpeed : horizontal * MoveSpeed / 2;

        //直接操控刚体的线性速度
        m_Rigidbody2D.velocity = new Vector2(lineSpeed, m_Rigidbody2D.velocity.y);

        //角色跳跃
        this.jump();
    }

    //角色的射线碰撞检测
    public void CharacterRayCollisionDetection()
    {
        var CapColl2D = GetComponent<CapsuleCollider2D>();
        var size = CapColl2D.size;
        var pos = CapColl2D.transform.localPosition;

        //获取胶囊碰撞器底部左右侧两端的pos
        float left_x = characDirection == Direction.left ? pos.x - size.x / 2 - CapColl2D.offset.x : pos.x - size.x / 2 + CapColl2D.offset.x;
        float left_y = pos.y;
        float right_x = characDirection == Direction.left ? pos.x + size.x / 2 - CapColl2D.offset.x : pos.x + size.x / 2 + CapColl2D.offset.x;
        float right_y = pos.y;

        var pos_left = new Vector2(left_x, left_y);
        var pos_right = new Vector2(right_x, right_y);

        //通过两点，向下发射线
        var vector1 = new Vector2(pos_left.x, pos_left.y - 0.01f);
        var direction1 = vector1 - pos_left;

        var vector2 = new Vector2(pos_right.x, pos_right.y - 0.01f);
        var direction2 = vector2 - pos_right;

        /*
         * http://blog.csdn.net/qq_33000225/article/details/55225095
         * 1、origin：射线投射的原点
         * 2、direction：射线投射的方向
         * 3、distance：射线的长度
         * 4、layerMask：射线只会投射到layerMask层的碰撞体（注意此int参数的写法：1 << 层数）
         * 5、射线方向为：目标点-起点
         * 用法：Physics2D.Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask);
         */

        var collider_left = Physics2D.Raycast(pos_left, direction1, 0.1f, 1 << LayerMask.NameToLayer("MapBlock")).collider;
        var collider_right = Physics2D.Raycast(pos_right, direction2, 0.1f, 1 << LayerMask.NameToLayer("MapBlock")).collider;

        //Debug.Log("colliderName1: " + collider_left);
        //Debug.Log("colliderName2: " + collider_right);

        if (collider_left == null & collider_right == null)
            m_isGrounded = false;
        else
            m_isGrounded = true;
    }

    public void jump()
    {
        if (m_isGrounded && Input.GetButtonDown("Jump"))
        {
            m_Rigidbody2D.AddForce(new Vector2(0, 800));
            AudioControler.getInstance().SE_Jump.Play();
            m_isGrounded = false;
        }
    }
}