using Sirenix.OdinInspector;
using UnityEngine;

namespace Hollow.ScriptableObjects.Nodes
{
    public enum NodeType
    {
        Intro,
        Linear,
        Action,
        Choice,
        Branching,
        AdvancedBranching,
        Safe,
        Dialogue,
        Puzzle,
    }

    public abstract class NodeBase : ObjectBase
    {


        [BoxGroup("Metadata", showLabel: false)] [DisplayAsString] [LabelText("Node type")] [LabelWidth(70)]
        public NodeType type;

        [BoxGroup("Metadata", showLabel: false)] [LabelWidth(70)]
        public Sprite image;

        [LabelWidth(70)] [BoxGroup("Metadata", showLabel: false)]
        public string location;

        [BoxGroup("Metadata", showLabel: false)] [BoxGroup("Metadata", showLabel: false)] [LabelWidth(70)]
        public string previous;


        public abstract string Text();


        public abstract string FetchNextNode(int index);

    }
}
