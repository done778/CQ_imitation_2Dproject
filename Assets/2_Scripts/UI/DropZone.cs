using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            // 드롭 존에 캐릭터가 놓였을 때, 이미 누군가 자리하고 있다면, 그 애를 원래 자리로 돌려놔야함.
            var alreadyHero = transform.GetComponentInChildren<MyPartyPanelDragableImage>();
            if (alreadyHero != null) {
                alreadyHero.transform.SetParent(alreadyHero.originParent);
                alreadyHero.transform.localPosition = Vector3.zero;
            }
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
        }
    }
}
