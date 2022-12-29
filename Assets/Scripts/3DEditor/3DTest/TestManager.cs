using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class TestManager : MonoBehaviour
{
    public GameObject plain;
    private void Start()
    {

    }

    private void Load()
    {
        //Load
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            Block3D_Data[] blockData = JsonHelper.FromJson<Block3D_Data>(saveString);
            for(int i=0;i<blockData.Length;i++)
            {
                GameObject createdObject = Instantiate(plain, blockData[i].position, Quaternion.identity);
                createdObject.GetComponent<Block3D>().id = blockData[i].id;
            }

            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");  
            for(int i=0;i<blocks.Length;i++)
            {
                for(int j=0;j<blocks.Length;j++)
                {
                    if(blocks[j].GetComponent<Block3D>().id == blockData[i].nextId)
                    {
                        blocks[i].GetComponent<Block3D>().next = blocks[j].GetComponent<Block3D>();
                    }
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
