using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hollow.ScriptableObjects.Nodes
{
  /// <summary>
  /// The node that display an linear text
  /// </summary>
  [CreateAssetMenu(menuName = "hollow/createNewNode", fileName = "newLinearNode")]
  public class NodeLinear : NodeBase
  {
    [Title("Linear node data")]
    [BoxGroup("Linear data", showLabel: false)]
    [TextArea]
    public string text;
    [FormerlySerializedAs("nextNode")]
    [BoxGroup("Linear data"), LabelWidth(70)]
    public string nextNode;

    
    public NodeLinear()
    {
      this.type = NodeType.Linear;
    }

    public override string Text()
    {
      return text;
    }

    public override string FetchNextNode(int index)
    {
      return nextNode;
    }
  }
}


