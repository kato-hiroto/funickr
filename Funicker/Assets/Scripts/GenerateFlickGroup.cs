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
        List<int> numbers = RandomNumbers(70, 10);
        string namebase = "syamiko0";
        for (int i = 0; i < 10; i++)
        {
            foreach(int item in numbers)
            {
                string num = item.ToString();
                if (item < 10)
                {
                    num = "0" + num;
                }
                filepaths.Add(namebase + num);
            }            
        }
        fog.readData(filepaths);
    }

    // 0からendMax-1までランダムに重複なくN個の数字を返す関数
    List<int> RandomNumbers(int endMax, int N)
    {
        List<int> numbers = new List<int>();
        for (int i = 0; i < N; i++)
        {
            int val = Random.Range(0, endMax - i);

            // valのマッピング範囲を移動
            for (int j = 0; j < i; j++)
            {
                if (val >= numbers[j])
                {
                    val++;
                }
            }
            numbers.Add(val);
        }
        return numbers;
    }
}
