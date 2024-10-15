using System.Collections;
using UnityEngine;

public class GeneralBulletScript : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
