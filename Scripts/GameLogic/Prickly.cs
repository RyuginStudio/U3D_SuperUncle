/*
 * 时间：2018年3月9日17:28:41
 * 作者：vszed
 * 功能：挂载到刺头图块下，触碰即死
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prickly : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() && !collision.gameObject.GetComponent<Character>().isUnmatched)
        {
            GameControler.getInstance().gameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() && !collision.gameObject.GetComponent<Character>().isUnmatched)
        {
            GameControler.getInstance().gameOver();
        }
    }
}
