using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public int cubesInRow = 5;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    private float cubeSize = 0.4f;
    private float cubesPivotDistance;
    private Vector3 cubesPivot;
    private GameObject[] cubes;

    private Rigidbody rb;
    private MeshRenderer mesh;
    private Renderer ren;

    public void explode(GameObject cube)
    {
        mesh = cube.GetComponent<MeshRenderer>();
        rb = cube.GetComponent<Rigidbody>();
        ren = cube.GetComponent<Renderer>();
        cubeSize = (float)ren.bounds.size.x/cubesInRow;
        cubesPivotDistance = (float)cubeSize*cubesInRow/2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        cube.SetActive(false);
        int counter = 0;
        cubes = new GameObject[cubesInRow * cubesInRow * cubesInRow];
        for (int i = 0; i < cubesInRow; i++)
            for (int j = 0; j < cubesInRow; j++)
                for (int k = 0; k < cubesInRow; k++, counter++)
                    createPiece(i, j, k, counter, cube);
        Destroy(cube);
        Collider[] colliders = Physics.OverlapSphere(cube.transform.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody hitRB = hit.GetComponent<Rigidbody>();
            if (hitRB != null)
                hitRB.AddExplosionForce(explosionForce, cube.transform.position, explosionRadius, explosionUpward);
        }

        foreach (GameObject piece in cubes)
        {
            StartCoroutine(fadeInAndOut(piece, false, 6.0f));
            Destroy(piece, 8.0f);
        }
    }

    private void createPiece(int x, int y, int z, int counter, GameObject cube)
    {
        GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.position = cube.transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = rb.mass / cubesInRow;
        piece.GetComponent<MeshRenderer>().material = mesh.material;
        piece.GetComponent<Renderer>().material.color = ren.material.color;
        cubes[counter] = piece;
    }

    IEnumerator fadeInAndOut(GameObject objectToFade, bool fadeIn, float duration)
    {
        float counter = 0f;
        float a, b;
        if (fadeIn)
        {
            a = 0;
            b = 1;
        }
        else
        {
            a = 1;
            b = 0;
        }

        Color currentColor = Color.clear;
        MeshRenderer tempRenderer = objectToFade.GetComponent<MeshRenderer>();

        if (tempRenderer != null)
        {
            currentColor = tempRenderer.material.color;
            tempRenderer.material.SetFloat("_Mode", 2);
            tempRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            tempRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            tempRenderer.material.SetInt("_ZWrite", 0);
            tempRenderer.material.DisableKeyword("_ALPHATEST_ON");
            tempRenderer.material.EnableKeyword("_ALPHABLEND_ON");
            tempRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            tempRenderer.material.renderQueue = 3000;
        }
        else
            yield break;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            tempRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }
}
