using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    private PhotonView PV;
    public Text txtNumber;
    private AudioSource mySource;
    [SerializeField]private AudioClip meow;
    int globalNumber;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        mySource = GetComponent<AudioSource>();
    }

    void Update()
    {
        txtNumber.text = "Number of times the button has been pressed: " + globalNumber;
    }

    public void IncreaseNumber()
    {
        PV.RPC("RPC_IncreaseNumber", RpcTarget.AllViaServer);
    }
    public void Meow()
    {
        PV.RPC("RPC_Meowing", RpcTarget.AllViaServer);
    }
    [PunRPC]
    void RPC_IncreaseNumber()
    {
        globalNumber++;
    }
    [PunRPC]
    void RPC_Meowing()
    {
        mySource.PlayOneShot(meow);
    }
}