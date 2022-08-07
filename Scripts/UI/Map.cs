using UnityEngine;

public class Map : Singleton<Map>
{
    private new Renderer renderer;
    private float y;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            y += Time.deltaTime * 0.1f;
            renderer.material.SetTextureOffset("_MainTex", new(0, y));
        }
    }
}
