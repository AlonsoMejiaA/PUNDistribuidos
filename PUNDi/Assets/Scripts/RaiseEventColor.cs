using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEventColor : MonoBehaviourPun
{
    private Renderer playerRenderer;

    private const byte COLOR_CHANGE_EVENT = 0;
    private void Awake()
    {

        playerRenderer = this.transform.GetChild(0).GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventRecieved;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventRecieved;
    }

    private void NetworkingClient_EventRecieved(EventData obj)
    {
        if(obj.Code == COLOR_CHANGE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            if (this.photonView.ViewID == (int)datas[0])
            {
                float r = (float)datas[1];
                float g = (float)datas[2];
                float b = (float)datas[3];
                playerRenderer.material.color = new Color(r, g, b, 1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (base.photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }
    private void ChangeColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        playerRenderer.material.color = new Color(r, g, b, 1f);


        object[] datas = new object[] {base.photonView.ViewID, r, g, b };

        PhotonNetwork.RaiseEvent(COLOR_CHANGE_EVENT, datas, Photon.Realtime.RaiseEventOptions.Default,ExitGames.Client.Photon.SendOptions.SendUnreliable);
    }
}
