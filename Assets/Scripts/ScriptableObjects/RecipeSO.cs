using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectsSO> kitchenObjectSOList;
    public String recipeName;
}
