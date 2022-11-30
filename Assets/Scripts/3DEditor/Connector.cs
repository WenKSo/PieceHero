using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public Block3D startBlock;
    public Block3D endBlock;

    private Vector3 mousePos;
    private Vector3 startMousePos;

    private LineRenderer lineRend;
    private MapEditor3D mapEditor;

    void Start()
    {
        mapEditor = FindObjectOfType<MapEditor3D>();
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
    }

    void Update()
    {
        if(mapEditor.currentMode != Mode.Connect) return;

        if(!startBlock) return;
        if(Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRend.SetPosition(0, startMousePos);
            lineRend.SetPosition(1, mousePos);
        }
    }
}
