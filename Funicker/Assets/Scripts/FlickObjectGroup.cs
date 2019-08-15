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
    List<FlickObject> randomFlickObjs = new List<FlickObject>();

    // randomFlickObjsのインデックスなど
    int index = 0;
    int len   = 0;
    
    // 画面サイズと操作範囲
    float width = 720f;
    float height = 1280f;
    float ctrlWidth = 540f;
    float ctrlHeight = 540f;
    
    // 触れた時のマウス位置など
    bool isPushing = false;
    Vector2 startMouse = Vector2.zero;
    Vector2 lastValue = Vector2.zero;

    // 物体が戻るアニメーションから得るパラメータ
    Animator anim;
    public float moveParam = 0f;

    // 保存データの読み込み
    public void readData(List<string> filepaths)
    {
        Debug.Log(Screen.width);
        foreach (var path in filepaths)
        {
            Sprite tmp = Resources.Load<Sprite>(path);
            Debug.Log(tmp);
            generateObjects(tmp);
        }
        generateFirstState();
    }

    // フリックする物体の生成
    void generateObjects(Sprite img)
    {
        // 場所の指定
        GameObject tmp = Instantiate(imgPrefab);
        tmp.transform.SetParent(this.transform);
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.localRotation = Quaternion.identity;

        // 画像の指定
        tmp.GetComponent<Image>().sprite = img;

        // リストへ格納
        flickObjs.Add(tmp.GetComponent<FlickObject>());
    }

    // 物体の初期状態
    void generateFirstState()
    {
        randomFlickObjs = flickObjs.OrderBy(i => Guid.NewGuid()).ToList();
        index = 0;
        len   = randomFlickObjs.Count;
        randomFlickObjs[index].setComponent().setFlickImg();
        randomFlickObjs[(index + 1) % len].setComponent().setNextImg();
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

        // フリックする物体の状態変更
        if (distance >= 1f) {
            randomFlickObjs[index].setEndImg();
            randomFlickObjs[(index + 1) % len].setFlickImg();
            randomFlickObjs[(index + 2) % len].setNextImg();
            index = (index + 1) % len;

            // マウス状態のリセット
            isPushing = false;
            startMouse = Vector2.zero;
            lastValue  = Vector2.zero;
            valueX = 0f;
            valueY = 0f;
            distance = 0f;
        }
        
        // フリックする物体のパラメータ変更
        randomFlickObjs[index].setParam(valueX, valueY, distance);
        randomFlickObjs[(index + 1) % len].setParam(valueX, valueY, distance);
    }

    // フリックする物体の削除
    void deleteObjects()
    {

    }

}
