/*
 * 时间：2018年4月6日13:04:33
 * 作者：vszed
 * 功能：联网排行榜
 * 参考：https://blog.csdn.net/icrazyaaa/article/details/49909937
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankList : MonoBehaviour
{
    public GameObject RotatePic;
    public GameObject ErrorLabel;

    // Use this for initialization
    void Start()
    {
        string url = "47.75.2.153";//"www.vszed.com";
        StartCoroutine(load(url));
    }

    // Update is called once per frame
    void Update()
    {
        rotateLoadingAnim();
    }

    public IEnumerator load(string url)
    {
        //发送请求：U3D的WWW基于Http通讯 => 适合短连接
        WWW httpGet = new WWW(url);

        //接收请求
        yield return httpGet;

        Debug.Log(httpGet.error);

        //判断请求是否有错误：空为没错误
        if (string.IsNullOrEmpty(httpGet.error))
        {
            Debug.Log(httpGet.text);
        }
        else
        {
            //网络异常
            ErrorLabel.SetActive(true);
        }



    }

    void rotateLoadingAnim()
    {
        RotatePic.transform.Rotate(new Vector3(0, 0, -1));
    }
}
