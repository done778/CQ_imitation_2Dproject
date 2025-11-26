using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.RegistPausePanel(gameObject);
    }
}
