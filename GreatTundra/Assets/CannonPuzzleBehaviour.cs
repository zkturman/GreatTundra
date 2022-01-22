using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPuzzleBehaviour : GamePuzzleBehaviour
{
    private bool isSolutionTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSolutionTriggered && !IsSolved)
        {
            SolvePuzzle();
        }
    }

    public override void SolvePuzzle()
    {
        Debug.Log("Solved Cannon puzzle.");
        base.SolvePuzzle();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CannonAttackBehaviour>(out CannonAttackBehaviour cannon))
        {
            isSolutionTriggered = true;
        }
    }
}
