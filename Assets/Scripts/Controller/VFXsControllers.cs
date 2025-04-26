using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VFXType
{
    splash, waterDrop, coins, damaged
}
public class VFXsControllers : MonoBehaviour
{
    [SerializeField] ParticleSystem particle_waterDrop;
    public static VFXsControllers Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void PlayVFX(VFXType type, Vector3 position)
    {
        particle_waterDrop.Play();
        switch (type)
        {
            case VFXType.splash:
                break;
            case VFXType.waterDrop:
                particle_waterDrop.gameObject.transform.position = position;
                particle_waterDrop.Play();
                break;
            case VFXType.coins:
                break;
            case VFXType.damaged:
                break;
        }
    }
}
