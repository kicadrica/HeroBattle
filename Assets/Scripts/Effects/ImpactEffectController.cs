using UnityEngine;

public class ImpactEffectController : MonoBehaviour
{
    [SerializeField] private TypeOfPool Type;

    private void OnParticleSystemStopped()
    {
        Pool.PutToPool(Type, this);
    }
}
