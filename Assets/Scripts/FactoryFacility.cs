using System.Collections;
using UnityEngine;

public class FactoryFacility : MonoBehaviour
{
    [SerializeField]
    private ResourceBase _producedResource;
    [SerializeField]
    private WarehouseFacility _storeWarehouseFacility;
    [SerializeField]
    private WarehouseFacility _produceWarehouseFacility;

    private bool _producing = false;
    private float _progress = 0;

    private void Update()
    {
        if(!_producing)
            _producing = TryScheduleProduction();
        else
            ProduceResource();
    }

    private bool TryScheduleProduction()
    {       
        if(!_produceWarehouseFacility.SubstorageHasSpace(_producedResource))
            return false;

        int requiredMatsSize = _producedResource.RequiredResourceMaterials.Count;
        for (int i = 0; i < requiredMatsSize; i++)
        {
            if (!_storeWarehouseFacility.SubstorageHasResource(_producedResource.RequiredResourceMaterials[i]))
                return false;
        }

        for (int i = 0; i < requiredMatsSize; i++)
        {
            _storeWarehouseFacility.RemoveFromSubstorage(_producedResource.RequiredResourceMaterials[i]);
        }

        return true;
    }

    private void ProduceResource()
    {
        _progress += Time.deltaTime;
        if(_progress >= _producedResource.TimeToProduce)
        {
            _progress = 0;
            _produceWarehouseFacility.AddToSubstorage(_producedResource);
            _producing = false;
        }
    }
}
