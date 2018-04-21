/*
 * 时间：2018年4月21日18:14:28
 * 作者：vszed
 * 功能：食人花
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiranhaFlower : MonoBehaviour
{
    //死亡及旋转动画
    public bool isDied = false;
    bool ownRotateSwitch = false;
    int rotateAngle;

    // Use this for initialization
    void Start()
    {
        if (EnemyEditor.getInstance() != null)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        ownRotate(rotateAngle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDied)
        {
            if (collision.gameObject.GetComponent<Character>() && !collision.gameObject.GetComponent<Character>().isUnmatched)
            {
                GameControler.getInstance().gameOver();
            }
            else if (collision.gameObject.GetComponent<Character>() && collision.gameObject.GetComponent<Character>().isUnmatched)
            {
                this.die(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isDied)
        {
            if (collision.gameObject.GetComponent<Character>() && !collision.gameObject.GetComponent<Character>().isUnmatched)
            {
                GameControler.getInstance().gameOver();
            }
            else if (collision.gameObject.GetComponent<Character>() && collision.gameObject.GetComponent<Character>().isUnmatched)
            {
                this.die(collision.gameObject);
            }
        }
    }

    //无敌角色触碰导致食人花死亡
    public void die(GameObject ob)
    {
        isDied = true;

        StartCoroutine(GameControler.getInstance().ScoreUIControl(200, transform.position, 0.1f));

        AudioControler.getInstance().SE_Emy_Down.Play();

        //更改绘制层级 0 => 4
        GetComponent<SpriteRenderer>().sortingOrder = 4;

        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().gravityScale = 3;
        GetComponent<Rigidbody2D>().mass = 1;

        if (GetComponent<BoxCollider2D>())
            Destroy(GetComponent<BoxCollider2D>());

        var obPos = ob.transform.position;

        if (obPos.x > transform.position.x)
        {
            rotateAngle = 10;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-400, 400), ForceMode2D.Force);
        }
        else
        {
            rotateAngle = -10;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 400), ForceMode2D.Force);
        }

        ownRotateSwitch = true;

        Destroy(gameObject.GetComponentInParent<Image>().gameObject, 5);  //image没实际意义，只供删除父物体
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
