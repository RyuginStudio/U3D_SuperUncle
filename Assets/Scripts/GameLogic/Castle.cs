/*
 * 时间：2018年3月27日01:59:38
 * 作者：vszed
 * 功能：挂载到城堡图块下
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);

        if (EnemyEditor.getInstance() != null)
        {
            GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(isGroundedSetStatic());
        displayMask();
    }

    public IEnumerator isGroundedSetStatic()
    {
        //在编辑地图中会直接删掉刚体
        if (GetComponentInParent<Rigidbody2D>() && GetComponentInParent<Rigidbody2D>().velocity.y == 0)
        {
            yield return new WaitForSeconds(2);
            Destroy(GetComponentInParent<Rigidbody2D>());
            Destroy(GetComponentInParent<BoxCollider2D>());
        }
    }

    public void displayMask()
    {
        if (Flag.alreadyGetFlag)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
