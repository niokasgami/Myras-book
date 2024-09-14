using System;
using System.Collections.Generic;
using System.Linq;
using Hollow.Editor.HollowStoryEditor.Nodes;
using Hollow.ScriptableObjects.Nodes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Hollow.Editor.GraphEditor
{

  public class DialogueGraphView : GraphView
  {
    private readonly Vector2 defaultNodeSize = new Vector2(150, 200);
    private Dictionary<Guid, NodeBase> nodeCollection;

    public DialogueGraphView(Dictionary<Guid, NodeBase> collections)
    {

      BuildGraphStyle();
      nodeCollection = collections;
      AddElement(GenerateIntroNode());
      GenerateNodes();
      ConnectNodes();
    }

    
    private void BuildGraphStyle()
    {
      styleSheets.Add(Resources.Load<StyleSheet>("EditorGraphStyle"));
      SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
      this.AddManipulator(new ContentDragger());
      this.AddManipulator(new SelectionDragger());
      this.AddManipulator(new RectangleSelector());

      var grid = new GridBackground();
      Insert(0, grid);
    }
    private DialogBase GenerateIntroNode()
    {
      if (nodeCollection.All(node => node.Value.type != NodeType.Intro))
      {
        EditorUtility.DisplayDialog("No Intro Node",
          "There is no node of type Intro.", "OK");
        return null;
      }

      var node = nodeCollection.Values.FirstOrDefault(node => node.type == NodeType.Intro);
      var dialog = new DialogIntro(node);
      dialog.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
      return dialog;
    }

    private void GenerateNodes()
    {
      foreach (var node in nodeCollection.Values)
      {
        if (node.type == NodeType.Intro)
        {
          continue;
        }

        if (node.type == NodeType.Linear)
        {
          var linearNode = new DialogLinear(node);
          linearNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
          AddElement(linearNode);
        }

        if (node.type == NodeType.Action)
        {
          var actionNode = new DialogAction(node);
          actionNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
          AddElement(actionNode);
        }

        if (node.type == NodeType.Branching)
        {
          var branchingNode = new DialogueBranching(node);
          branchingNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
          AddElement(branchingNode);
        }
        {
          
        }
      }
    }

    private void ConnectNodes()
    {
      foreach (var node in nodeCollection)
      {
        if (node.Value.FetchNextNode(-1) == "")
        {
          continue;
        }

        switch (node.Value.type)
        {
          case NodeType.Linear:
            ConnectLinearNode(node.Value);
            break;
          case NodeType.Intro:
            // in this case we can use the linear Node process.
            ConnectLinearNode(node.Value);
            break;
          case NodeType.Action:
            // again in this case we can use the linear Node process
            ConnectLinearNode(node.Value);
            break;
          case NodeType.Choice:
            // todo
            break;
          case NodeType.Branching:
            ConnectBranchingNode(node.Value);
            break;
          case NodeType.AdvancedBranching:
            break;
          case NodeType.Safe:
            break;
          case NodeType.Dialogue:
            break;
          case NodeType.Puzzle:
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private void ConnectLinearNode(NodeBase node)
    {
      var guid = GetGuidByKey(node.key);
      var dataA = nodeCollection[guid];
      var graphNodeA = GetGraphNodeByGuid(guid);

      var guidB = GetGuidByKey(dataA.FetchNextNode(-1));
      var graphNodeB = GetGraphNodeByGuid(guidB);

      var edge = graphNodeA.output[0].ConnectTo(graphNodeB.input);
      AddElement(edge);
    }

    private void ConnectBranchingNode(NodeBase node)
    {
      var guid = GetGuidByKey(node.key);
      var mainNode = (NodeBranching)nodeCollection[guid];
      var parentGraphNode = GetGraphNodeByGuid(guid);

     
      var guidFirstBranch = GetGuidByKey(mainNode.mainNode);
      var firstBranch = GetGraphNodeByGuid(guidFirstBranch);

      var guidSecondBranch = GetGuidByKey(mainNode.secondNode);
      var secondBranch = GetGraphNodeByGuid(guidSecondBranch);

      var mainEdge = parentGraphNode.output[0].ConnectTo(firstBranch.input);
      var secondEdge = parentGraphNode.output[1].ConnectTo(secondBranch.input);
      AddElement(mainEdge);
      AddElement(secondEdge);
    }
    
    private List<DialogBase> GraphChildren()
    {
      var list = new List<DialogBase>();
      foreach (var element in graphElements)
      {
        if (element is DialogBase)
        {
          list.Add(element as DialogBase);
        }
      }

      return list;
    }

    private DialogBase GetGraphNodeByGuid(Guid guid)
    {
      return GraphChildren().FirstOrDefault(node => node.guid == guid);
    }
    private Guid GetGuidByKey(string key)
    {
      foreach (var node in nodeCollection)
      {
        if (node.Value.key == key)
        {
          return node.Value.Guid;
        }
      }

      throw new Exception("invalid key!");
    }


    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
      base.BuildContextualMenu(evt);
      if (evt.target is GraphView)
      {
        evt.menu.AppendAction("Testing", (e) => { Debug.Log("Ping!"); });
      }
    }


    public DialogueNodeS CreateDialogueNode(string nodeName)
    {
      var dialogueNode = new DialogueNodeS
      {
        title = nodeName,
        DialogueText = nodeName,
        GUID = Guid.NewGuid().ToString()
      };
      var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
      inputPort.portName = "Input";
      dialogueNode.inputContainer.Add(inputPort);
      dialogueNode.RefreshExpandedState();
      dialogueNode.RefreshPorts();
      dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
      return dialogueNode;
    }

    private Port GeneratePort(DialogueNodeS node, Direction portDirection,
      Port.Capacity capacity = Port.Capacity.Single)
    {
      return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
      var compatiblePorts = new List<Port>();
      foreach (var port in ports)
      {
        if (startPort != port && startPort.node != port.node)
        {
          compatiblePorts.Add(port);
        }
      }

      return compatiblePorts;
    }
  }
}