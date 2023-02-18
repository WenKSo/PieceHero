using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Fusion;

public class TestManager : MonoBehaviour
{
    public GameObject testBlock;
    public GameObject startBlock;
    public GameObject endBlock;

    public GameObject[] blocks;

    private void Start()
    {
        Debug.developerConsoleVisible = true;
        Load();
    }

    private void Load()
    {
        PlayerData hostData = new PlayerData();
        string map = "";
        
        //Find the Host Player Data
        PlayerData[] pds = FindObjectsOfType<PlayerData>();
        for(int i = 0; i<pds.Length;i++)
        {
            if(pds[i].Map[0]=="#0") continue;
            hostData = pds[i];
        }

        for (int i = 0; i < hostData.Map.Length; ++i)
        {
            Debug.Log(hostData.Map[i]);
            if(hostData.Map[i].Substring(0,1) == "#") break;
            map+=hostData.Map[i];
        }
        Debug.Log(map);
        LoadData(map);
    }

    private void LoadData(string mapString)
    {
        Block3D_Data[] blockData = JsonHelper.FromJson<Block3D_Data>(mapString);
        for(int i=0;i<blockData.Length;i++)
        {
            GameObject createdObject;
                switch(blockData[i].blockType)
                {
                    case BlockType.Plain:
                        createdObject = Instantiate(testBlock, blockData[i].position, Quaternion.identity);
                        break;
                    case BlockType.Start:
                        createdObject = Instantiate(startBlock, blockData[i].position, Quaternion.identity);
                        break;
                    case BlockType.Finish:
                        createdObject = Instantiate(endBlock, blockData[i].position, Quaternion.identity);
                        break;
                    default:
                        createdObject = Instantiate(testBlock, blockData[i].position, Quaternion.identity);
                        break;
                }
                createdObject.GetComponent<TestBlock>().id = blockData[i].id;
        }

        blocks = GameObject.FindGameObjectsWithTag("Block");  
        for(int i=0;i<blocks.Length;i++)
        {
            for(int j=0;j<blocks.Length;j++)
            {
                if(blocks[j].GetComponent<TestBlock>().id == blockData[i].nextId)
                {
                    blocks[i].GetComponent<TestBlock>().next = blocks[j].GetComponent<TestBlock>();
                }
            }
        }
    }

    public void onRoll()
    {
        InputVariables.instance.onClicked = !InputVariables.instance.onClicked;
    }

    public GameObject findBlock(int id)
    {
        foreach(GameObject b in blocks)
        {
            if(b.GetComponent<TestBlock>().id == id) return b;
        }
        return null;
    }

    [Serializable]
    private class Block3D_Data {
        public Vector3 position;
        public int id;
        public int nextId;
        public BlockType blockType;
    }
}
