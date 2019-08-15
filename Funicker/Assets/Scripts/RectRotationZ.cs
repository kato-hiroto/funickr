using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectRotationZ : MonoBehaviour
{
    // 回転用のパラメータ
    public float rotateZ = 0f;
    RectTransform rect;

    // コンポーネントの読み込み
    void Start()
    {        
        rect = GetComponent<RectTransform>();
    }

    // 回転角度
    void Update()
    {
        rect.localRotation = Quaternion.Euler(0f, 0f, rotateZ);
    }

}
