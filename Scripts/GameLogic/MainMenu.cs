/*
 * 时间：2018年3月26日21:23:23
 * 作者：vszed
 * 功能：游戏主菜单
 * 1.退出至标题
 * 2.重新开始当前关卡（生命-1）
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject btn_RestartMission;
    public GameObject btn_QuitToTitle;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //按钮禁用
        if (GameData.MarioLives < 2)
            btn_RestartMission.GetComponent<Button>().interactable = false;
    }

    public void quitToTitle()
    {
        btn_RestartMission.GetComponent<Button>().interactable = false;
        btn_QuitToTitle.GetComponent<Button>().interactable = false;

        GameControler.getInstance().GameOver = true;
        AudioControler.getInstance().SE_Daikettefinal.Play();
        StartCoroutine(SceneTransition.getInstance().loadScene("TitleScene", 0.3f, 3));
    }

    public void closeMenu()
    {
        AudioControler.getInstance().SE_SYS_PAUSE.Play();
        gameObject.SetActive(false);
    }

    //重新开始当前关卡
    public void restartCurrentMission()
    {
        //加限制
        if (GameData.MarioLives > 1)
        {
            btn_QuitToTitle.GetComponent<Button>().interactable = false;
            btn_RestartMission.GetComponent<Button>().interactable = false;

            AudioControler.getInstance().SE_PlayExit.Play();

            GameControler.getInstance().gameOver();
        }

    }

}
