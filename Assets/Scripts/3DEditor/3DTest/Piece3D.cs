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
        currentBlock = testManager.findBlock(BlockId).GetComponent<TestBlock>();
    }

    public static void OnBlockIDChanged(Changed<Piece3D> changed)
    {
        changed.Behaviour.OnBlockIDChanged();
    }

    private void OnBlockIDChanged()
    {
        TestManager testManager = FindObjectOfType<TestManager>();
        currentBlock = testManager.blocks[BlockId].GetComponent<TestBlock>();
        updatePos();
    }

    public void updatePos()
    {
        Vector3 pos = currentBlock.transform.position;
        transform.position = pos;
    }

    public override void FixedUpdateNetwork() 
    {
    }
}
