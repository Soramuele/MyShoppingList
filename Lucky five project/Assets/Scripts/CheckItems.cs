using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItems : MonoBehaviour
{
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.BoxCast(transform.position, new Vector3(1,1,1), transform.forward, new Quaternion(0,0,0,0), 100, layerMask))
        {
            Debug.Log("Item Found");
        }
    }
}
