using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    private int spriteIndex;

    private Vector3 direction;

    public float gravity = -9.8f;

    public float strength = 5f;

    public float tilt = 5f;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            direction = Vector3.up * strength;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            direction = Vector3.down * strength;
        }

        transform.position += direction * Time.deltaTime;

        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite() {
        spriteIndex++;
        if(spriteIndex > sprites.Length) {
            spriteIndex = 0;
        }
        if (spriteIndex < sprites.Length && spriteIndex >= 0) {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }
}
