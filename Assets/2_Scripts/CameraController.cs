using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        BattleManager.Instance.RegistVirtualCamera(this);
    }
    public void CameraInit(GameObject follow)
    {
        cam.Follow = follow.transform;
    }
}
