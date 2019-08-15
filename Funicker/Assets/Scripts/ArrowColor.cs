using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowColor : MonoBehaviour
{
    public List<Image> arrows = new List<Image>();

    // インデックスで指定された印の色のみ変える
    public void ChangeColor(int index)
    {
        foreach(var item in arrows)
        {
            item.color = new Color(1f, 1f, 0f);
        }
        if (index < arrows.Count)
        {
            arrows[index].color = new Color(1f, 0f, 0f);
        }
    }

}
