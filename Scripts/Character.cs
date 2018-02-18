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
        jump,
    }

    public Status characStatus;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        keyboardControl();
        rotateAnim(Input.GetAxis("Horizontal"));
    }

    void keyboardControl()
    {
        var horizontal = Input.GetAxis("Horizontal");
        //var verticle = Input.GetAxis("Vertical");

        //Debug.Log(horizontal);
        //Debug.Log(verticle);

        GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * MoveSpeed, 0));
    }

    void rotateAnim(float horizontal)  //角色转向动画
    {
        if(horizontal != 0)
        {
            
        }
    }
}
