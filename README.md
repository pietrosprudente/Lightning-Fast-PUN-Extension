Easy way of using Photon PUN 
What it can do currently (Server Events (better than PunRPC), (will try to add networked variables :D))

Want to get your game listed, just drop an issue and I will manage it :D

**Games using Lightning Fast Pun:**
> *Mindless Physics*

**Code Examples:**
> *Server Events*
>> ```[ServerEvent]```
>> ```LightningFastPUN.SendServerEvent(string voidName, bool secureRpc, PhotonView view, float time, RpcTarget targets, params object[] parameters);```
> *Variable View*
>> Can only Save from 0 to 16 variables
>>> ```SetCloudVariable(int index, object value);```
>> Can only Load from 0 to 16 variables
>>> ```GetCloudVariable(int index);```
