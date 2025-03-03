using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesignSo;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStateUI _playerStateUI;

    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage =_playerBoosterTransform.GetComponent<Image>();
    }


    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSo.IncreaseDecreaseMultiplier,_wheatDesignSo.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform,_playerBoosterImage,_playerStateUI.GetRottenBoosterWheatImage,_wheatDesignSo.ActiveSprite,_wheatDesignSo.PassiveSprite,_wheatDesignSo.ActiveWheatSprite,_wheatDesignSo.PassiveWheatSprite,_wheatDesignSo.ResetBoostDuration);

        Destroy(gameObject);
    }
   
}
