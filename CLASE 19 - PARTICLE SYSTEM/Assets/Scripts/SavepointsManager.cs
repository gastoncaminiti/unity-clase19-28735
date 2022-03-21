using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavepointsManager : MonoBehaviour
{
    [SerializeField] List<Transform> Savepoint = new List<Transform>();
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Transform savepoint in transform){
            //Debug.Log(savepoint.name);
            Savepoint.Add(savepoint);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindSavePoint(string name){
        int indexPoint = Savepoint.FindIndex(item => item.name == name);
        GameManager.instance.lastSP = indexPoint;
    }

    public Transform GetSavePoint(int index){
        return Savepoint[index];
    }
}
