using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FlickObjectGroup : MonoBehaviour
{
    // 生成する物体のprefab
    public GameObject imgPrefab;

    // フリックする物体の格納
    List<FlickObject> flickObjs = new List<FlickObject>();
    public int index {get; set;} = 0;
    int len = 0;
    
    // 画面サイズと操作範囲
    float width = 720f;
    float height = 1280f;
    float ctrlWidth = 540f;
    float ctrlHeight = 1080f;
    
    // 触れた時のマウス位置など
    bool isPushing = false;
    Vector2 startMouse = Vector2.zero;
    Vector2 lastValue = Vector2.zero;

    // 物体が戻るアニメーションから得るパラメータ
    Animator anim;
    public float moveParam = 0f;

    // 与えられたResoucesパスのSprite読み込み
    public void readData(List<string> filepaths)
    {
        index = 0;
        foreach (var path in filepaths)
        {
            Sprite tmp = Resources.Load<Sprite>(path);
            if (tmp != null)
            {
                generateObjects(tmp);
                index++;
            }
        }
        len = flickObjs.Count;
        index = -1;
    }

    // フリックする物体の生成
    void generateObjects(Sprite img)
    {
        // 場所の指定
        GameObject tmp = Instantiate(imgPrefab);
        tmp.transform.SetParent(this.transform);
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.localRotation = Quaternion.identity;
        tmp.transform.localScale = Vector3.one;

        // 画像の指定
        tmp.transform.Find("Body").GetComponent<Image>().sprite = img;

        // リストへ格納
        FlickObject fo = tmp.transform.Find("Body").GetComponent<FlickObject>();
        flickObjs.Add(fo);
        fo.setComponent(index);
    }

    // コンポーネントの読み込み
    void Start() {
        anim = GetComponent<Animator>();
    }

    // 逐次更新
    void Update()
    {
        float valueX, valueY, distance;

        // マウスのボタンの押し直し
        if (Input.GetMouseButtonDown(0))
        {
            isPushing = true;
            startMouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            anim.SetBool("Pushed", false);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPushing = false;
            startMouse = Vector2.zero;
            anim.SetBool("Pushed", true);
        }

        // マウスのボタンが押されているとき
        if (isPushing)
        {
            // マウス位置からパラメータ計算
            float mouse_x = Input.mousePosition.x - startMouse.x;
            float mouse_y = Input.mousePosition.y - startMouse.y;
            valueX = mouse_x * 2f * width  / (Screen.width  * ctrlWidth);   // 幅　：ctrlWidth  を -1～1 に対応
            valueY = mouse_y * 2f * height / (Screen.height * ctrlHeight);  // 高さ：ctrlHeight を -1～1 に対応
            lastValue = new Vector2(valueX, valueY);
        }
        else
        {
            // 最後の位置から戻していく
            valueX = lastValue.x * moveParam;
            valueY = lastValue.y * moveParam;
        }
        distance = Mathf.Max(valueX * valueX, valueY * valueY);

        // フリック物体の操作
        if (index > -1)
        {
            if (distance >= 1f)
            {
                toFloatObj(-1);

                // マウス状態のリセット
                isPushing = false;
                startMouse = Vector2.zero;
                lastValue  = Vector2.zero;
                valueX = 0f;
                valueY = 0f;
                distance = 0f;
            }
            else
            {
                flickObjs[index].setParam(valueX, valueY);
            }
        }
    }

    // フリック物体の解除
    public void toFloatObj(int i) {
        if (index > -1 && index != i)
        {
            flickObjs[index].setFloatImg();
            index = -1;
        }
    }
}
