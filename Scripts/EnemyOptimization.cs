/*
 * 时间：2018年3月24日13:20:02
 * 作者：vszed
 * 功能：优化与角色距离过远的敌人
 * 用法：挂载到敌人父物体下
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOptimization : MonoBehaviour
{
    //优化距离阈值
    [SerializeField] private float maxDistance = 20;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        doOptimization();
    }

    private void doOptimization()
    {
        foreach (var item in GetComponentsInChildren<Transform>())
        {
            Vector2 charaPos;
            switch (item.tag)
            {
                case "Goomba":
                    {
                        if (!item.GetComponent<Goomba>().isDied)
                        {
                            charaPos = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject.transform.localPosition;

                            //Debug.Log(Vector2.Distance(item.localPosition, charaPos));

                            if (Vector2.Distance(item.localPosition, charaPos) > maxDistance)
                            {
                                item.GetComponent<Goomba>().enabled = false;
                                item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                            }
                            else
                            {
                                item.gameObject.GetComponent<Goomba>().enabled = true;
                                item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                            }

                            setFallDownDie(item.gameObject);
                        }
                        break;
                    }

                case "Tortoise":
                    {
                        if (!item.GetComponent<Tortoise>().isDied)
                        {
                            charaPos = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject.transform.localPosition;

                            //Debug.Log(Vector2.Distance(item.localPosition, charaPos));

                            if (Vector2.Distance(item.localPosition, charaPos) > maxDistance)
                            {
                                item.gameObject.GetComponent<Tortoise>().enabled = false;
                                item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                            }
                            else
                            {
                                item.gameObject.GetComponent<Tortoise>().enabled = true;
                                item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                            }

                            setFallDownDie(item.gameObject);
                        }
                        break;
                    }

                default:
                    break;
            }
        }
    }

    private void setFallDownDie(GameObject item)
    {
        //敌人掉落后设置为死亡销毁
        if (item.transform.position.y < -15)
        {
            GameObject.Destroy(item);
        }
    }
}
