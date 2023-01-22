using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EditorNetwork : NetworkBehaviour
{
     [Networked]
     [Capacity(10)] // Sets the fixed capacity of the collection
     NetworkArray<NetworkString<_256>> SaveNetArray { get; } =
     MakeInitializer(new NetworkString<_256>[] { "#0", "#1", "#2", "#3" });
    
    public override void FixedUpdateNetwork()
    {
        
    }

}
