/*
 * 时间：2018年3月31日01:46:16
 * 作者：vszed
 * 功能：敌人编辑器
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEditor : MonoBehaviour
{
    //存放所以敌人预制体
    public GameObject[] enemyPrefabs;

    //UI界面显示供选择的敌人图片
    public Sprite[] enemyPics;

    public static EnemyEditor getInstance()
    {
        return instance;
    }
    private static EnemyEditor instance;

    // Use this for initialization
    void Start()
    {
        instance = this;
        initToggles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //用于实例化的toggle预制体
    public GameObject togglePrefab;
    //实例化的位置
    public Transform trs_EnemyGroup;
    //选择的Toggle的种类
    public string toggle_type;

    void initToggles()
    {
        for (int i = 0; i < enemyPics.Length; i++)
        {
            var tempToggle = Instantiate(togglePrefab, trs_EnemyGroup);
            tempToggle.name = enemyPrefabs[i].name;
            tempToggle.GetComponentInChildren<Image>().sprite = enemyPics[i];
            tempToggle.GetComponent<Toggle>().group = trs_EnemyGroup.gameObject.GetComponent<ToggleGroup>();
            if (i == 0)
                tempToggle.GetComponent<Toggle>().isOn = true;
        }
    }

    //存放敌人的transform
    public Transform trans_enemy;

    //绘制敌人
    public void drawEnemy(Vector3 pos, string type)
    {
        if (type == "")
            type = "Goomba";
        var prefabBlock = Instantiate(Resources.Load("Prefab/Enemy/" + type), new Vector3(pos.x, pos.y, 0), new Quaternion(), trans_enemy);
        prefabBlock.name = type;
    }
}
