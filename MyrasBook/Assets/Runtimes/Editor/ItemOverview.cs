using Hollow.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
namespace Hollow.Editor
{
  using Sirenix.Utilities;
  using System.Linq;
  
#if UNITY_EDITOR
  using UnityEditor;
#endif
  [CreateAssetMenu(menuName = "hollow/ItemGlobalConfig", fileName = "ItemOverview")]
  public class ItemOverview : GlobalConfig<ItemOverview>
  {
    [ReadOnly]
    [ListDrawerSettings(ShowFoldout = true)]
    public ItemBase[] allItems;
#if UNITY_EDITOR
    [Button(ButtonSizes.Medium), PropertyOrder(-1)]
    public void UpdateNodeOverview()
    {
      this.allItems = AssetDatabase.FindAssets("t:ItemBase")
        .Select(guid => AssetDatabase.LoadAssetAtPath<ItemBase>(AssetDatabase.GUIDToAssetPath(guid)))
        .ToArray();
    }
#endif
  }
}
#endif
