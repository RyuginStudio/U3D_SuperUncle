/*
 * 时间：2018年3月23日00:35:16
 * 作者：vszed
 * 功能：提示牌（必须实例化到Cavans）
 * 从上方显示下落到屏幕某个坐标
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeCard : MonoBehaviour
{
    public Text Title;
    public Text Content;
    public string str_title;
    public string str_Content;
    public int FallSpeed = 200;
    public Vector3 fallToPos = new Vector3(-13, 37, 0);

    // Use this for initialization
    void Start()
    {
        insertTitleContent();
        //newRow();
    }

    // Update is called once per frame
    void Update()
    {
        fallDownDisplay();
    }

    //换行
    private void newRow()
    {
        //https://blog.csdn.net/zjw1349547081/article/details/53390609
        Content.text = Content.text.Replace("\\n", "\n");
    }

    private void insertTitleContent()
    {
        Title.text = str_title;
        Content.text = str_Content;
    }

    private void fallDownDisplay()
    {
        var currentPos = transform.localPosition;
        transform.localPosition = Vector3.MoveTowards(currentPos, fallToPos, FallSpeed * Time.deltaTime);
    }
}
