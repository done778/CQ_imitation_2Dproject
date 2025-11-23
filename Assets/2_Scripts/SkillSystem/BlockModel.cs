using UnityEngine;

public struct SkillBlock
{
    public BlockType type;
    public bool isChained; // 앞 블록과 연결 여부
    public int numChain; // 이 블록이 현재 몇 체인인가?
}
public enum BlockType
{
    Empty, Blue, Red, Green, End
}

public class BlockModel
{
    public SkillBlock[] blocks; 
    public int CurNumOfBlock { get; private set; }

    public void Init(int size)
    {
        blocks = new SkillBlock[size]; // 스킬 블록 배열
        for (int i = 0; i < size; i++)
        {
            blocks[i].type = BlockType.Empty;
            blocks[i].isChained = false;
            blocks[i].numChain = 0;
        }
        CurNumOfBlock = 0;
    }

    public void CreateBlock()
    {
        if (CurNumOfBlock < blocks.Length)
        {
            blocks[CurNumOfBlock].type = (BlockType)Random.Range(1, (int)BlockType.End);
            CurNumOfBlock++;
            CheckChainBlocks();
        }
    }

    private void CheckChainBlocks()
    {
        // if (CurNumOfBlock <= 0) return; // 블록이 없으면 검사할 필요 없음.

        blocks[0].isChained = false; // 첫번째 블록은 무조건 false

        int numChain = 1; // 최대 3체인까지 체크를 위한 변수
        for (int i = 1; i < CurNumOfBlock; i++)
        {
            // 타입을 비교해서 이전 블록과 같으면서 체인이 3미만이라면 참
            if (blocks[i].type == blocks[i-1].type && numChain < 3)
            {
                blocks[i].isChained = true;
                numChain++;
            }
            else
            {
                blocks[i].isChained = false;
                numChain = 1;
            }
        }
    }

    // 매개변수로 받은 index 위치의 블록과 연결된 블록 모두 제거하고 뒤에 있는걸 당김.
    public SkillBlock DeleteBlocks(int index)
    {
        SkillBlock chainSkill = new SkillBlock();
        chainSkill.type = blocks[index].type;
        int numChain = 0;
        int overrideIndex = 0; // 블록 제거 후 뒤에 있는 블록을 당겨오기 위한 인덱스

        while (blocks[index].isChained)
        {
            index--;
        }

        overrideIndex = index;
        numChain++;
        index++;

        while (blocks[index].isChained && index < blocks.Length)
        {
            numChain++;
            index++;
        }

        // 뒤에 있는 애 땡기고 체인도 일단 해제함.
        for (int i = overrideIndex; i < CurNumOfBlock; i++)
        {
            if (i + numChain < CurNumOfBlock)
            {
                blocks[i].type = blocks[i + numChain].type;
                blocks[i].isChained = false;
            }
            else
            {
                blocks[i].type = BlockType.Empty;
                blocks[i].isChained = false;
            }
        }

        CurNumOfBlock -= numChain;

        // 변경된 블록 배열에 다시 체인 검사까지 실행
        CheckChainBlocks();

        // 제거된 블록의 체인 수 할당
        chainSkill.numChain = numChain;

        return chainSkill;
    }
}
