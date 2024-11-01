using System.Collections;
using UnityEngine;

public class ParticleComponent : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;

    public void PlayParticle()
    {
        GameObject emitterInstance = Instantiate(particlePrefab);
        ParticleSystem particleSystem = emitterInstance.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.position = transform.position;

        StartCoroutine("PlayParticleCoroutine", particleSystem);
    }

    public void PlayBoxParticle()
    {
        GameObject emitterInstance = Instantiate(particlePrefab);
        ParticleSystem particleSystem = emitterInstance.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.shapeType = ParticleSystemShapeType.Box;

        BoxCollider box = GetComponent<BoxCollider>();
        Bounds boxBounds = box.bounds;

        ExtendToBoxCollider(shapeModule);

        StartCoroutine("PlayParticleCoroutine", particleSystem);
    }

    private IEnumerator PlayParticleCoroutine(ParticleSystem particleSystem)
    {
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.startLifetimeMultiplier);
        Destroy(particleSystem.gameObject);
        yield return null;
    }

    private bool ExtendToBoxCollider(ParticleSystem.ShapeModule shapeMod)
    {
        if (shapeMod.shapeType != ParticleSystemShapeType.Box) return false;

        BoxCollider box = GetComponent<BoxCollider>();
        Bounds boxBounds = box.bounds;

        shapeMod.position = boxBounds.center;
        shapeMod.scale = boxBounds.size;
        shapeMod.randomDirectionAmount = 1f;

        return true;
    }
}
