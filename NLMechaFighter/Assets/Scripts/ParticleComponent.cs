using System.Collections;
using UnityEngine;

public class ParticleComponent : MonoBehaviour
{
    public void PlayParticle(GameObject particlePrefab)
    {
        GameObject emitterInstance = Instantiate(particlePrefab);
        ParticleSystem particleSystem = emitterInstance.GetComponent<ParticleSystem>();
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        shapeModule.position = transform.position;

        StartCoroutine("PlayParticleCoroutine", particleSystem);
    }

    public void PlayBoxParticle(GameObject particlePrefab)
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

    public void PlayLoopingParticle(GameObject particlePrefab)
    {
        GameObject emitterInstance = Instantiate(particlePrefab, transform);
        ParticleSystem particleSystem = emitterInstance.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule mainModule = particleSystem.main;
        ParticleSystem.ShapeModule shapeModule = particleSystem.shape;
        mainModule.loop = true;

        particleSystem.Play();
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
