using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePauseBehaviour : MonoBehaviour
{
    private List<PausibleObject> pausibleObjects = new List<PausibleObject>();
    [SerializeField]
    private bool isGamePaused = false;

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

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            pausibleObjects.ForEach(x => x.SetObjectPauseFlag(true));
            isGamePaused = true;
        }
    }

    public void ResumeGame()
    {
        if (isGamePaused)
        {
            pausibleObjects.ForEach(x => x.SetObjectPauseFlag(false));
        }
    }
}
