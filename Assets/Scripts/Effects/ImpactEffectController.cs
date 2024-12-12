using UnityEngine;

public class ImpactEffectController : MonoBehaviour
{
    [SerializeField] private TypeOfPool Type;

    private void OnParticleSystemStopped()
    {
        Pool.ReturnObject(Type, this);
    }
}
