using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Photon.Pun {

    public class ServerEvent : PunRPC {
    }

    public class LightningFastPUN : MonoBehaviour {
        // RPC
        static PhotonView selectedPhotonView;

        public static async Task
            PerformRpcDelay(float delay) {
            await Task.Delay(Mathf.RoundToInt(delay) * 1000);
        }

        public static async void SendServerEvent(string voidName, bool secureRpc, PhotonView view, float time, RpcTarget targets, params object[] parameters) {
            Debug.DebugBreak();
            PhotonNetwork.RunRpcCoroutines = true;
            await PerformRpcDelay(time);
            LightningFastPUN lightningFastPUN = new LightningFastPUN();
            lightningFastPUN.ServerSendEventWait(voidName, secureRpc, view, targets, parameters);
        }

        private void ServerSendEventWait(string voidName, bool secureRpc, PhotonView view, RpcTarget targets, params object[] parameters) {
            selectedPhotonView = view;
            if (secureRpc) {
                selectedPhotonView.RpcSecure(voidName, targets, true, parameters);
            }
            else {
                selectedPhotonView.RPC(voidName, targets, parameters);
            }
        }
    }
}
