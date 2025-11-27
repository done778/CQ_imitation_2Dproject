using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockController : MonoBehaviour
{
    [SerializeField] private BlockView blockView;
    BlockModel blockModel;
    Button[] childrenArr;

    WaitForSeconds delay;
    Coroutine createBlocks;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        childrenArr = GetComponentsInChildren<Button>();
        blockModel.Init(childrenArr.Length);
        blockView.Init(blockModel, childrenArr);
        createBlocks = StartCoroutine(CreateBlock());
    }

    private void OnDestroy()
    {
        StopCoroutine(createBlocks);
    }

    void Init()
    {
        blockModel = new BlockModel();
        delay = new WaitForSeconds(1.2f);
    }

    public IEnumerator CreateBlock()
    {
        while (true)
        {
            blockModel.CreateBlock();
            blockView.UpdateBlockUI();
            yield return delay;
        }
    }

    public void CastingSkillBlock(int index)
    {
        SkillBlock block = blockModel.DeleteBlocks(index);
        blockView.UpdateBlockUI();
        // 가져온 블록을 배틀매니저에게 넘겨 스킬을 시전.
        BattleManager.Instance.CastingSkill(block);
    }
}
