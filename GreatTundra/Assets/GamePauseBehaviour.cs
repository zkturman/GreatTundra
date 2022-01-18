using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePauseBehaviour : MonoBehaviour
{
    private List<PausibleObject> pausibleObjects = new List<PausibleObject>();
    [SerializeField]
    private bool pauseStatus = false;

    private void Awake()
    {
        pausibleObjects = FindObjectsOfType<MonoBehaviour>().OfType<PausibleObject>().ToList();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
