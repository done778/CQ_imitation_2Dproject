using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillBlockController : MonoBehaviour
{
    // [SerializeField] private GameObject skillBlock;
    Color firstBlock;
    Color secondBlock;
    Color thirdBlock;

    GameObject[] blockBox;
    int[] blockChainArr;

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
        blockBox = new GameObject[buttonArr.Length]; // 버튼 오브젝트 배열
        blockChainArr = new int[buttonArr.Length]; // 체인 정보를 담을 배열
        for (int i = 0; i < buttonArr.Length; i++) 
        {
            blockBox[i] = (buttonArr[i].gameObject);
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
            if (curNumOfBlock < blockBox.Length)
            {
                BlockType blockType = (BlockType)Random.Range(1, (int)BlockType.End);

                switch (blockType)
                {
                    case BlockType.Blue:
                        blockBox[curNumOfBlock].GetComponent<Image>().color = firstBlock;
                        break;
                    case BlockType.Red:
                        blockBox[curNumOfBlock].GetComponent<Image>().color = secondBlock;
                        break;
                    case BlockType.Green:
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
        int indexChain = 0; // 체인 정보를 담을 변수
        GameObject preBlock = null; // 바로 전 블록
        GameObject curBlock = null; // 현재 블록
        for (int i = 0; i < curNumOfBlock; i++) 
        {
            curBlock = blockBox[i];
            if (preBlock == null) // 이전 블록이 없다 == 첫번째 블록
            {
                blockChainArr[i] = indexChain;
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
                indexChain++;
                numChain = 1;
            }
            blockChainArr[i] = indexChain;
            preBlock = curBlock;
        }
    }

    // 블록 제거 : 매개변수로 받은 인덱스의 블록을 제거하되, 이것과 연결된 블록도 함께 제거.
    // 제거 한 후, 뒤에 있던 블록들은 앞으로 당겨지고 이로 인해 다시 체인이 생길 수 있음.
    public int DeleteChainBlock(int index)
    {
        int numChain = 0;
        int target = blockChainArr[index];
        int overrideIndex = 0; // 블록 제거 후 뒤에 있는 블록을 당겨오기 위한 인덱스
        bool isPull = true; // 블록 제거 후 뒤에 블록을 당겨올 필요가 있는가?

        for (int i = 0; i < curNumOfBlock; i++)
        {
            if (blockChainArr[i] == target)
            {
                if (isPull)
                {
                    overrideIndex = i;
                    isPull = false;
                }
                numChain++;
            }
            if (blockChainArr[i] > target) {
                break;
            }
        }

        for (int i = overrideIndex; i < curNumOfBlock; i++)
        {
            blockBox[i].transform.GetChild(0).gameObject.SetActive(false); // 체인 이미지 비활성화
            if (i + numChain < curNumOfBlock)
                blockBox[i].GetComponent<Image>().color = blockBox[i + numChain].GetComponent<Image>().color;
            else
            {
                blockBox[i].SetActive(false);
            }
        }

        curNumOfBlock -= numChain;
        CheckBlocksChain();
        // 제거된 블록의 체인 수 반환
        return numChain;
    }
}
