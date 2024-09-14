using System.Linq;
using Hollow.ScriptableObjects;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;

namespace Hollow.Utils
{
  #if UNITY_EDITOR
  /**
   * Utility class that allow to fetch any object base and convert it into an dropdown list
   */
  public class ObjectFetcher<T> where T : ObjectBase
  {
    private readonly string[] objects = null;

    public ObjectFetcher()
    {
      objects = FetchAllKeys();
    }


    private string[] FetchAllKeys()
    {
      var resources = AssetDatabase.FindAssets("t:" + typeof(T).Name)
        .Select(guid => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid)))
        .ToArray();
      
      string[] keys = new string[resources.Length];

      for (var i = 0; i < resources.Length; i++)
      {
        keys[i] = resources[i].key;
      }

      // here we just sort the entry for conveniences sake in the editor.
      keys.Sort();
      return keys;
    }

    /// Retrieves dropdown items based on an array of strings.
    /// @param array The array of strings.
    /// @return The dropdown list containing the array elements as items.
    /// /
    public ValueDropdownList<string> GetValueDropdownItems()
    {
      var list = new ValueDropdownList<string>();
      foreach (var element in FetchAllKeys())
      {
        list.Add(element,element);
      }

      return list;
    }
  }
  #endif
}