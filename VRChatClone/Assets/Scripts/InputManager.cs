using UnityEngine;

public class VRInputManager : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;

    void Update()
    {
        if (Input.GetAxis("LeftStickHorizontal") != 0 || Input.GetAxis("LeftStickVertical") != 0)
        {
            Vector3 move = new Vector3(Input.GetAxis("LeftStickHorizontal"), 0, Input.GetAxis("LeftStickVertical"));
            player.Translate(move * moveSpeed * Time.deltaTime);
        }
    }
}
