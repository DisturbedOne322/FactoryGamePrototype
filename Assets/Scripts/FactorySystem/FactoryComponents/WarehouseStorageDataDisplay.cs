using UnityEngine;
using UnityEngine.UI;

public class WarehouseStorageDataDisplay : MonoBehaviour
{
    [SerializeField]
    private WarehouseStorage _storage;

    [SerializeField]
    private Text _dataText;

    private void Awake()
    {
        _storage.OnItemCountChanged += _storage_OnItemCountChanged;
    }

    private void OnDestroy()
    {
        _storage.OnItemCountChanged -= _storage_OnItemCountChanged;
    }

    private void _storage_OnItemCountChanged(int count, int capacity)
    {
        _dataText.text = count + " / " + capacity;
    }
}
