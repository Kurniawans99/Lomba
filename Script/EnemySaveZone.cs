using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySaveZone : MonoBehaviour
{
    [SerializeField] private LayerMask zoneMask;
    public Collider colliderBox;
    public void ResetTime(ILastTimeOut lastTimeOut)
    {
        lastTimeOut.TimeOut = 5;
    }

    private void Update()
    {
        Vector3 boxSize = colliderBox.bounds.size;
        Collider[] colliders = Physics.OverlapBox(colliderBox.transform.position, boxSize / 2, Quaternion.identity);
        foreach (Collider hitCollider in colliders)
        {
            if (hitCollider.gameObject != this.gameObject && hitCollider.gameObject.layer == zoneMask)
            {
                GetTimeComponent(hitCollider);
            }
        }

    }

    private void GetTimeComponent(Collider hitCollider)
    {
        bool getlastTime = hitCollider.transform.parent.TryGetComponent(out ILastTimeOut lastTimeOut);

        if (getlastTime)
        {
            Debug.Log(getlastTime);

            ResetTime(lastTimeOut);
        }
    }
}
