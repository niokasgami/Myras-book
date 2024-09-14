using UnityEngine;
using UnityEngine.EventSystems;

namespace Hollow
{
    public class TestScriptMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // Start is called before the first frame update
        void Start()
        {

        }
        

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("it entered!");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("It exited!");
        }
    }
}
