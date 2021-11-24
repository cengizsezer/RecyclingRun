using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera Camera;
    public GameObject FixedPoint;
    public GameObject CapturedObject;
    public GameObject Obstacles;


    public int CekmeKuvveti;
   
    public Vector3 NesneyiDondur;
    public bool NesneAktif;

    private void Start()
    {
        Camera = Camera.main;
        Obstacles = GameObject.FindWithTag("Obstacles");

      
        NesneyiDondur = Vector3.zero;
        NesneAktif = false;
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !NesneAktif)
        {
            NesneyiBul();
        }

        if (Input.GetMouseButtonUp(0) && NesneAktif)
        {
            NesneyiBirak();
        }

        NesneyiCek();

    }


    void NesneyiBul()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 200.0f))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red, 0.1f);
            if (hit.collider.CompareTag("Obstacles"))
            {
                CapturedObject = hit.collider.gameObject;
                var CapturedRb = CapturedObject.GetComponent<Rigidbody>();
                CapturedObject.transform.parent = FixedPoint.transform;
                CapturedObject.transform.LookAt(FixedPoint.transform.position);
                CapturedRb.constraints = RigidbodyConstraints.FreezeAll;
                NesneAktif = true;
            }
        }
    }

    void NesneyiCek()
    {
        if (NesneAktif)
        {
            CapturedObject.transform.position = Vector3.Lerp(CapturedObject.transform.position,
                FixedPoint.transform.position, CekmeKuvveti * Time.deltaTime);
            float xEkseni = Random.Range(-1.5f, 1.5f);
            float yEkseni = Random.Range(-1.5f, 1.5f);
            float zEkseni = Random.Range(-1.5f, 1.5f);

            NesneyiDondur = new Vector3(xEkseni, yEkseni, zEkseni);

            CapturedObject.transform.Rotate(NesneyiDondur);
        }
    }

    void NesneyiBirak()
    {
        var CapturedRb = CapturedObject.GetComponent<Rigidbody>();
        CapturedRb.transform.parent = null;
        CapturedRb.constraints = RigidbodyConstraints.None;
        NesneAktif = false;
    }

   
}
