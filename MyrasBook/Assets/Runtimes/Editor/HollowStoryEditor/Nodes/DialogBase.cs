using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using Hollow.ScriptableObjects.Nodes;
using UnityEditor;
using UnityEngine;

namespace Hollow.Editor.HollowStoryEditor.Nodes
{
  public abstract class DialogBase : Node
  {
    public static Action<Guid> OnNodeSelected;
    public Guid guid;
    public NodeType type;
    public Port input;
    public List<Port> output;

    protected DialogBase(NodeBase nodeData)
    {
      guid = nodeData.Guid;
      type = nodeData.type;
      title = nodeData.key;
      output = new List<Port>();
      GeneratePorts();
    }

    public override void OnSelected()
    {
      OnNodeSelected?.Invoke(guid);
    }

    public void Refresh()
    {
      RefreshExpandedState();
      RefreshPorts();
    }
    
    
    protected Port GeneratePort(Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
      return InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }


    protected void GeneratePorts()
    {
      GenerateInputPort();
      GenerateOutputPort();
      Refresh();

    }

    protected virtual void GenerateInputPort()
    {
      input = GeneratePort(Direction.Input, Port.Capacity.Multi);
      input.portName = "Input";
      inputContainer.Add(input);
    }

    protected virtual void GenerateOutputPort()
    {
      var generations = GeneratePort(Direction.Output);
      output.Add(generations);
      output[0].portName = "Next";
      outputContainer.Add(output[0]);
      
    }
  }
}