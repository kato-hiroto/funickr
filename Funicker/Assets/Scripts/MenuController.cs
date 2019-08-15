using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // UI要素
    public GameObject frame;
    public GenerateFlickGroup generateFlickGroup;

    // 状態
    bool isEdit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 編集ボタンを押したとき
    public void PushEdit() {
        isEdit = !isEdit;
        generateFlickGroup.isEdit = isEdit;
        frame.SetActive(isEdit);   // フレームの表示
        generateFlickGroup.returnFlickObj().switchEdit(isEdit); // 文字入力の切り替え
    }
}
