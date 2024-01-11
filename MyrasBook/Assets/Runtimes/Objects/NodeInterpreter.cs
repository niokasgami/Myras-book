using System;
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

    private Dictionary<string, NodeBase> nodes = new Dictionary<string, NodeBase>();
    
    

    private void Start()
    {
      Database.OnDataLoaded += AssignData;
      //   nodes = Database.nodes;
      //   currentNode = FetchNode("Start");
    }

    /// <summary>
    /// Assigns the data to the nodes and current node.
    /// </summary>
    private void AssignData()
    {
      nodes = Database.nodes;
      currentNode = FetchNode("Start");
      Database.OnDataLoaded -= AssignData;
      Debug.Log(Database.localVariables[0].name);
    }
  





    /// <summary>
    /// Retrieves the text of the current node.
    /// </summary>
    /// <returns>The text of the current node.</returns>
    public string Text()
    {
      return currentNode.Text();
    }


    /// <summary>
    /// Retrieves the type of the current node.
    /// </summary>
    /// <returns>The type of the current node.</returns>
    public NodeType Type()
    {
      return currentNode.type;
    }
    
    public NodeBase FetchNode(string key)
    {
      if (!nodes.ContainsKey(key))
      {
        Debug.LogError($"The node {key} does not exist!");
      }
      return nodes[key];
    }


    /// <summary>
    /// Moves to the next node in the sequence.
    /// </summary>
    /// <param name="index">The index of the current node. Default value is -1.</param>
    public void Next(int index = -1)
    {
      var nextNodeKey = currentNode.FetchNextNode(index);
      currentNode = FetchNode(nextNodeKey);
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
  }
}
