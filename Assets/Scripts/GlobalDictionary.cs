using System.Collections.Generic;

public class GlobalDictionary : MySingleton<GlobalDictionary> {

    private Dictionary<string, System.Object> messageDict = new Dictionary<string, object>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set(string key, System.Object message)
    {
        if (messageDict.ContainsKey(key))
        {
            messageDict[key] = message;
            return;
        }
        messageDict.Add(key, message);
    }

    public System.Object Get(string key)
    {
        if (messageDict.ContainsKey(key))
            return messageDict[key];
        return null;
    }
}
