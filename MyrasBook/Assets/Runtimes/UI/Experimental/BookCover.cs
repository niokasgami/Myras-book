using UnityEngine;
using UnityEngine.EventSystems;

namespace Hollow.UI.Experimental
{
  public class BookCover : MonoBehaviour, IDropHandler
  {

    public void OnDrop(PointerEventData eventData)
    {
      Debug.Log("OnDrop");
    }
  }
}
