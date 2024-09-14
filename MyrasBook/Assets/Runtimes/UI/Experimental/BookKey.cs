using UnityEngine;
using UnityEngine.EventSystems;

namespace Hollow.UI.Experimental
{
  public class BookKey: MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler,IEndDragHandler
  {

    [SerializeField]
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
      rectTransform = GetComponent<RectTransform>();
      canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      Debug.Log("You taken the key");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      Debug.Log("OnBeginDrag");
      canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      Debug.Log("OnEndDrag");
      canvasGroup.blocksRaycasts = true;
    }
  }
}
