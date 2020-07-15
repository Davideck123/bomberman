using UnityEngine;

public class Explosion : MonoBehaviour
{
    public new Collider2D collider;

    float collisionTime = 0.4f;

    // Update is called once per frame
    void Update()
    {
        collisionTime -= Time.deltaTime;
        if(collisionTime <= 0f)
        {
            collider.enabled = false;
        }
        Destroy(gameObject, 0.9f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy")) {
            col.gameObject.GetComponent<EnemyAI>().animator.SetBool("Death", true);
            col.gameObject.GetComponent<EnemyAI>().moveSpeed = 0f;
            col.gameObject.GetComponent<EnemyAI>().collider.enabled = false;
            Destroy(col.gameObject, 2f);
            FindObjectOfType<LevelCreator>().numberOfEnemies--;
        }
        if (col.gameObject.tag.Equals("Player"))
        {
            col.gameObject.GetComponent<PlayerMovement>().animator.SetBool("Death", true);
            col.gameObject.GetComponent<PlayerMovement>().moveSpeed = 0f;
            for (int i = 0; i < 3; i++)
            {
                col.gameObject.GetComponent<PlayerMovement>().colliders[i].enabled = false;
            }
            Destroy(col.gameObject, 2f);
            FindObjectOfType<GameMenu>().canvas.enabled = true;
        }
    }
}
