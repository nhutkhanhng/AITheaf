using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mNetworkTransform : MonoBehaviour, kINetworkUpdate
{
    public virtual void FixedUpdateNetwork()
    {
        
    }

    public virtual void kRender()
    {
        
    }

    public virtual void _LateUpdate()
    {

    }

    public void LateUpdate()
    {
        _LateUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
