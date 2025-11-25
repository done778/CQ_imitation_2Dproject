using System.Collections.Generic;
using UnityEngine;

public class SkillBlock
{
    public BlockType type;
    public bool isChained; // 앞 블록과 연결 여부
    public int numChain; // 이 블록이 현재 몇 체인인가?

    public SkillBlock(BlockType type = BlockType.Empty, bool isChained = false, int numChain = 1)
    {
        this.type = type;
        this.isChained = isChained;
        this.numChain = numChain;
    }
}
public enum BlockType
{
    Empty, Blue, Red, Green, End
}

// 처음에 스킬 블록을 담는 자료구조를 배열로 선택. 스킬 블록은 구조체로 정의
// 크기가 변할 필요가 없고 블록을 활성/비활성으로 관리할 것이기 때문.
// 하지만 체인 검사에서 항상 처음 블록부터 검사하는 문제와
// 중간 블록 삭제 시 뒤에 있는걸 땡겨오는 과정에서 코드 복잡성이 증가하는 문제가 있음.

// 블록이 중간에서 자주 제거된다는 특징 때문에 링크드리스트로 변경하고 스킬 블록을 구조체에서 클래스로 변경

// 그리고 블록 생성과 제거 후 체인 검사를 모든 블록을 검사하는 같은 로직을 쓰는데
// 블록 생성 후 체인 검사는 생성된 블록만 검사하고
// 블록 제거 후 체인 검사는 제거된 블록 양 옆만 검사하도록 로직을 분리하여 최적화


#region 배열을 사용한 BlockModel

//public class BlockModel
//{
//    public SkillBlock[] blocks;
//    public int CurNumOfBlock { get; private set; }

//    public void Init(int size)
//    {
//        blocks = new SkillBlock[size]; // 스킬 블록 배열
//        for (int i = 0; i < size; i++)
//        {
//            blocks[i].type = BlockType.Empty;
//            blocks[i].isChained = false;
//            blocks[i].numChain = 0;
//        }
//        CurNumOfBlock = 0;
//    }

//    public void CreateBlock()
//    {
//        if (CurNumOfBlock < blocks.Length)
//        {
//            blocks[CurNumOfBlock].type = (BlockType)Random.Range(1, (int)BlockType.End);
//            CurNumOfBlock++;
//            CheckChainBlocks();
//        }
//    }

//    private void CheckChainBlocks()
//    {
//        // if (CurNumOfBlock <= 0) return; // 블록이 없으면 검사할 필요 없음.

//        blocks[0].isChained = false; // 첫번째 블록은 무조건 false

//        int numChain = 1; // 최대 3체인까지 체크를 위한 변수
//        for (int i = 1; i < CurNumOfBlock; i++)
//        {
//            // 타입을 비교해서 이전 블록과 같으면서 체인이 3미만이라면 참
//            if (blocks[i].type == blocks[i - 1].type && numChain < 3)
//            {
//                blocks[i].isChained = true;
//                numChain++;
//            }
//            else
//            {
//                blocks[i].isChained = false;
//                numChain = 1;
//            }
//        }
//    }
//    // 매개변수로 받은 index 위치의 블록과 연결된 블록 모두 제거하고 뒤에 있는걸 당김.
//    public SkillBlock DeleteBlocks(int index)
//    {
//        SkillBlock chainSkill = new SkillBlock();

//        chainSkill.type = blocks[index].type;
//        int numChain = 0;
//        int overrideIndex = 0; // 블록 제거 후 뒤에 있는 블록을 당겨오기 위한 인덱스

//        while (blocks[index].isChained)
//        {
//            index--;
//        }

//        overrideIndex = index;
//        numChain++;
//        index++;

//        while (blocks[index].isChained && index < blocks.Length)
//        {
//            numChain++;
//            index++;
//        }

//        // 뒤에 있는 애 땡기고 체인도 일단 해제함.
//        for (int i = overrideIndex; i < CurNumOfBlock; i++)
//        {
//            if (i + numChain < CurNumOfBlock)
//            {
//                blocks[i].type = blocks[i + numChain].type;
//                blocks[i].isChained = false;
//            }
//            else
//            {
//                blocks[i].type = BlockType.Empty;
//                blocks[i].isChained = false;
//            }
//        }

//        CurNumOfBlock -= numChain;

//        // 변경된 블록 배열에 다시 체인 검사까지 실행
//        CheckChainBlocks();

//        // 제거된 블록의 체인 수 할당
//        chainSkill.numChain = numChain;

//        return chainSkill;
//    }
//}

#endregion


#region 링크드 리스트를 사용한 BlockModel

public class BlockModel
{
    public LinkedList<SkillBlock> blocksLL;
    public int boxSize;

    public void Init(int size)
    {
        blocksLL = new LinkedList<SkillBlock>();
        boxSize = size;
    }

    public void CreateBlock()
    {
        if (blocksLL.Count < boxSize)
        {
            var curBlock = blocksLL.AddLast(new SkillBlock((BlockType)Random.Range(1, (int)BlockType.End)));
            if (blocksLL.Count > 1)
            {
                var preBlock = curBlock.Previous;
                if (curBlock.Value.type == preBlock.Value.type && preBlock.Value.numChain < 3)
                {
                    curBlock.Value.isChained = true;
                    curBlock.Value.numChain = preBlock.Value.numChain + 1;
                }
            }
        }
    }

    // 매개변수로 받은 index 위치의 블록과 연결된 블록 모두 제거하고 뒤에 있는걸 당김.
    public SkillBlock DeleteBlocks(int index)
    {
        SkillBlock resultSkillBlock = new SkillBlock();
        var curBlock = blocksLL.First;
        int chain = 1;

        // 링크드 리스트에 인덱스식 접근이 마음에 안드는데..
        // 인덱스가 가리킨 노드로 간다.
        for (int i = 0; i < index; i++)
            curBlock = curBlock.Next;

        // 반환할 블록의 타입 정보를 담는다.
        resultSkillBlock.type = curBlock.Value.type;

        // 가리킨 노드가 앞 노드와 연결된 상태면 이전으로 이동한다.
        // 이렇게 하면 연결된 블록들 중 첫번째 블록을 가리키게 된다.
        while (curBlock.Value.isChained == true)
            curBlock = curBlock.Previous;

        // 현재 가리킨 블록은 어차피 지워져야 하는데 연결된 블록도 지워져야 하므로
        // 다음 블록이 있는지 확인하고
        // 있다면 연결되었는지도 확인하고 지운 후 최종적으로 현재 블록도 지운다.
        if (curBlock.Next != null)
        {
            while (curBlock.Next != null && curBlock.Next.Value.isChained == true)
            {
                blocksLL.Remove(curBlock.Next);
                chain++;
            }
            if (curBlock.Next != null)
            {
                var checkBlock = curBlock.Next;
                blocksLL.Remove(curBlock);

                if (checkBlock.Previous != null)
                {
                    // 블록 삭제 후 체인 체크
                    // 중간 블록이 사라짐으로서 새로 연결된 두 블럭과 그 뒤 블록을 검사해야한다.
                    // 색깔이 다르거나 마지막 블록이라면 탈출.
                    while (checkBlock.Value.type == checkBlock.Previous.Value.type)
                    {
                        if (checkBlock.Previous.Value.numChain < 3)
                        {
                            checkBlock.Value.isChained = true;
                            checkBlock.Value.numChain = checkBlock.Previous.Value.numChain + 1;
                        }
                        else
                        {
                            checkBlock.Value.isChained = false;
                            checkBlock.Value.numChain = 1;
                        }
                        if (checkBlock.Next != null)
                            checkBlock = checkBlock.Next;
                        else
                            break;
                    }
                }
            }
            else
            {
                blocksLL.Remove(curBlock);
            }
        }
        else
        {
            blocksLL.Remove(curBlock);
        }

        resultSkillBlock.numChain = chain;

        return resultSkillBlock;
    }
}

#endregion
