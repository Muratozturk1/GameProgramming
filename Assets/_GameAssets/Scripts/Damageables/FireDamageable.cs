using UnityEngine;

public class FireDame : MonoBehaviour, IDamageable
{
    [SerializeField] private float _force = 10f;
    public void GiveDamage(Rigidbody playerRigidbody, Transform playerViusalTransform)
    {
        HealthManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerViusalTransform.forward * _force, ForceMode.Impulse);
        Destroy(gameObject);
    }
}
