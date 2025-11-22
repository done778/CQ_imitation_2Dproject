using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockType
{
    First, Second, Third, End
}
public class SkillBlockController : MonoBehaviour
{
    // [SerializeField] private GameObject skillBlock;
    Color firstBlock;
    Color secondBlock;
    Color thirdBlock;

    List<GameObject> blockBox;

    int curNumOfBlock;

    Coroutine createBlocks;
    WaitForSeconds delay;

    private void Start()
    {
        Init();
        createBlocks = StartCoroutine(CreateBlock());
    }

    private void Init()
    {
        delay = new WaitForSeconds(1.5f);

        firstBlock = new Color(0.4f, 0.4f, 1f); // 청색 블록
        secondBlock = new Color(1f, 0.4f, 0.4f); // 적색 블록
        thirdBlock = new Color(0.4f, 1f, 0.4f); // 녹색 블록

        Button[] buttonArr = GetComponentsInChildren<Button>();
        Debug.Log($"{buttonArr.Length}");
        blockBox = new List<GameObject>(buttonArr.Length); // 배열을 리스트로 생성
        for (int i = 0; i < buttonArr.Length; i++) 
        {
            blockBox.Add(buttonArr[i].gameObject);
            blockBox[i].transform.GetChild(0).gameObject.SetActive(false);
            blockBox[i].SetActive(false);
        }
        curNumOfBlock = 0;
    }

    private void OnDestroy()
    {
        StopCoroutine(createBlocks);
    }

    // 블록 생성
    public IEnumerator CreateBlock()
    {
        while (true) 
        {
            if (curNumOfBlock < blockBox.Count)
            {
                 BlockType blockType = (BlockType)Random.Range(0, (int)BlockType.End);

                switch (blockType)
                {
                    case BlockType.First:
                        blockBox[curNumOfBlock].GetComponent<Image>().color = firstBlock;
                        break;
                    case BlockType.Second:
                        blockBox[curNumOfBlock].GetComponent<Image>().color = secondBlock;
                        break;
                    case BlockType.Third:
                        blockBox[curNumOfBlock].GetComponent<Image>().color = thirdBlock;
                        break;
                    default:
                        break;
                }
                blockBox[curNumOfBlock].SetActive(true);
                curNumOfBlock++;
                CheckBlocksChain();
            }

            yield return delay;
        }
    }

    // 블록 체인 검사
    public void CheckBlocksChain()
    {
        int numChain = 1; // 최대 3체인까지 체크를 위한 변수
        GameObject preBlock = null; // 바로 전 블록
        GameObject curBlock = null; // 현재 블록
        for (int i = 0; i < curNumOfBlock; i++) 
        {
            curBlock = blockBox[i];
            if (preBlock == null) // 이전 블록이 없다 == 첫번째 블록
            {
                preBlock = curBlock;
                continue;
            }

            Color curBlockColor = curBlock.GetComponent<Image>().color;

            // 색깔로 비교해서 이전 블록과 같으면서 체인이 3미만이라면 참
            if (curBlockColor == preBlock.GetComponent<Image>().color && numChain < 3)
            {
                Image chainImage = curBlock.transform.GetChild(0).GetComponent<Image>();
                chainImage.color = curBlockColor;
                chainImage.gameObject.SetActive(true);
                numChain++;
            }
            else
            {
                numChain = 1;
            }

            preBlock = curBlock;
        }
    }
}
