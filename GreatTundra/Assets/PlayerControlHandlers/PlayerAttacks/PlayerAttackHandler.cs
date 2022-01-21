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
    private (KeyCode key, int mouseButton) staffAttackCommand = (KeyCode.J, LEFTCLICK);
    private const int MIDDLECLICK = 2;
    private (KeyCode key, int mouseButton) cannonAttackCommand = (KeyCode.I, MIDDLECLICK);
    private const int RIGHTCLICK = 1;
    private (KeyCode key, int mouseButton) shurikenAttackCommand = (KeyCode.L, RIGHTCLICK);
    private bool isCharging = false;
    private int chargeAttackPowerLevel;

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
            validStaffInput = Input.GetKeyDown(staffAttackCommand.key) 
                || Input.GetMouseButtonDown(staffAttackCommand.mouseButton);
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
            isCharging = true;
            StartCoroutine(fireSnowCannonball(0.5f));
        }
    }

    private bool isCannonAttack()
    {
        bool validCannonInput = false;
        if (canCannonAttack)
        {
            validCannonInput = Input.GetKeyDown(cannonAttackCommand.key) 
                || Input.GetMouseButtonDown(cannonAttackCommand.mouseButton);
        }
        return validCannonInput;
    }

    private IEnumerator fireSnowCannonball(float levelIntervalInSeconds)
    {
        yield return chargePowerRoutine(cannonAttackCommand, levelIntervalInSeconds);
        if (chargeAttackPowerLevel > 0)
        {
            GameObject cannonball = Instantiate(snowCannonball, transform.position, Quaternion.identity);
            cannonball.GetComponent<CannonAttackBehaviour>().ReleaseAttack(chargeAttackPowerLevel);
        }
        chargeAttackPowerLevel = 0;
        isCharging = false;
    }

    private void handleShurikenAttack()
    {
        if (isShurikenAttack())
        {
            isCharging = true;
            StartCoroutine(fireSnowShuriken(0.3f));
        }
    }

    private bool isShurikenAttack()
    {
        bool validShurikenInput = false;
        if (canShurikenAttack)
        {
            validShurikenInput = Input.GetKeyDown(shurikenAttackCommand.key) 
                || Input.GetMouseButtonDown(shurikenAttackCommand.mouseButton);
        }
        return validShurikenInput;
    }

    private IEnumerator fireSnowShuriken(float levelIntervalInSeconds)
    {
        yield return chargePowerRoutine(shurikenAttackCommand, levelIntervalInSeconds);
        Debug.Log(chargeAttackPowerLevel);
        if (chargeAttackPowerLevel > 0)
        {
            Collider2D playerCollider = GetComponent<Collider2D>();
            Vector3 shurikenOrigin = (playerCollider.bounds.size.x * PlayerStats.GetProjectileDirection()) + playerCollider.bounds.center;
            GameObject shuriken = Instantiate(snowShuriken, shurikenOrigin, Quaternion.identity);
            shuriken.GetComponent<ShurikenAttackBehaviour>().ReleaseAttack(chargeAttackPowerLevel);
        }
        chargeAttackPowerLevel = 0;
        isCharging = false;
    }

    private IEnumerator chargeAttackAnimationRoutine()
    {
        yield return null;
    }

    private IEnumerator chargePowerRoutine((KeyCode key, int mouseButton) attackCommand, float levelIntervalInSeconds)
    {
        float currentChargingTime = 0f;
        float maxTime = levelIntervalInSeconds * 3;
        chargeAttackPowerLevel = 0;
        while (isCharging && currentChargingTime < maxTime)
        {
            currentChargingTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            isCharging = Input.GetKey(attackCommand.key) || Input.GetMouseButtonDown(attackCommand.mouseButton);
        }
        chargeAttackPowerLevel = (int)(currentChargingTime / levelIntervalInSeconds);
    }
}
