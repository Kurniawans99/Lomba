using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class FollowCameraScript : MonoBehaviour
{
    private Vector3 _offset;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime; 
    private Vector3 currentVelocity = Vector3.zero;
    [SerializeField] public int CameraTeam;

    private void Awake()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp( transform.position, targetPosition,ref currentVelocity, smoothTime);
    }

    public void SwitchTarget(Vector3 targetPos,Transform player)
    {
        target = player;
        transform.position = new Vector3(targetPos.x , transform.position.y, targetPos.z - 30);
        _offset = transform.position - target.position;

    }
}
