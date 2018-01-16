/*
 * 时间：2018年1月14日22:32:53
 * 作者：vszed
 * 功能：地图编辑器
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapEditor : MonoBehaviour
{
    private static MapEditor instance;

    public static MapEditor getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;

        createMask(16, 32);// row, column);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform mapEditor;
    public Transform mapMaskGrid;

    public GameObject PrefabMask;        //图块遮罩

    public Toggle[] ToggleMapBlock;      //地图图块Toggle
    public GameObject[] PrefabMapBlock;  //地图图块Prefab

    public int row;     //行
    public int column;  //列


    public int getSingleBlockChoice()  //获取单一图块选择“索引值”
    {
        int idx = 0;

        while (idx < ToggleMapBlock.Length)
        {
            if (ToggleMapBlock[idx].isOn)
                break;
            idx++;
        }
        return idx;
    }

    public void drawBlock(Vector3 pos)
    {
        Debug.Log("Pos: " + Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Instantiate(PrefabMapBlock[getSingleBlockChoice()], new Vector3(pos.x, pos.y, 0), new Quaternion(), mapEditor);
    }

    public void createMask(int row, int column)  //创建网格遮罩
    {
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Instantiate(PrefabMask, new Vector3(0.64f * i, 0.64f * j, 0), new Quaternion(), mapMaskGrid);
            }
        }

        //坐标写死，将坐标系移至左下角
        mapMaskGrid.transform.position = new Vector3(-8.574f, -3.683f, 0);
    }
}
