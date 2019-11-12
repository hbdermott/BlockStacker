using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    public float speed = 0.0f;
    private Vector3 move;
    public bool paused;
    private GameController controller;
    private List<int> playerCol = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            move = new Vector3(speed + controller.points/30f, 0, 0);
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x > Screen.width / 2)
                    rb.velocity += move;
                else
                    rb.velocity -= move;
            }
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = Mathf.Clamp01(pos.x);
            if (pos.x == 0)
                rb.velocity = move;
            else if (pos.x == 1)
                rb.velocity = -move;
        }

    }

    void OnCollisionEnter(Collision col)
    {
        int id = col.gameObject.GetInstanceID();
        if (playerCol.Contains(id))
            return;
        playerCol.Add(id);
        gameObject.AddComponent<FixedJoint>().connectedBody = col.rigidbody;
    }
}
