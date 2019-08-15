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

    // コンポーネントの格納
    Animator anim;
    Image img;
    Text text;
    InputField inputField;
    RectTransform rect;
    GenerateFlickGroup generator;

    // 保存されるべきパラメータ
    public int correctDirection = 0;
    public string subtitle = "かわいいシャミ子";

    // Componentの読み込み
    void Start()
    {
        setComponent();
    }

    // 回転角度
    void Update()
    {
        rect.localRotation = Quaternion.Euler(0f, 0f, rotateZ);
    }

    // animのセット
    public FlickObject setComponent()
    {
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        text = transform.Find("Text").GetComponent<Text>();
        inputField = transform.Find("InputField").GetComponent<InputField>();
        generator = transform.parent.parent.GetComponent<GenerateFlickGroup>();
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
        // フリック状態
        appearEnable = false;
        flickEnable = true;
        img.enabled = true;
        anim.SetBool("Changed", true);
        anim.SetFloat("X", 0f);
        anim.SetFloat("Y", 0f);
        anim.SetFloat("Distance", 0f);

        // 周辺のUI
        generator.frameArrowColor.ChangeColor(correctDirection);
        switchEdit(generator.isEdit);
        text.text = subtitle;
        inputField.text = subtitle;
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
