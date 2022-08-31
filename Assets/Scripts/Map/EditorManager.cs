using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorManager : MonoBehaviour
{
    public GameObject block;
    public TMP_InputField width;
    public TMP_InputField length;

   public void CreateCanvas(){
        int w =  int.Parse(width.text);
        int l = int.Parse(length.text);
        for (int x=0; x<l; ++x){
            for (int y=0; y<w; ++y){
                Instantiate(block, new Vector3(x-5,y-3,0), Quaternion.identity);
            }
        }
   } 
}
