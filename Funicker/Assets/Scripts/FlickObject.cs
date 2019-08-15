using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickObject : MonoBehaviour
{
    // 表示と状態のフラグ
    bool appearEnable = false;
    bool flickEnable = false;

    // コンポーネントの格納
    Animator anim;
    Image img; 

    // Componentの読み込み
    void Start()
    {
        setComponent();
    }

    // animのセット
    public FlickObject setComponent()
    {
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
        return this;
    }

    // 次の画像としてセット
    public void setNextImg()
    {
        appearEnable = true;
        img.enabled = true;
        anim.SetBool("Changed", false);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);
    }

    // フリック画像としてセット
    public void setFlickImg()
    {
        appearEnable = false;
        flickEnable = true;
        img.enabled = true;
        anim.SetBool("Changed", true);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);
    }

    // 終わった画像としてセット
    public void setEndImg()
    {
        flickEnable = false;
        img.enabled = false;
        anim.SetBool("Changed", false);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);
    }

    // パラメータのセット
    public void setParam(float x, float y, float distance)
    {
        if (appearEnable)
        {
            anim.SetFloat("Distance", distance);
        }
        else if (flickEnable)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
    }
}
