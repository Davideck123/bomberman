using UnityEngine;

public class Explosion : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1.25f);
    }
}
