using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFastVariablesView : MonoBehaviourPunCallbacks, IPunObservable
{
    public object[] localStoredVariables = { 
        0,
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        11,
        12,
        13,
        14,
        15,
        16,
    };

    /// <summary>
    /// Can only Save from 0 to 16 variables
    /// </summary>
    /// <param name="index"> the index stored that the value is stored</param>
    /// <param name="value"> the value the index is stored in</param>
    public void SetCloudVariable(int index, object value) {
        localStoredVariables.SetValue(value, index);
        if(PhotonNetwork.PhotonServerSettings.PunLogging >= (PunLogLevel)1)
            Debug.Log(transform.name + localStoredVariables.ToString());
    }

    /// <summary>
    /// Can only Load from 0 to 16 variables
    /// </summary>
    /// <param name="index"> the index stored that the value is stored</param>
    public object GetCloudVariable(int index) {
        if (localStoredVariables.GetValue(index) == null)
            return null;
        object value;
        value = localStoredVariables.GetValue(index);
        return value;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            foreach (object cloudStoredVariable in localStoredVariables) {
                stream.SendNext(cloudStoredVariable);
            }
        }
        else {
            for (int i = 0; i < localStoredVariables.Length; i++) {
                localStoredVariables.SetValue((object)stream.ReceiveNext(), i);
            }
        }
    }
}
