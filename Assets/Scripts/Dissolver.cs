using UnityEngine;

public class Dissolver : MonoBehaviour
{
    public SpriteRenderer Sprite;
    private Material DisolverMaterial;
    public Shader DisolverShader;

    private float disolving = 0;

    void Start()
    {
        DisolverMaterial = new Material(DisolverShader);
    }

    void Update()
    {
        if (disolving > 0)
        {
            DisolverMaterial.SetFloat("_Fade", disolving);
            disolving -= 0.005f;
        }
    }


    public void Dissolve()
    {
        disolving = 0.9f;
        Sprite.material = DisolverMaterial;
    }


    public void StopDissolve()
    {
        disolving = 0;
    }

}
