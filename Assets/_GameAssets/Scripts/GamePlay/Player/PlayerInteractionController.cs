using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
            Debug.Log("Gold Wheat Collected");
        }
        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            Debug.Log("Holy Wheat Collected");
        }
        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
            Debug.Log("Rotten Wheat Collected");
        }
    }
}
