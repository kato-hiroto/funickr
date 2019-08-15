using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFlickGroup : MonoBehaviour
{
    // 生成するprefab
    public GameObject groupObject;

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
        filepaths.Add("2191e9ca");
        filepaths.Add("16fc7595");
        filepaths.Add("ImageEmpty");
        fog.readData(filepaths);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
