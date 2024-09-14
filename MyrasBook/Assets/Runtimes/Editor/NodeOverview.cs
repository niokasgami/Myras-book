using Hollow.ScriptableObjects.Nodes;
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
  [CreateAssetMenu(menuName = "hollow/GlobalConfig", fileName = "NodesOverview")]
  public class NodeOverview : GlobalConfig<NodeOverview>
  {
    [ReadOnly]
    [ListDrawerSettings(ShowFoldout = true)]
    public NodeBase[] allNodeRessources;
#if UNITY_EDITOR
    [Button(ButtonSizes.Medium), PropertyOrder(-1)]
    public void UpdateNodeOverview()
    {
      this.allNodeRessources = AssetDatabase.FindAssets("t:NodeBase")
        .Select(guid => AssetDatabase.LoadAssetAtPath<NodeBase>(AssetDatabase.GUIDToAssetPath(guid)))
        .ToArray();
    }
#endif
  }
}
#endif
