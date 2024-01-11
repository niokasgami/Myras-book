using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Hollow
{
    public enum NodeType
    {
        Linear,
        Action,
        Choice,
    }
    public abstract class NodeBase : ScriptableObject
    {
        
        [Title("Metadata")]
        [BoxGroup("Metadata", showLabel: false)]
        [LabelWidth(70)]
        public string key;
        
        [BoxGroup("Metadata", showLabel: false)]
        [DisplayAsString]
        [LabelText("Node type")]
        [LabelWidth(70)]
        public NodeType type;
        
        [BoxGroup("Metadata", showLabel: false)]
        [LabelWidth(70)]
        public Sprite image;
        [LabelWidth(70)]
        [BoxGroup("Metadata", showLabel: false)]
        public string location;
        
        [BoxGroup("Metadata", showLabel: false)]
        [BoxGroup("Metadata", showLabel: false)]
        [LabelWidth(70)]
        public string previous;
        
        private void OnNameChange()
        {
            var path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.RenameAsset(path, key);
        }

        public abstract string Text();


        public abstract string FetchNextNode(int index);
    }
    

}
