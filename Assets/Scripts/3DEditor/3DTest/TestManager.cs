using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TestManager : MonoBehaviour
{
    public GameObject testBlock;

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        //Load
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            LoadData(saveString);
        }
    }

    private void LoadData(string mapString)
    {
        Block3D_Data[] blockData = JsonHelper.FromJson<Block3D_Data>(mapString);
        for(int i=0;i<blockData.Length;i++)
        {
            GameObject createdObject = Instantiate(testBlock, blockData[i].position, Quaternion.identity);
            createdObject.GetComponent<TestBlock>().id = blockData[i].id;
        }

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");  
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

    [Serializable]
    private class Block3D_Data {
        public Vector3 position;
        public int id;
        public int nextId;
    }
}
