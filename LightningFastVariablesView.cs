using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningFastVariablesView : MonoBehaviour, IPunObservable {

    private PhotonView photonView;

    private object[] localStoredVariables = {
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

    private List<object> localStoredVariablesList;

    void Start() { 
        photonView = GetComponent<PhotonView>();
    }

    /// <summary>
    /// Can only Save from 0 to 16 variables!
    /// </summary>
    /// <param name="index"> the index stored that the value is stored</param>
    /// <param name="value"> the value the index is stored in</param>
    public void SetCloudVariable(int index, object value) {
        if(value == localStoredVariables.GetValue(index)) return;
        localStoredVariables.SetValue(value, index);
        localStoredVariablesList = localStoredVariables.ToList();
        for (int i = 0; i < localStoredVariables.Length; i++) {
            if (localStoredVariables[i] == null) {
                localStoredVariablesList.Remove(i);
                localStoredVariables = localStoredVariablesList.ToArray();
            }
        }
        if (PhotonNetwork.PhotonServerSettings.PunLogging >= (PunLogLevel)1)
            Debug.Log(transform.name + localStoredVariables.ToString());
    }

    /// <summary>
    /// Can only Load from 0 to 16 variables!
    /// </summary>
    /// <param name="index"> the index stored that the value is stored</param>
    public object GetCloudVariable(int index) {
        if (photonView.IsMine)
            return localStoredVariables.GetValue(index);
        else {
            if (localStoredVariables.GetValue(index) == null)
                return null;
            return localStoredVariables.GetValue(index);
        }        
    }

    /// <summary>
    /// Can Save and Load from 0 to 16 variables!
    /// </summary>
    /// <param name="index"> the index stored that the value is stored</param>
    /// <param name="value"> the value the index is stored in</param>
    public object CloudVariable(int index, object value = null) {
        if (photonView.IsMine) {
            if (value != null && value != GetCloudVariable(index)) {
                SetCloudVariable(index, value);
            }
            return localStoredVariables.GetValue(index);
        }
        else if (GetCloudVariable(index) != null){
            return GetCloudVariable(index);
        }      
        return null;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.IsWriting) {
            foreach (object cloudStoredVariable in localStoredVariables) {
                stream.SendNext(cloudStoredVariable);
            }
        }
        else {
            for (int i = 0; i < localStoredVariables.Length; i++) {
                localStoredVariables.SetValue(stream.ReceiveNext(), i);
                localStoredVariablesList = localStoredVariables.ToList();
                if (localStoredVariables[i] == null) {
                    localStoredVariablesList.Remove(i);
                    localStoredVariables = localStoredVariablesList.ToArray();
                }
            }
        }
    }

    private void PrintConsole(object message) {
        if (PhotonNetwork.PhotonServerSettings.PunLogging >= (PunLogLevel)1) {
            Debug.Log(message);
        }
    }
}
