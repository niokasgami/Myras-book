using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Hollow.Editor
{

  public class NodeTable
  {
    [TableList(IsReadOnly = true, AlwaysExpanded = true ), ShowInInspector]
    private readonly List<NodeWrapper> allNodes;

    public NodeBase this[int index]
    {
      get { return this.allNodes[index].Node; }
    }

    public NodeTable(IEnumerable<NodeBase> nodes)
    {
      this.allNodes = nodes.Select(x => new NodeWrapper(x)).ToList();
    }
    
    private class NodeWrapper
    {
      private NodeBase node;

      public NodeBase Node
      {
        get { return this.node; }
      }

      public NodeWrapper(NodeBase node)
      {
        this.node = node;
      }
      
      [TableColumnWidth(120, resizable:false)]
      [ShowInInspector]

      public string Key { get { return this.node.key; } set { this.node.key = value; EditorUtility.SetDirty(this.node); }
      }
      
      [TableColumnWidth(120, resizable:false)]
      [ShowInInspector]
      public string Previous { get { return this.node.previous; } set { this.node.previous = value; EditorUtility.SetDirty(this.node); }
      }
      
      [TableColumnWidth(200, resizable:false)]
      [ShowInInspector]
      public string Location { get { return this.node.location; } set { this.node.location = value; EditorUtility.SetDirty(this.node); }
      }
      [TableColumnWidth(50)]
      [ShowInInspector]
      [DisplayAsString]
      public NodeType Type { get { return this.node.type; } } 
     
      // todo figure out how to rename  in the preview but this is not mandatory
      private void OnNameChange()
      {
        var path = AssetDatabase.GetAssetPath(this.node);
        AssetDatabase.RenameAsset(path, this.node.key);
      }
    }
  }
}
