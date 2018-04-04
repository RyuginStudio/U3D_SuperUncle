/*
 * 时间：2018年4月3日09:57:32
 * 作者：vszed
 * 功能：跨平台操控
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossPlatform : MonoBehaviour
{
    public GameObject character;
    public bool btn_A_isPressed;

    private static CrossPlatform instance;

    public static CrossPlatform getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
        judgePlatform();
    }

    // Update is called once per frame
    void Update()
    {
        characterMove();
    }

    void judgePlatform()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
            Destroy(gameObject);
    }

    public void BtnB_Jump()
    {
        if (Character.getInstance().getIsGrounded() && !GameControler.getInstance().GameOver)
        {
            character.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 730));
            AudioControler.getInstance().SE_Jump.Play();
        }
    }

    public void Btn_A_LongPressed()
    {
        btn_A_isPressed = true;
    }
    public void Btn_A_Click()
    {
        btn_A_isPressed = false;
    }

    public void characterMove()
    {
        if (Joystick.getInstance().isBeginDrag && !GameControler.getInstance().GameOver && !Character.getInstance().getAnimator().GetBool("isDie") && !Character.getInstance().getAnimator().GetBool("isGetFlag") && !Character.getInstance().getAnimator().GetBool("goToCastle"))
        {
            if (GameObject.Find("Stick").transform.localPosition.x > 0)
            {
                Character.getInstance().characterMove(1);
                //Debug.Log("Right");
            }
            else
            {
                Character.getInstance().characterMove(-1);
                //Debug.Log("Left");
            }
        }
    }

    public GameObject menuUI;

    //打开菜单
    public void onOpenMenu()
    {
        //实现手机晃动震动效果
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif

        AudioControler.getInstance().SE_SYS_PAUSE.Play();
        if (menuUI.activeSelf)
            menuUI.SetActive(false);
        else
            menuUI.SetActive(true);
    }
}