using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera flyCamera;
    public float xOffset = 6f;
    
    public void HandleDropdown(int index){
        flyCamera.transform.position = new Vector3(xOffset * index, 0, 0);
        flyCamera.transform.eulerAngles = Vector3.zero;
    }
}
