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

    //角色是否无敌
    public bool isUnmatched = false;

    //单例
    private static Character instance;
    public static Character getInstance()
    {
        return instance;
    }

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
        instance = this;

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
        if (!m_animator.GetBool("isDie"))
        {
            keyboardControl();
            CharacterRayCollisionDetection();
            fallDownDie();
        }
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

        //角色脚步音效
        playFootPrintSE(horizontal);

        //角色下落音效
        playFallDownSE();
    }

    //角色的射线碰撞检测(头部和脚部)
    public void CharacterRayCollisionDetection()
    {
        #region JudgeFeet  //检测脚部碰撞

        //=================脚部与图块 Begin=================//

        var CapColl2D = GetComponent<CapsuleCollider2D>();
        var size = CapColl2D.size;
        var pos = CapColl2D.transform.localPosition;

        //Debug.Log("size: " + size);
        //Debug.Log("pos: " + pos);

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

        if (collider_left == null && collider_right == null)
        {
            m_isGrounded = false;
            canPlayFallDownSE = true;
        }
        else
        {
            m_isGrounded = true;
        }

        //=================脚部与图块 End=================//


        //=================脚部与敌人 Begin=================//

        var collider_left_enemy = Physics2D.Raycast(pos_left, direction1, 0.1f, 1 << LayerMask.NameToLayer("EnemyLayer")).collider;
        var collider_right_enemy = Physics2D.Raycast(pos_right, direction2, 0.1f, 1 << LayerMask.NameToLayer("EnemyLayer")).collider;

        if (collider_left_enemy != null)
        {
            switch (collider_left_enemy.gameObject.tag)
            {
                case "Goomba":
                    {
                        collider_left_enemy.gameObject.GetComponent<Goomba>().doBeTread(gameObject);
                        break;
                    }
                case "Tortoise":
                    {
                        collider_left_enemy.gameObject.GetComponent<Tortoise>().doBeTread(gameObject);
                        break;
                    }
            }
        }

        if (collider_right_enemy != null)
        {
            switch (collider_right_enemy.gameObject.tag)
            {
                case "Goomba":
                    {
                        collider_right_enemy.gameObject.GetComponent<Goomba>().doBeTread(gameObject);
                        break;
                    }
                case "Tortoise":
                    {
                        collider_right_enemy.gameObject.GetComponent<Tortoise>().doBeTread(gameObject);
                        break;
                    }
            }
        }

        //=================脚部与敌人 End=================//

        #endregion

        #region JudgeHead  //检测头部碰撞

        //=================头部与图块 Begin=================//

        float left_head_x = characDirection == Direction.left ? pos.x - size.x / 2 - CapColl2D.offset.x : pos.x - size.x / 2 + CapColl2D.offset.x;
        float left_head_y = pos.y + size.y;
        float right_head_x = characDirection == Direction.left ? pos.x + size.x / 2 - CapColl2D.offset.x : pos.x + size.x / 2 + CapColl2D.offset.x;
        float right_head_y = pos.y + size.y;

        var pos_head_left = new Vector2(left_head_x, left_head_y);
        var pos_head_right = new Vector2(right_head_x, right_head_y);

        //通过两点，向上发射线
        var vectorHeadLeft = new Vector2(pos_head_left.x, pos_head_left.y + 0.01f);
        var directionHeadLeft = vectorHeadLeft - pos_head_left;

        var vectorHeadRight = new Vector2(pos_head_right.x, pos_head_right.y + 0.01f);
        var directionHeadRight = vectorHeadRight - pos_head_right;

        var collider_Head_Left = Physics2D.Raycast(pos_head_left, directionHeadLeft, 0.05f, 1 << LayerMask.NameToLayer("MapBlock")).collider;
        var collider_Head_Right = Physics2D.Raycast(pos_head_right, directionHeadRight, 0.05f, 1 << LayerMask.NameToLayer("MapBlock")).collider;

        //Debug.Log("collider1: " + collider_Head_Left);
        //Debug.Log("collider2: " + collider_Head_Right);
        //Debug.DrawRay(pos_head_left, directionHeadLeft, Color.red, 0.05f);
        //Debug.DrawRay(pos_head_right, directionHeadRight, Color.red, 0.05f);

        //需要保证是向“上”跳的状态（限制线速度的Y）
        if (collider_Head_Left != null && m_Rigidbody2D.velocity.y > -1)
        {
            collider_Head_Left.GetComponent<MapBlock>().BlockCollision();
        }
        if (collider_Head_Right != null && m_Rigidbody2D.velocity.y > -1)
        {
            collider_Head_Right.GetComponent<MapBlock>().BlockCollision();
        }

        //=================头部与图块 End=================//

        //=================头部与敌人 Begin=================//

        var collider_Head_Left_enemy = Physics2D.Raycast(pos_head_left, directionHeadLeft, 0.05f, 1 << LayerMask.NameToLayer("EnemyLayer")).collider;
        var collider_Head_Right_enemy = Physics2D.Raycast(pos_head_right, directionHeadRight, 0.05f, 1 << LayerMask.NameToLayer("EnemyLayer")).collider;

        if ((collider_Head_Left_enemy != null || collider_Head_Right_enemy != null) && !isUnmatched)
        {
            GameControler.getInstance().gameOver();
        }

        //=================头部与敌人 End=================//

        #endregion

        #region JudgeBody  //检测身体碰撞

        /*
         * 于胶囊碰撞器上添加了boxCollider(isTrigger)触发器 => 以免被卡住
         * 用这个碰撞器OnTriggerEnter2D与Stay2D代替身体部位的射线碰撞处理
         */

        #endregion
    }

    public void jump()
    {
        if (m_isGrounded && Input.GetButtonDown("Jump"))
        {
            m_Rigidbody2D.AddForce(new Vector2(0, 730));
            AudioControler.getInstance().SE_Jump.Play();
        }
    }

    public void fallDownDie()
    {
        if (transform.position.y < -15)
        {
            GameControler.getInstance().gameOver();
        }
    }

    public void playFootPrintSE(float horizontal)
    {
        if (m_isGrounded && horizontal != 0
            && !AudioControler.getInstance().SE_FootNote.isPlaying
            && !AudioControler.getInstance().SE_FootNote_L.isPlaying)
        {
            AudioControler.getInstance().SE_FootNote.Play();

            if (Input.GetButton("Sprint"))
                AudioControler.getInstance().SE_FootNote_L.PlayDelayed(0.1f);
            else
                AudioControler.getInstance().SE_FootNote_L.PlayDelayed(0.165f);
        }
    }

    //能否播放下落落地声音
    private bool canPlayFallDownSE = true;

    public void playFallDownSE()
    {
        if (m_isGrounded && canPlayFallDownSE && !AudioControler.getInstance().SE_FallDown.isPlaying)
        {
            AudioControler.getInstance().SE_FallDown.Play();
            canPlayFallDownSE = false;
        }
    }

    public void characterDie()
    {
        //SE
        AudioControler.getInstance().SE_Die1.Play();
        AudioControler.getInstance().SE_Die2.PlayDelayed(0.5f);
        AudioControler.getInstance().SE_OhNo.PlayDelayed(0.5f);

        //死亡时屏幕抖动  弃用
        //GameObject.FindWithTag("MainCamera").GetComponent<ShakeCamera>().isshakeCamera = true;

        m_animator.SetBool("isDie", true);
        Destroy(GetComponent<CapsuleCollider2D>());
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        GameObject.Find("small_sprite_stand").transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isUnmatched)
        {
            switch (collision.gameObject.tag)
            {
                case "Goomba":
                    GameControler.getInstance().gameOver();
                    break;
                case "Tortoise":
                    {
                        if (collision.GetComponent<Tortoise>().TortoiseStatus == Tortoise.Status.isShellStatic)
                        {
                            collision.GetComponent<Tortoise>().pushOrTreadShell(gameObject);
                        }
                        else
                            GameControler.getInstance().gameOver();
                        break;
                    }
            }
        }
        else
        {
            switch (collision.gameObject.tag)
            {
                case "Goomba":
                    collision.gameObject.GetComponent<Goomba>().doBeTread(gameObject);
                    break;
                case "Tortoise":
                    collision.gameObject.GetComponent<Tortoise>().doBeTread(gameObject);
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isUnmatched)
        {
            switch (collision.gameObject.tag)
            {
                case "Goomba":
                    GameControler.getInstance().gameOver();
                    break;
                case "Tortoise":
                    {
                        if (collision.GetComponent<Tortoise>().TortoiseStatus == Tortoise.Status.isShellStatic)
                        {
                            collision.GetComponent<Tortoise>().pushOrTreadShell(gameObject);
                        }
                        else if(transform.position.x < collision.transform.position.x && collision.GetComponent<Tortoise>().TortoiseDirection == Tortoise.direction.left 
                            || transform.position.x > collision.transform.position.x && collision.GetComponent<Tortoise>().TortoiseDirection == Tortoise.direction.right)
                        {   //需要对龟壳的direction进行判断
                            GameControler.getInstance().gameOver();
                        }
                            
                        break;
                    }
            }
        }
        else
        {
            switch (collision.gameObject.tag)
            {
                case "Goomba":
                    collision.gameObject.GetComponent<Goomba>().doBeTread(gameObject);
                    break;
                case "Tortoise":
                    collision.gameObject.GetComponent<Tortoise>().doBeTread(gameObject);
                    break;
            }
        }
    }

}
