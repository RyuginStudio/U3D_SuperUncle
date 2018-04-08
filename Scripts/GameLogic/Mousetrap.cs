/*
 * 时间：2018年3月31日18:35:19
 * 作者：vszed
 * 功能：捕鼠器 => 可下不可上
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousetrap : MonoBehaviour
{
    //捕鼠器显示后的图片
    public Sprite block;

    // Use this for initialization
    void Start()
    {
        //游戏模式
        if (!MapEditor.getInstance())
            GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>())
        {
            if (collision.GetComponent<Rigidbody2D>().velocity.y > 0) //保证为向上
            {
                if (!AudioControler.getInstance().BGM_SickCow.isPlaying)
                {
                    AudioControler.getInstance().stopAllBGM();
                    AudioControler.getInstance().BGM_SickCow.Play();
                }
                GetComponent<SpriteRenderer>().sprite = block;
                GetComponent<SpriteRenderer>().enabled = true;

                foreach (var item in GetComponents<BoxCollider2D>())
                {
                    item.enabled = true;
                    item.isTrigger = false;
                }
                Destroy(this);
            }

        }
    }
}
