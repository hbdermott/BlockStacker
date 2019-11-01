using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cube : MonoBehaviour
{
    public bool collided = false;
    public bool sticky = false;
    public bool heavy = false;


    public int cubesInRow = 5;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    private float cubeSize = 0.4f;
    private float cubesPivotDistance;
    private Vector3 cubesPivot;
    private GameObject[] cubes;

    private Renderer ren;
    private MeshRenderer mesh;
    private Scene gameController;
    private Rigidbody rb;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ren = GetComponent<Renderer>();
        mesh = GetComponent<MeshRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Scene>();
        cubeSize = (float)ren.bounds.size.x/cubesInRow;
        cubesPivotDistance = (float)cubeSize*cubesInRow/2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

    }

    void OnCollisionEnter(Collision col)
    {
        if (sticky)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = col.rigidbody; 
        }
        collided = true;
    }
    

    public void explode()
    {
        int counter = 0;
        gameObject.SetActive(false);
        cubes = new GameObject[cubesInRow * cubesInRow * cubesInRow];
        for (int i = 0; i < cubesInRow; i++)
            for (int j = 0; j < cubesInRow; j++)
                for (int k = 0; k < cubesInRow; k++, counter++)
                    createPiece(i, j, k, counter);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider hit in colliders)
        {
            Rigidbody hitRB = hit.GetComponent<Rigidbody>();
            if(hitRB != null)
                hitRB.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
        }
        DestroyCube();
        foreach(GameObject piece in cubes)
            Destroy(piece, 3);

    }

    private void createPiece(int x, int y, int z, int counter)
    {
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = rb.mass / cubesInRow;
        piece.GetComponent<MeshRenderer>().material = mesh.material;
        piece.GetComponent<Renderer>().material.color = ren.material.color;
        cubes[counter] = piece;
    }

    public void DestroyCube()
    {
        Destroy(gameObject);
        gameController.cubesDestroyed++;
    }


    void Update()
    {
        if (Camera.main.ViewportToWorldPoint(transform.position).y < -5)
            DestroyCube();
    }
}
