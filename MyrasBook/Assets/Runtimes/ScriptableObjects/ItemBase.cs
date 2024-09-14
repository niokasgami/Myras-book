using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


namespace Hollow.ScriptableObjects
{
  [CreateAssetMenu(menuName = "hollow/create new item", fileName = "new Item")]
  public class ItemBase : ObjectBase
  {
    [PropertySpace(SpaceAfter =  7)]
    [HorizontalGroup("Split",70,LabelWidth = 100)]
    [HideLabel, PreviewField(60, ObjectFieldAlignment.Left)]
    public Sprite image;
    
    [BoxGroup("Metadata")]
    [LabelWidth(100)]
    public string displayName;
    

    [HorizontalGroup("Split")]
    [HideLabel, TextArea(4, 14), LabelWidth(100)]
    public string description;

    #if UNITY_EDITOR
    private void OnNameChanged()
    {
      var path = AssetDatabase.GetAssetPath(this);
      AssetDatabase.RenameAsset(path, key);
    }
    #endif
  }
}
