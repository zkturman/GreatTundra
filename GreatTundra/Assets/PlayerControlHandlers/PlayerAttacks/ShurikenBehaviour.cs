using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenBehaviour : MonoBehaviour
{
    [SerializeField]
    private string[] layerNamesToIgnore;
    private LayerMask layersToIgnore;
    private Rigidbody2D shurikenRigidBody;
    private void Awake()
    {
        layersToIgnore = LayerMask.GetMask(layerNamesToIgnore);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectiles"), LayerMask.NameToLayer("Projectiles"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectiles"), LayerMask.NameToLayer("Player"));
        shurikenRigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layersToIgnore != (layersToIgnore | 1 << collision.gameObject.layer))
        {
            shurikenRigidBody.freezeRotation = true;
            shurikenRigidBody.velocity = Vector2.zero;
            GameObject gameObject = new GameObject("ShurikenParent");
            transform.SetParent(gameObject.transform);
            gameObject.transform.SetParent(collision.gameObject.transform);
            StartCoroutine(shurikenStick());
        }
    }

    public void ReleaseShuriken(float speed, float rotationalSpeed)
    {
        shurikenRigidBody.velocity = PlayerStats.GetProjectileDirection() * speed;
        shurikenRigidBody.angularVelocity = rotationalSpeed;
    }

    private IEnumerator shurikenStick()
    {
        yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
    }
}
