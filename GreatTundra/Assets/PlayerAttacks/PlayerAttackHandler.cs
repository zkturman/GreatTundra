using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject snowCannonball;
    [SerializeField]
    private GameObject snowShuriken;
    private float staffCooldown = 0.5f;
    private bool canStaffAttack = true;
    private float cannonCooldown = 1f;
    private bool canCannonAttack = true;
    private float shurikenCooldown = 0.75f;
    private bool canShurikenAttack = true;
    private const int LEFTCLICK = 0;
    private const int MIDDLECLICK = 2;
    private const int RIGHTCLICK = 1;
    private bool isCharging = false;

    private void Awake()
    {
        //add item booleans here
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleStaffAttack();
        handleCannonAttack();
        handleShurikenAttack();
    }

    private void handleStaffAttack()
    {
        if (isStaffAttack())
        {
            Debug.Log("Staff Attack");
            performStaffAttack();
            StartCoroutine(staffAttackAnimationRoutine());
            StartCoroutine(attackCooldownRoutine());
        }
    }

    private bool isStaffAttack()
    {
        bool validStaffInput = false;
        if (canStaffAttack)
        {
            validStaffInput = Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(LEFTCLICK);
        }
        return validStaffInput;
    }

    private void performStaffAttack()
    {
        StaffAttackHelper staffAttack = new StaffAttackHelper(this.gameObject);
        staffAttack.SwingStaff();
    }

    private void OnDrawGizmosSelected()
    {
        var playerCollider = GetComponent<Collider2D>();
        Gizmos.DrawSphere(new Vector3(playerCollider.bounds.max.x, playerCollider.bounds.center.y, 0), playerCollider.bounds.size.y / 1.5f);
    }

    private IEnumerator staffAttackAnimationRoutine()
    {
        yield return null;
    }

    private IEnumerator attackCooldownRoutine()
    {
        setAllAttackStatuses(false);
        yield return new WaitForSeconds(staffCooldown); //switch to animation if longer
        setAllAttackStatuses(true);
    }

    private void setAllAttackStatuses(bool newStatus)
    {
        canCannonAttack = newStatus;
        canShurikenAttack = newStatus;
        canStaffAttack = newStatus;
    }

    private void handleCannonAttack()
    {
        if (isCannonAttack())
        {
            Debug.Log("Cannon Attack");
            isCharging = true;
            StartCoroutine(chargePowerRoutine());
        }
    }

    private bool isCannonAttack()
    {
        bool validCannonInput = false;
        if (canCannonAttack)
        {
            validCannonInput = Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(MIDDLECLICK);
        }
        return validCannonInput;
    }

    private void handleShurikenAttack()
    {
        if (isShurikenAttack())
        {
            Debug.Log("Shuriken Attack");
        }
    }

    private bool isShurikenAttack()
    {
        bool validShurikenInput = false;
        if (canShurikenAttack)
        {
            validShurikenInput = Input.GetKeyDown(KeyCode.L) || Input.GetMouseButtonDown(RIGHTCLICK);
        }
        return validShurikenInput;
    }

    private IEnumerator chargeAttackAnimationRoutine()
    {
        yield return null;
    }

    private IEnumerator chargePowerRoutine()
    {
        float elapsedTime = 0f;
        while (isCharging && elapsedTime < 1.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            isCharging = Input.GetKey(KeyCode.K) || Input.GetMouseButtonDown(MIDDLECLICK);
        }

        int powerLevel = (int)(elapsedTime / 0.5f);
        Debug.Log(elapsedTime + ", " + powerLevel);
        if (powerLevel > 0)
        {
            GameObject cannonball = Instantiate(snowCannonball, transform.position, Quaternion.identity);
            cannonball.GetComponent<CannonAttackBehaviour>().ReleaseAttack(powerLevel);
        }
        isCharging = false;
    }
}
