using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickObject : MonoBehaviour
{
    // 浮遊用のパラメータ
    public float floating = 0f;
    public bool isFloating {get; set; } = true;
    float width = 720;
    float height = 1280;
    float Xseed = 0f;
    float Yseed = 0f;

    // コンポーネントの格納
    bool doAccessComponent = false;
    Animator anim;
    Image img;
    Canvas mySortCanvas;
    RectTransform rect;
    RectTransform myRoot;
    FlickObjectGroup flickObjectGroup;

    // 保存されるべきパラメータ
    public int index {get; set;} = 0;

    void Start()
    {
        setComponent(0);
        setFloatImg();
    }

    void Update()
    {
        // 浮遊時の位置
        if (isFloating)
        {
            float seed_floating = (floating + Xseed + 2) % 2 - 1;
            float xpos = Mathf.Abs(seed_floating) * width * 1.2f - width * 0.6f;
            float ypos = Mathf.Sign(seed_floating) * Mathf.Sin(Yseed * 10f * Mathf.PI) * height / 2f;
            myRoot.localPosition = new Vector3(xpos, ypos, 0f);
        }
    }

    // Componentの読み込み
    public FlickObject setComponent(int i)
    {
        if (!doAccessComponent)
        {
            doAccessComponent = true;
            index = i;
            anim = GetComponent<Animator>();
            img = GetComponent<Image>();
            rect = GetComponent<RectTransform>();
            myRoot = transform.parent.GetComponent<RectTransform>();
            mySortCanvas = transform.parent.GetComponent<Canvas>();
            flickObjectGroup = transform.parent.parent.GetComponent<FlickObjectGroup>();
        }
        return this;
    }

    // フリック画像としてセット
    public void setFlickImg()
    {
        // フリック状態
        isFloating = false;
        flickObjectGroup.toFloatObj(index);
        flickObjectGroup.index = index;
        anim.SetBool("Floating", false);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        myRoot.localPosition = Vector3.zero;
        mySortCanvas.sortingOrder = 1;
    }

    // 浮遊画像としてセット
    public void setFloatImg()
    {
        isFloating = true;
        anim.SetBool("Floating", true);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        myRoot.localPosition = Vector3.zero;
        mySortCanvas.sortingOrder = 0;

        // Seed値のセット
        Xseed = Random.Range(-1f, 1f);
        Yseed = Random.Range(0f, 1f);
    }

    // パラメータのセット
    public void setParam(float x, float y)
    {
        if (!isFloating)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
        }
        
    }
}
