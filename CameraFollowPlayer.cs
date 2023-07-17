using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [Range(0, 1)]
    public float smoothSpeed;
    private Vector3Int offset = new Vector3Int(1, 1, 0);
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
    }

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 desiredPosition = player.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);


            smoothedPosition.z = -10;

            transform.position = smoothedPosition;
        }
    }
}
