using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EditorNetwork : NetworkBehaviour
{
    public struct NetworkStructBlock : INetworkStruct
    {
        [Networked]
        public Vector3 position { get; set; }

        [Networked]
        public int id { get; set; }

        [Networked]
        public int nextId { get; set; }
    }

    // For convenience, declared Networked INetworkStructs can use the ref keyword,
    // which allows direct modification of members without needing to work with copies.
    [Networked]
     public ref NetworkStructBlock NetworkedStructRef => ref MakeRef<NetworkStructBlock>();

}
