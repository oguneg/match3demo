using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;
    public void SetTargets(List<Transform> cells)
    {
        foreach(var element in cells)
        {
            targetGroup.AddMember(element, 1, 1);
        }
    }
}
