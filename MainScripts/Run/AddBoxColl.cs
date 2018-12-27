using UnityEngine;
using UnityEditor;

public class AddBoxColl : MonoBehaviour
{
    BoxCollider2D textBox;

    Renderer textRenderer;
    Vector2 center;

    public void Start()
    {
        textBox = GetComponent<BoxCollider2D>();
        textRenderer = GetComponent<Renderer>();
        center = transform.TransformPoint(textBox.offset);
    }

    public void Update()
    {
        textBox.size = textRenderer.bounds.size;
        textBox.offset = transform.InverseTransformPoint(textRenderer.bounds.center);
    }

}