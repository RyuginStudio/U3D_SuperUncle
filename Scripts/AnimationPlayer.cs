using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public List<Sprite> AnimationFrames;
    public int FramesIdx;
    public int RepeatIdx;  //二次循环起始图
    public float ScheduleUpdate;
    public float CurrentTime;
    public float DeltaTime;
    public bool autoPlay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (autoPlay == true)
        {
            play();
        }
    }

    public void play()
    {
        CurrentTime = Time.time;

        if (CurrentTime - ScheduleUpdate > DeltaTime)
        {
            ScheduleUpdate = Time.time;

            this.GetComponent<Image>().sprite = AnimationFrames[FramesIdx];

            if (FramesIdx < AnimationFrames.Count - 1)
            {
                ++FramesIdx;
            }
            else
            {
                FramesIdx = RepeatIdx;
            }
        }

    }

}
