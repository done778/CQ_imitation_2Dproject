using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 드래그 앤 드롭으로 파티 구성을 하기 위한 스크립트
// 1. 영웅을 끌어서 파티 칸에 넣기
// 2. 파티 칸에 있는 영웅을 바깥으로 드래그해서 빼기
// 3. 영웅을 끌어서 파티 칸에 넣었을 때, 이미 자리하고 있는 영웅이 있다면 그 애를 원래 자리로 돌려 놓기

// 위까지 구현함.

// 하지만 아래의 문제점들이 남아있음.

// 1. 드래그 가능한 이미지는 캐릭터당 하나 뿐이라
// 이미 파티 칸에 배치된 영웅을 대기 칸에서 다시 드래그 할 수 없음.
// (대기 칸에 남아 있는 이미지는 고정된 이미지.)

// 2. 파티칸 내에서 자리만 바꾸고 싶을 때, 드래그 앤 드롭하면 자리가 바뀌는게 아닌
// 원래 있던 애를 대기 칸으로 돌려놔버림.

// 1번의 경우 이미 배치된 영웅을 대기 칸에서 또 드래그할 수 있게 하고
// 파티 칸에 배치했을 때, 서로 다른 칸에 중복된 영웅이 존재하면
// 이전에 배치된 영웅을 삭제하거나 자리를 교환하는 등의 기능도 고려할 수 있음.

// 2번도 파티 구성을 하는데 있어 유저 경험에 꽤 큰 영향이 있다고 생각함.

public class MyPartyPanelDragableImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform originParent;
    [HideInInspector] public Transform curParent;
    private CanvasGroup canvasGroup;
    [SerializeField] private CharacterBaseStatus heroInfo;

    public CharacterBaseStatus HeroInfo => heroInfo;

    private void Awake()
    {
        originParent = transform.parent;
        curParent = originParent;
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 1. 최상위 계층으로 이동
        transform.SetParent(originParent.root);
        // 2. 레이캐스트 블라킹을 끔. (마우스 포인터로 날아가는 레이가 이미지를 뚫음.)
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    { 
        // 드래그 중엔 이미지가 마우스 포인터 따라가게끔.
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if(transform.parent == originParent.root)
        {
            transform.SetParent(originParent);
            transform.localPosition = Vector3.zero;
            if (curParent != originParent)
            {
                BattleManager.Instance.SetHeroEntry(curParent.gameObject.GetComponent<DropZone>().PositionIndex, null);
                curParent = originParent;
            }
        }
    }
}
