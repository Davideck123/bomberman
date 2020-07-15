using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;

    void Update()
    {
        ///start explosion after the end of the countdown
        
        countdown -= Time.deltaTime;

        if (countdown <= 0f)
        {
            FindObjectOfType<MapDestroyer>().Explosion(transform.position);
            Destroy(gameObject);
            if (FindObjectOfType<BombSpawner>() != null)
            {
                FindObjectOfType<BombSpawner>().bombSpawned = false;
            }
        }
    }
}
