/*
 * 时间：2018年3月27日01:34:45
 * 作者：vszed
 * 功能：挂在到旗子图块下
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGroundedSetStatic();
    }

    //落地后移除刚体
    public void isGroundedSetStatic()
    {
        //在编辑地图中会直接删掉刚体
        if (GetComponent<Rigidbody2D>() && GetComponent<Rigidbody2D>().velocity.y == 0)
            Destroy(GetComponent<Rigidbody2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            Debug.Log("123");
        }
    }
}
