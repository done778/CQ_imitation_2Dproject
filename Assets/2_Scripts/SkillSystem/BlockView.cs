using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI 표시 담당
public class BlockView : MonoBehaviour
{
    BlockType blockType;
    BlockModel blockModel;
    GameObject[] blockBox;

    // 버튼 타입에 따라 색깔 부여
    Color blueBlock;
    Color redBlock;
    Color greenBlock;

    public void Init(BlockModel Model, Button[] childrenArr)
    {
        blueBlock = new Color(0.4f, 0.4f, 1f); // 청색 블록
        redBlock = new Color(1f, 0.4f, 0.4f); // 적색 블록
        greenBlock = new Color(0.4f, 1f, 0.4f); // 녹색 블록

        blockModel = Model;
        blockBox = new GameObject[childrenArr.Length];
        for (int i = 0; i < blockBox.Length; i++) 
        {
            blockBox[i] = childrenArr[i].gameObject;
        }
    }

    public void UpdateBlockUI()
    {
        for (int i = 0; i < blockBox.Length; i++) 
        {
            blockType = blockModel.blocks[i].type;

            if (blockType == BlockType.Empty)
            {
                blockBox[i].transform.GetChild(0).gameObject.SetActive(false);
                blockBox[i].SetActive(false);
                continue;
            }

            switch (blockType)
            {
                case BlockType.Blue:
                    SetColor(blockBox[i], blueBlock, blockModel.blocks[i].isChained);
                    break;
                case BlockType.Red:
                    SetColor(blockBox[i], redBlock, blockModel.blocks[i].isChained);
                    break;
                case BlockType.Green:
                    SetColor(blockBox[i], greenBlock, blockModel.blocks[i].isChained);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetColor(GameObject block, Color color, bool isChained)
    {
        block.GetComponent<Image>().color = color;
        Image chainImage = block.transform.GetChild(0).GetComponent<Image>();
        chainImage.color = color;
        block.SetActive(true);
        if (isChained)
            chainImage.gameObject.SetActive(true);
        else
            chainImage.gameObject.SetActive(false);
    }
}
