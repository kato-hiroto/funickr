using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFlickGroup : MonoBehaviour
{
    // 生成するprefab
    public GameObject groupObject;

    // 編集フレームの色操作
    public ArrowColor frameArrowColor;

    // 編集状態
    public bool isEdit {get; set; } = false;

    // 操作対象のFlickObjectGroup
    FlickObjectGroup fog;

    // Start is called before the first frame update
    void Start()
    {
        // fogの生成
        GameObject tmp = Instantiate(groupObject);
        tmp.transform.SetParent(this.transform);
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.localRotation = Quaternion.identity;
        tmp.transform.localScale = Vector3.one;
        fog = tmp.GetComponent<FlickObjectGroup>();

        // fogの物体生成関数を実行
        List<string> filepaths = new List<string>();
        string namebase = "syamiko0";
        for (int i = 0; i < 71; i++)
        {
            string num = i.ToString();
            if (i < 10)
            {
                num = "0" + num;
            }
            filepaths.Add(namebase + num);
        }
        fog.readData(filepaths);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 現在の操作対象であるフリック物体を返す
    public FlickObject returnFlickObj() {
        return fog.returnFlickObj();
    }

    // 編集状態の受信
    // public void changeIsEdit(bool state) {
    //     isEdit = state;
    // }
}
