using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockController : MonoBehaviour
{
    [SerializeField] private BlockView blockView;
    BlockModel blockModel;
    
    WaitForSeconds delay;
    Coroutine createBlocks;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Button[] childrenArr = GetComponentsInChildren<Button>();
        Debug.Log($"{childrenArr.Length}");
        blockModel.Init(childrenArr.Length);
        blockView.Init(blockModel, childrenArr);
        createBlocks = StartCoroutine(CreateBlock());
    }

    void Init()
    {
        blockModel = new BlockModel();
        delay = new WaitForSeconds(1.5f);
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
    }
}
