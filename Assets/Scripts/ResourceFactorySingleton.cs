using System.Threading.Tasks;
using UnityEngine;

public class ResourceFactorySingleton
{
    public static ResourceFactorySingleton Instance
    {
        get
        {
            if(Instance == null)
                Instance = new ResourceFactorySingleton();

            return Instance;
        }
        set => Instance = value;
    }

    public async Task<GameObject> ProduceResource(ResourceBase resource)
    {
        await Task.Delay((int)(resource.TimeToProduce * 1000));

        GameObject result = new GameObject();
        result.GetComponent<MeshRenderer>().material.color = resource.ResourceColor;
        return result;
    }
}
