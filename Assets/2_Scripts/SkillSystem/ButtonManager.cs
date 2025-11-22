using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public SkillBlockController blockController;
    // 스킬 블록 버튼 눌렸을 때 매니저에게 알림

    public void OnBlockButtonClick(int index)
    {
        Debug.Log($"{index}번 블록 눌림.");
        blockController.DeleteChainBlock(index);
    }
}
