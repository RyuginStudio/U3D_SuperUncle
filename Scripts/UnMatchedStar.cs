/*
 * 时间：2018年4月1日15:20:00
 * 作者：vszed
 * 功能：无敌幸运星
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnMatchedStar : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    private bool isContact;

    // Use this for initialization
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();

        if (EnemyEditor.getInstance() != null)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        jump();
    }

    void jump()
    {
        if (!isContact && m_rigidBody.velocity.y == 0)
        {
            m_rigidBody.AddForce(new Vector2(0, 300));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(contactFunc(collision));
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(contactFunc(collision));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(contactFunc(collision.collider));
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        StartCoroutine(contactFunc(collision.collider));
    }

    private IEnumerator contactFunc(Collider2D collision)
    {
        if (!isContact)
        {
            if (collision.GetComponent<Character>() && !collision.GetComponent<Character>().isUnmatched)
            {
                isContact = true;

                Destroy(GetComponent<CircleCollider2D>());
                Destroy(GetComponent<SpriteRenderer>());
                Destroy(GetComponent<Rigidbody2D>());

                StartCoroutine(GameControler.getInstance().ScoreUIControl(500, transform.position, 0));

                collision.GetComponent<Character>().isUnmatched = true;

                AudioControler.getInstance().stopAllBGM();
                AudioControler.getInstance().SE_PLY_CHANGE_BIG.Play();
                yield return new WaitForSeconds(.2f);
                AudioControler.getInstance().SE_VOC_GET_STAR.Play();
                yield return new WaitForSeconds(.2f);
                AudioControler.getInstance().BGM_MarioKartStar.Play();
                yield return new WaitForSeconds(17.5f);
                AudioControler.getInstance().SE_STAR_FINISH.Play();
                yield return new WaitForSeconds(1);
                AudioControler.getInstance().stopAllBGM();
                AudioControler.getInstance().BGM_Ground.Play();

                collision.GetComponent<Character>().isUnmatched = false;
                Destroy(gameObject);
            }
            else if (collision.GetComponent<Character>() && collision.GetComponent<Character>().isUnmatched)  //无敌不会附加
            {
                isContact = true;

                Destroy(GetComponent<CircleCollider2D>());
                Destroy(GetComponent<SpriteRenderer>());
                Destroy(GetComponent<Rigidbody2D>());

                AudioControler.getInstance().SE_VOC_GET_STAR.Play();
                StartCoroutine(GameControler.getInstance().ScoreUIControl(1000, transform.position, 0));

                Destroy(gameObject, 3);
            }
        }
    }
}
