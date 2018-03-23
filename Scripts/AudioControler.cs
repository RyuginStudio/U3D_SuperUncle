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
    public AudioSource SE_Die1;
    public AudioSource SE_Die2;
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


    //=========BGM=========//
    public AudioSource BGM_Title;
    public AudioSource BGM_Ground;
    public AudioSource BGM_Ground_Hurry;
    public AudioSource BGM_10MarioMap;
}