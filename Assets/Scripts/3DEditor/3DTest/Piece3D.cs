using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Piece3D : NetworkBehaviour
{
    [Networked(OnChanged = "OnBlockIDChanged")] public int BlockId { get; set; }
    public TestBlock currentBlock;

    public override void Spawned()
    {
        TestManager testManager = FindObjectOfType<TestManager>();
    }

    public static void OnBlockIDChanged(Changed<Piece3D> changed)
    {
        changed.Behaviour.OnBlockIDChanged();
    }

    private void OnBlockIDChanged()
    {
        TestManager testManager = FindObjectOfType<TestManager>();
    }
}
