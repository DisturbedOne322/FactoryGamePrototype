using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour
{
    [SerializeField]
    private Text _countText;
    [SerializeField]
    private Image _image;

    public InventoryItemDisplay SetColor(Color color)
    {
        _image.color = color;
        return this;
    }

    public InventoryItemDisplay SetCount(int count)
    {
        _countText.text = count.ToString();
        return this;    
    }
}
