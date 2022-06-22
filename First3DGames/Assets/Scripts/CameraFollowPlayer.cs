using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public static CameraFollowPlayer instance;
    public Transform player;
    public Vector3 offset;

    public void Awake()
    {
        instance = this;
    }
    void FixedUpdate()
    {
        offset = new Vector3(1, 10, -10);
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
    }


}
