using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // UI要素
    public GameObject frame;
    public GenerateFlickGroup generateFlickGroup;
    FlickObjectGroup fog = null;

    // 状態
    bool isEdit = false;
    bool isPanel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fog == null)
        {
            fog = generateFlickGroup.ReturnFog();
        }
    }

    // 編集ボタンを押したとき
    public void PushEdit() {
        isEdit = !isEdit;
        generateFlickGroup.isEdit = isEdit;
        frame.SetActive(isEdit);   // フレームの表示
        fog?.returnFlickObj().switchEdit(isEdit); // 文字入力の切り替え
    }
    
    // 展開ボタンを押したとき
    public void PushPanel() {
        isPanel = !isPanel;
        generateFlickGroup.isPanel = isPanel;
        fog.ChgPanelMode(isPanel);
        fog.returnFlickObj().switchEdit(isEdit); // 文字入力の切り替え
    }
}
