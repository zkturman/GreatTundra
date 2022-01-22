using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAttackBehaviour : MonoBehaviour, ChargeAttack
{
    [SerializeField]
    private Rigidbody2D cannonRigidBody;
    [SerializeField]
    private float cannonSpeed = 10f;
    [SerializeField]
    private string[] layerNamesToIgnore;
    private LayerMask layersToIgnore;

    private void Awake()
    {
        layersToIgnore = LayerMask.GetMask(layerNamesToIgnore);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectiles"), LayerMask.NameToLayer("Projectiles"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectiles"), LayerMask.NameToLayer("Player"));
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ReleaseAttack(int powerStage)
    {
        transform.localScale = transform.localScale * powerStage;
        cannonRigidBody.velocity = PlayerStats.GetProjectileDirection() * cannonSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layersToIgnore != (layersToIgnore | 1 << collision.gameObject.layer))
        {
            StartCoroutine(snowballExplode());
        }
    }

    private IEnumerator snowballExplode()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
