using Photon.Pun;
using UnityEngine;
//mini
[RequireComponent(typeof(Player))]
public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            DisableConmponents();
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;      //To not network the physics...
            AssignRemoteLayer();
        }
    }

    void DisableConmponents()
    {
        /*To disable components like PlayerMovement, AnimationController, CameraFollow, PlayerTurning, ShootingBullet, 
        ArmDirection, HeadDirection, etc, on the Remote Player.
        */
        for (int i = 0; i < componentsToDisable.Length; i++)
            componentsToDisable[i].enabled = false;
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    public override void OnDisable()
    {
        //PlayersList.UnRegisterPlayer(transform.name);
    }
}
