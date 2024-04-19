using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public Transform respawn;
    public float teleportDelay = 10f;
    public float Yoffset = 2f;
    public float Xoffset = 2f;

    private void Start()
    {
        StartCoroutine(TeleportCoroutine());
    }

    private IEnumerator TeleportCoroutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(teleportDelay);

            Vector3 newPosition = respawn.position;
            newPosition.y += Yoffset;
            newPosition.x += Xoffset;
            transform.position = newPosition;
        }
    }

    void Update()
    {
        if (gameObject.CompareTag("Bird"))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (gameObject.CompareTag("Ship"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
