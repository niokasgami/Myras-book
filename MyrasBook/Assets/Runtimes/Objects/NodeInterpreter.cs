using System.Collections.Generic;
using System.Linq;
using Hollow.Management;
using Hollow.ScriptableObjects.Nodes;
using UnityEngine;

namespace Hollow.Objects
{
  
  public class NodeInterpreter : MonoBehaviour
  {

    [HideInInspector]
    public NodeBase currentNode = null;
    
    public string startNode;
    private Dictionary<string, NodeBase> nodes = new Dictionary<string, NodeBase>();
    
    

    private void Start()
    {
      Database.OnDataLoaded += AssignData;
      //   nodes = Database.nodes;
      //   currentNode = FetchNode("Start");
    }

    /// <summary>
    /// Assigns the data to the nodes and current key.
    /// </summary>
    private void AssignData()
    {
      nodes = Database.nodes;
      currentNode = FetchNode(startNode);
      Database.OnDataLoaded -= AssignData;
    }
    
    /// <summary>
    /// Retrieves the text of the current key.
    /// </summary>
    /// <returns>The text of the current key.</returns>
    public string Text()
    {
      return currentNode.Text();
    }

    /**
     * return the current location the player is at.
     */
    public string Location()
    {
      return this.currentNode.location;
    }
    /// <summary>
    /// Retrieves the type of the current key.
    /// </summary>
    /// <returns>The type of the current key.</returns>
    public NodeType Type()
    {
      return currentNode.type;
    }
    
    public NodeBase FetchNode(string key)
    {
      if (!nodes.ContainsKey(key))
      {
        Debug.LogError($"The key {key} does not exist!");
      }
      return nodes[key];
    }


    /// <summary>
    /// Moves to the next key in the sequence.
    /// </summary>
    /// <param name="index">The index of the current key. Default value is -1.</param>
    public void Next(int index = -1)
    {
      var nextNodeKey = currentNode.FetchNextNode(index);
      currentNode = FetchNode(nextNodeKey);

      if (currentNode.type == NodeType.Action)
      {
        ExecuteActions();
      }
    }

    public void Jump(string key)
    {
      var node = FetchNode(key);
      currentNode = node;
      if (currentNode.type == NodeType.Action)
      {
        ExecuteActions();
      }
      GameManager.Instance.RequestGuiRefresh();
    }

    public void ExecuteActions()
    {
      var node = (NodeAction)currentNode;
      var actions = node.actions;
      foreach (var action in actions)
      {
        action.OnAction();
      }
    }

    public bool IsConditionsFulfilled(int index)
    {
      var choice = (NodeChoice)currentNode;
      var conditions = choice.list[index].conditions;
      if (conditions == null)
      {
        return true;
      }
      return conditions.All(t => t.IsFulfilled());
    }

    public int ChoiceSize()
    {
      var choice = (NodeChoice)currentNode;
      return choice.list.Count;
    }

    public bool IsChoiceNode()
    {
      return currentNode.type == NodeType.Choice;
    }

  }
}
