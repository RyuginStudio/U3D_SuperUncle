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

    //=========BGM=========//
    public AudioSource BGM_Ground;
    public AudioSource BGM_Ground_Hurry;
}