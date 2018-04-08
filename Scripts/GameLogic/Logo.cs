using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logo : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Invoke("playSound", 0.5f);
        Invoke("jumpScene", 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void playSound()
    {
        AudioControler.getInstance().SE_vszed.Play();
    }

    void jumpScene()
    {
        StartCoroutine(SceneTransition.getInstance().loadScene("TitleScene", 0, 2));
    }
}
