using UnityEngine;
using Photon.Pun;

public class AntiCheatManager : MonoBehaviourPun
{
    void Update()
    {
        if (photonView.IsMine)
        {
            if (transform.position.y > 100) // Prevent flying
            {
                photonView.RPC("KickPlayer", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    void KickPlayer()
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Cheater kicked!");
    }
}
