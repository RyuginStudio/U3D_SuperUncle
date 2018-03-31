/*
 * 时间：2018年3月31日01:23:37
 * 作者：vszed
 * 功能：实现一个scrollView切换显示两种：图块和敌人
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFunction : MonoBehaviour
{
    public GameObject BlockGroup;
    public GameObject EnemyGroup;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        displayMapOrEnemy();
    }

    void displayMapOrEnemy()
    {
        if (GetComponent<Toggle>().isOn)
        {
            BlockGroup.SetActive(false);
            EnemyGroup.SetActive(true);
        }
        else
        {
            BlockGroup.SetActive(true);
            EnemyGroup.SetActive(false);
        }
    }
}
