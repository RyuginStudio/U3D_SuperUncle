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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        drawMap();
    }

    public Transform mapEditor;

    public Toggle[] ToggleMapBlock;      //地图图块Toggle
    public GameObject[] PrefabMapBlock;  //地图图块Prefab


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

    public void drawMap()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log(getSingleBlockChoice());
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //鼠标的屏幕坐标转成世界坐标
            Instantiate(PrefabMapBlock[getSingleBlockChoice()], new Vector3(pos.x, pos.y, 0), new Quaternion(), mapEditor);
        }
    }
}
