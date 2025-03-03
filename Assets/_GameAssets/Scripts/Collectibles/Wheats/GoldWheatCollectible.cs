using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSo;
    [SerializeField] private PlayerController _playerController;


    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSo.IncreaseDecreaseMultiplier,_wheatDesignSo.ResetBoostDuration);
        Destroy(gameObject);
    }
}
