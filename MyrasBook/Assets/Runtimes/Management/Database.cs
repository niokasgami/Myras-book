using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hollow.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Hollow.Management
{
  public static class Database
  {
    public static event Action OnDataLoaded;
    
    public static Dictionary<string, ItemBase> items = new Dictionary<string, ItemBase>();
    public static Dictionary<string, NodeBase> nodes = new Dictionary<string, NodeBase>();
    public static List<VariableContainer> localVariables = new List<VariableContainer>();
    public static List<VariableContainer> globalVariables = new List<VariableContainer>();


    /// <summary>
    /// 
    /// </summary>
    public static async void LoadDatabase()
    {
      await LoadNodesData().Task;
      await LoadItemsData().Task;
      await LoadVariablesData().Task;
      OnDataLoaded?.Invoke();
    }
    private static AsyncOperationHandle<IList<NodeBase>> LoadNodesData()
    {
      return Addressables.LoadAssetsAsync<NodeBase>("nodes", (node) => { nodes.Add(node.key, node); });
    }

    private static AsyncOperationHandle<IList<ItemBase>> LoadItemsData()
    {
      return Addressables.LoadAssetsAsync<ItemBase>("items", (item) => { items.Add(item.key, item); });
    }

    private static AsyncOperationHandle<VariableOverview> LoadVariablesData()
    {
      var variablesData = Addressables.LoadAssetAsync<VariableOverview>("variables");
      variablesData.Completed += handle =>
      {
        localVariables = handle.Result.local;
        globalVariables = handle.Result.global;
      };
      return variablesData;
    }
  }
  
}
