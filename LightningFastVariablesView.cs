using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningFastVariablesView : MonoBehaviourPunCallbacks, IPunObservable {
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

    /// <summary>
    /// Can only Save from 0 to 16 variables
    /// </summary>
    /// <param name="index"> the index stored that the value is stored</param>
    /// <param name="value"> the value the index is stored in</param>
    public void SetCloudVariable(int index, object value) {
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
                localStoredVariablesList = localStoredVariables.ToList();
                if (localStoredVariables[i] == null) {
                    localStoredVariablesList.Remove(i);
                    localStoredVariables = localStoredVariablesList.ToArray();
                }
            }
        }
    }
}
