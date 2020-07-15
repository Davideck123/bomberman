using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public new SpriteRenderer renderer;

    public Collider2D boxCol;
    public Collider2D circleCol;

    public Sprite gateClosed;
    public Sprite gateOpen;

    void Update()
    {
        ///If enemies aren't alive, open the gate

        if (FindObjectOfType<LevelCreator>().numberOfEnemies <= 0)
        {
            renderer.sprite = gateOpen;
            boxCol.enabled = false;
            circleCol.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        ///Enter gate on collision with player

        if (col.gameObject.tag.Equals("Player"))
        {
            Invoke("ChangeScene", 0.15f);
        }
    }

    void ChangeScene()
    {
        ///Change scene to a next one

        FindObjectOfType<PlayerMovement>().moveSpeed = 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
