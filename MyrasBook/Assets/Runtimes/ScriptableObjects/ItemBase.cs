using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Hollow.ScriptableObjects
{
  [CreateAssetMenu(menuName = "hollow/create new item", fileName = "new Item")]
  public class ItemBase : ScriptableObject
  {
    [PropertySpace(SpaceAfter =  7)]
    [HorizontalGroup("Split",55,LabelWidth = 100)]
    [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
    public Image image;
    
    [PropertySpace(SpaceAfter =  7)]
    [VerticalGroup("Split/Meta")]
    [InlineButton("OnNameChanged","rename")]
    public string key;
    

    [VerticalGroup("Split/Meta")]
    public string displayName;
    

    [BoxGroup("Description")]
    [HideLabel, TextArea(4, 14)]
    public string description;

    private void OnNameChanged()
    {
      var path = AssetDatabase.GetAssetPath(this);
      AssetDatabase.RenameAsset(path, key);
    }
  }
}
