Easy way of using Photon PUN
What it can do currently (Server Events (better than PunRPC), Variable View (better than PhotonSerializeView))

**Code Examples!**
> *Server Events*
>> ```[ServerEvent]```
>> 
>> ```LightningFastPUN.SendServerEvent(string voidName, bool secureRpc, PhotonView view, float time, RpcTarget targets, params object[] parameters);```
>> 
> *Variable View*
>> Can only Save from 0 to 16 variables
>>> ```SetCloudVariable(int index, object value);```
>>> 
>> Can only Load from 0 to 16 variables
>>> ```GetCloudVariable(int index);```
>>> 
>> Can Save and Load from 0 to 16 variables
>>> ```CloudVariable(int index, [object value = null]);```
>>> 
>> **How ```CloudVariable();``` would be used!**
>> 
>>> Setting up the Cloud Variable, allocate the index and insert the value!
>>> 
>>>```variablesView.CloudVariable(0, speed);```
>>> 
>>> Getting the Cloud Variable, insert the cast + index and returns the value!
>>> 
>>>```speed = (float)variablesView.CloudVariable(0);```

**End of Code Examples!**

Want to get your game listed, just drop an issue and I will manage it :D

**Games using Lightning Fast Pun:**
> *Mindless Physics*
