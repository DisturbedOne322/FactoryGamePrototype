using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "New Resource")]
public class ResourceBase : ScriptableObject
{
    [Header("Instances of this type cannot be assigned. \n" +
        "Multiple instances cannot be assigned.")]
    public List<ResourceRecipe> RequiredResourceMaterials;

    [SerializeField]
    private Color _resourceColor = Color.yellow;
    public Color ResourceColor => _resourceColor;

    [SerializeField, Min(0.1f)]
    private float _timeToProduce = 1.0f;
    public float TimeToProduce => _timeToProduce;

    [SerializeField, Min(0.01f)]
    private float _timeToPick = 1.0f;
    public float TimeToPick => _timeToPick;

    private void OnValidate()
    {
        RemoveSameInstanceAsMe();
        RemoveDuplicates();
    }

    private void RemoveSameInstanceAsMe()
    {
        for (int i = RequiredResourceMaterials.Count - 1; i >= 0; i--)
        {
            if (RequiredResourceMaterials[i]?.Resource == this)
                RequiredResourceMaterials.RemoveAt(i);
        }
    }

    private void RemoveDuplicates()
    {
        HashSet<ResourceBase> uniqueResources = new HashSet<ResourceBase>();
        for (int i = 0; i < RequiredResourceMaterials.Count; i++)
        {
            ResourceBase resource = RequiredResourceMaterials[i]?.Resource;
            if (resource != null)
            {
                if (!uniqueResources.Add(resource))
                {
                    // If adding fails, it means the resource already exists, so remove it
                    RequiredResourceMaterials[i] = null;
                }
            }
        }
    }
}

[Serializable]
public class ResourceRecipe
{
    public ResourceBase Resource;
    [Min(1)]
    public int Amount = 1;
}
