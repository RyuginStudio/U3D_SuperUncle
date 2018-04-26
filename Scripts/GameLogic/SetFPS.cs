/*
 * 时间：2018年4月26日16:40:35
 * 作者：vszed
 * 功能：ios|android平台下更改游戏帧数 => 60
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFPS : MonoBehaviour
{
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            Application.targetFrameRate = 60;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
