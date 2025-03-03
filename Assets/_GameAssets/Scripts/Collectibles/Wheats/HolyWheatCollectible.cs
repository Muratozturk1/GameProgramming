using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSo;
    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        _playerController.SetJumpForce(_wheatDesignSo.IncreaseDecreaseMultiplier, _wheatDesignSo.ResetBoostDuration);
        Destroy(gameObject);
    }
}
