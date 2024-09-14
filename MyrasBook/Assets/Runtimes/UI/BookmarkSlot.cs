using Hollow.Management;
using Hollow.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Hollow.UI
{
  public class BookmarkSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
  {

    [SerializeField]
    private int slot;
    [SerializeField]
    private Image sprite;
    [SerializeField]
    private TextMeshProUGUI locationText;
    private NodeInterpreter interpreter;
    private bool isWritten = false;
   // private bool isEnabled;
   
    private void Start()
    {
      interpreter = GameObject.Find("GameManager").GetComponent<NodeInterpreter>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if(!isWritten) return;
      locationText.text = BuildLocationStr();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      locationText.text = "";
    }

    private string BuildLocationStr()
    {
      var node = Player.Instance.GetBookmark(this.slot);
      var location = interpreter.FetchNode(node).location;
      return "If I remember this will bring me back to the " + location + "..." ;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (!isWritten)
      {
        var page = interpreter.currentNode.key;
        Player.Instance.WriteBookmark(page,slot);
        sprite.color = Color.blue;
        isWritten = true;
      }
      else
      {
        var bookmark = Player.Instance.GetBookmark(slot);
        interpreter.Jump(bookmark);
        isWritten = false;
        sprite.color = Color.white;
        Player.Instance.DestroyBookmark(slot);
      }
      GameManager.Instance.RequestGuiRefresh();
    }
  }
}
