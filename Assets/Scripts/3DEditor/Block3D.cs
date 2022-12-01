using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block3D : MonoBehaviour
{
    public Block3D next;
    private Vector3 mOffset;
    private float mZCoord;

    void OnMouseDown()
    {
        Debug.Log(MapEditor3D.Instance==null);
        if(MapEditor3D.Instance.currentMode != Mode.Move) return;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //Store offset = gameObject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        // pixel coordinates (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game objecy on Screen
        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if(MapEditor3D.Instance.currentMode != Mode.Move) return;
        transform.position = GetMouseWorldPos() + mOffset;
    }

}
