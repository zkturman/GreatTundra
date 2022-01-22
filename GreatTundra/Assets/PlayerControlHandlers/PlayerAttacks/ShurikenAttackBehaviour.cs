using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenAttackBehaviour : MonoBehaviour, ChargeAttack
{
    [SerializeField]
    private List<GameObject> shurikenObjects;
    [SerializeField]
    private float shurikenSpeed = 20f;
    [SerializeField]
    private float shurikenRotationSpeed = 10f;

    // Start is called before the first frame update
    void Start() {    }

    // Update is called once per frame
    void Update() {    }

    public void ConfigurePuzzleIgnoring(ShurikenPuzzleBarrierBehaviour[] shurikenPuzzles)
    {
        foreach(ShurikenPuzzleBarrierBehaviour puzzle in shurikenPuzzles)
        {
            foreach(GameObject shuriken in shurikenObjects)
            {
                Physics2D.IgnoreCollision(shuriken.GetComponent<Collider2D>(), puzzle.GetComponent<Collider2D>());
            }
        }
    }

    public void ReleaseAttack(int powerStage)
    {
        List<GameObject> shurikenToFire = detrmineShurikenToFire(powerStage);
        foreach (GameObject shuriken in shurikenToFire)
        {
            shuriken.SetActive(true);
            shuriken.GetComponent<ShurikenBehaviour>().ReleaseShuriken(shurikenSpeed, shurikenRotationSpeed);
            shuriken.transform.SetParent(null);
        }
        Destroy(this.gameObject);
    }

    private List<GameObject> detrmineShurikenToFire(int numberToActivate)
    {
        List<GameObject> allShuriken = new List<GameObject>(shurikenObjects);
        if (numberToActivate == shurikenObjects.Count)
        {
            return allShuriken;
        }

        List<GameObject> randomShurikensToFire = new List<GameObject>();
        int i = 0;
        while (i < numberToActivate)
        {
            int diceRoll = Random.Range(0, allShuriken.Count);
            randomShurikensToFire.Add(allShuriken[diceRoll]);
            allShuriken.RemoveAt(diceRoll);
            i++;
        }
        return randomShurikensToFire;
    }


}
