using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickObject : MonoBehaviour
{
    // 表示と状態のフラグ
    bool appearEnable = false;
    bool flickEnable = false;

    // 回転用のパラメータ
    public float rotateZ = 0f;

    // 浮遊用のパラメータ
    public float floating = 0f;
    public bool isFloating {get; set; } = false;
    float width = Screen.width;
    float height = Screen.height;
    float Xseed = 0f;
    float Yseed = 0f;

    // コンポーネントの格納
    bool doAccessComponent = false;
    Animator anim;
    Image img;
    Text text;
    InputField inputField;
    RectTransform rect;
    GenerateFlickGroup generator;
    GameObject myRoot;

    // 保存されるべきパラメータ
    public int correctDirection = 0;
    public string subtitle = "";

    void Start()
    {
        setComponent();
    }

    void Update()
    {
        // 浮遊時の位置
        if (isFloating)
        {
            float seed_floating = (floating + Xseed + 2) % 2 - 1;
            float xpos = Mathf.Abs(seed_floating) * width * 1.2f - width * 0.1f;
            float ypos = Mathf.Sign(seed_floating) * Yseed * height / 2f + height / 2f;
            myRoot.transform.position = new Vector3(xpos, ypos, 0f);
        }
        
        // 回転角度
        rect.localRotation = Quaternion.Euler(0f, 0f, rotateZ);
    }

    // Componentの読み込み
    public FlickObject setComponent()
    {
        if (!doAccessComponent)
        {
            doAccessComponent = true;
            anim = GetComponent<Animator>();
            img = GetComponent<Image>();
            rect = GetComponent<RectTransform>();
            text = transform.Find("Text").GetComponent<Text>();
            inputField = transform.Find("InputField").GetComponent<InputField>();
            generator = transform.parent.parent.GetComponent<GenerateFlickGroup>();

            // 自分のルートオブジェクト作成
            myRoot = new GameObject();
            myRoot.transform.SetParent(transform.parent);
            myRoot.transform.localPosition = Vector3.zero;
            myRoot.transform.localRotation = Quaternion.identity;
            myRoot.transform.localScale = Vector3.one;
            transform.SetParent(myRoot.transform);
        }
        return this;
    }

    // 次の画像としてセット
    public void setNextImg()
    {
        appearEnable = true;
        isFloating = false;
        img.enabled = true;
        anim.SetBool("Changed", false);
        anim.SetBool("Floating", false);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);
        myRoot.transform.position = new Vector3(width / 2f, height / 2f, 0f);
    }

    // フリック画像としてセット
    public void setFlickImg()
    {
        // フリック状態
        appearEnable = false;
        isFloating = false;
        flickEnable = true;
        img.enabled = true;
        anim.SetBool("Changed", true);
        anim.SetBool("Floating", false);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);
        myRoot.transform.position = new Vector3(width / 2f, height / 2f, 0f);

        // 周辺のUI
        generator.frameArrowColor.ChangeColor(correctDirection);
        switchEdit(generator.isEdit);
        text.text = subtitle;
        inputField.text = subtitle;
    }

    // 浮遊画像としてセット
    public void setFloatImg()
    {
        rotateZ = 0f;
        isFloating = true;
        flickEnable = false;
        img.enabled = true;
        anim.SetBool("Floating", true);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);
        myRoot.transform.position = new Vector3(width / 2f, height / 2f, 0f);

        // Seed値のセット
        Xseed = Random.Range(-1f, 1f);
        Yseed = Random.Range(0f, 1f);
    }

    // 終わった画像としてセット
    public void setEndImg()
    {
        isFloating = false;
        flickEnable = false;
        img.enabled = false;
        anim.SetBool("Changed", false);
        anim.SetBool("Floating", false);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);
        myRoot.transform.position = new Vector3(width / 2f, height / 2f, 0f);
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

    // サブタイトル編集状態の切り替え
    public void switchEdit(bool state)
    {
        inputField.gameObject.SetActive(state);
        text.gameObject.SetActive(!state);
    }

    // サブタイトルの書き換え
    public void changeSubtitle() {
        text.text = inputField.text;
        subtitle = inputField.text;
    }
}
