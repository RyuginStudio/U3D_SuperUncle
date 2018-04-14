using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControler : MonoBehaviour
{
    private static AudioControler instance;

    public static AudioControler getInstance()
    {
        return instance;
    }

    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //=========S E=========//
    public AudioSource SE_HurryUp;
    public AudioSource SE_OhNo;
    public AudioSource SE_Die;
    public AudioSource SE_Jump;
    public AudioSource SE_Hit_Block;
    public AudioSource SE_Gain_Coin;
    public AudioSource SE_FootNote;
    public AudioSource SE_FootNote_L;
    public AudioSource SE_FallDown;
    public AudioSource SE_Emy_Fumu;
    public AudioSource SE_Emy_Down;
    public AudioSource SE_Appear;
    public AudioSource SE_vszed;
    public AudioSource SE_Confirm;
    public AudioSource SE_Invalid;
    public AudioSource SE_Daikettefinal;
    public AudioSource SE_MoveMap;
    public AudioSource SE_GameOver;
    public AudioSource SE_SYS_PAUSE;
    public AudioSource SE_PlayExit;
    public AudioSource SE_SYS_GOAL_FLAG;
    public AudioSource SE_course_clear;
    public AudioSource SE_VOC_MA_CLEAR_NORMAL;
    public AudioSource SE_SCORE_COUNT;
    public AudioSource SE_SCORE_COUNT_FINISH;
    public AudioSource SE_ONE_UP;
    public AudioSource SE_VOC_GET_STAR;
    public AudioSource SE_PLY_CHANGE_BIG;
    public AudioSource SE_STAR_FINISH;
    public AudioSource SE_PlayNext;
    public AudioSource SE_FlyClapLong;
    public AudioSource SE_CourseClearFilter;

    //=========BGM=========//
    public AudioSource BGM_Title;
    public AudioSource BGM_Ground;
    public AudioSource BGM_Ground_Hurry;
    public AudioSource BGM_10MarioMap;
    public AudioSource BGM_SickCow;
    public AudioSource BGM_MarioKartStar;
    public AudioSource BGM_VersionUpdate;

    //关掉所有BGM => 在新的BGM播放之前调用
    public void stopAllBGM()
    {
        if (AudioControler.getInstance().BGM_Ground != null && AudioControler.getInstance().BGM_Ground.isPlaying)
            AudioControler.getInstance().BGM_Ground.Stop();
        else if (AudioControler.getInstance().BGM_Ground_Hurry != null && AudioControler.getInstance().BGM_Ground_Hurry.isPlaying)
            AudioControler.getInstance().BGM_Ground_Hurry.Stop();
        else if (AudioControler.getInstance().BGM_SickCow != null && AudioControler.getInstance().BGM_SickCow.isPlaying)
            AudioControler.getInstance().BGM_SickCow.Stop();
        else if (AudioControler.getInstance().BGM_10MarioMap != null && AudioControler.getInstance().BGM_10MarioMap.isPlaying)
            AudioControler.getInstance().BGM_10MarioMap.Stop();
        else if (AudioControler.getInstance().BGM_MarioKartStar != null && AudioControler.getInstance().BGM_MarioKartStar.isPlaying)
            AudioControler.getInstance().BGM_MarioKartStar.Stop();
        else if (AudioControler.getInstance().BGM_Title != null && AudioControler.getInstance().BGM_Title.isPlaying)
            AudioControler.getInstance().BGM_Title.Stop();
        else if (AudioControler.getInstance().BGM_VersionUpdate != null && AudioControler.getInstance().BGM_VersionUpdate.isPlaying)
            AudioControler.getInstance().BGM_VersionUpdate.Stop();
    }
}