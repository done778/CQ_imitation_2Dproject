using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] private int positionIndex;

    public int PositionIndex => positionIndex;

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

            // 현재 드롭된 오브젝트의 영웅 정보를 배틀매니저에 등록.
            var curHero = eventData.pointerDrag.gameObject.GetComponent<MyPartyPanelDragableImage>();
            if (curHero != null)
            {
                curHero.curParent = transform;
                BattleManager.Instance.SetHeroEntry(positionIndex, curHero.HeroInfo);
            }
            else
            {
                Debug.LogError("not exist Object on Drop zone");
            }
        }
    }
}
