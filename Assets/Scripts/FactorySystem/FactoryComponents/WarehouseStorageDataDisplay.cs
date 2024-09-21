using UnityEngine;
using UnityEngine.UI;

public class WarehouseStorageDataDisplay : MonoBehaviour
{
    [SerializeField]
    private WarehouseStorage _storage;

    [SerializeField]
    private Text _dataText;

    private void DisplayData()
    {
        switch (_storage.StorageType)
        {
            case WarehouseStorageType.ResourceStore:
                _dataText.text = _storage.AmountStored + " / " + _storage.StoreCapacity;
                break;
            default:
                _dataText.text = _storage.AmountStored + " / " + _storage.StoreCapacity;
                break;
        }
    }
}
