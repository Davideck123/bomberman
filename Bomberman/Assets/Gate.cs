using UnityEngine;

public class Gate : MonoBehaviour
{
    public new SpriteRenderer renderer;

    public Collider2D boxCol;
    public Collider2D circleCol;

    public Sprite gateClosed;
    public Sprite gateOpen;


    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<LevelCreator>().numberOfEnemies <= 0)
        {
            renderer.sprite = gateOpen;
            boxCol.enabled = false;
            circleCol.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Invoke("enterGate", 0.15f);
        }
    }

    void enterGate()
    {
        FindObjectOfType<PlayerMovement>().moveSpeed = 0f;
    }

}
