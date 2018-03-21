/*
 * 时间：2018年3月21日15:55:22
 * 作者：vszed
 * 功能：Logo+欢迎界面
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    [SerializeField] private GameObject blackMask;

    [SerializeField] private bool startLogoAnim;

    // Use this for initialization
    void Start()
    {
        Invoke("startLogo", 2);
    }

    // Update is called once per frame
    void Update()
    {
        changeLogoAlpha();
    }

    void startLogo()
    {
        startLogoAnim = true;
    }

    void changeLogoAlpha()
    {
        if (startLogoAnim)
        {
            var currentColor = blackMask.GetComponent<SpriteRenderer>().color;
            if (currentColor.a == 0)
            {
                Invoke("jumpScene", 2);
            }
            blackMask.GetComponent<SpriteRenderer>().color = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a - .03f);
        }
    }

    void jumpScene()
    {
        SceneManager.CreateScene("MainScene");
    }
}
