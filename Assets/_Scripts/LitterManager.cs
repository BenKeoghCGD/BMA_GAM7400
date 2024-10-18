using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to handle litter spawned in the world. Separate manager for extended functionality, mainly for AI implementation. (BH)
public class LitterManager
{
    private HashSet<Litter> _worldLitter;
    public LitterManager()
    {
        _worldLitter = new HashSet<Litter>();
    }

    public HashSet<Litter> GetWorldLitter()
    {
        return _worldLitter;
    }
    public int Count()
    {
        if(_worldLitter == null)
        {
            return 0;
        }

        return _worldLitter.Count;
    }
    public void ClearLitter()
    {
        if(_worldLitter == null || _worldLitter.Count == 0)
        {
            return;
        }

        foreach(Litter litter in _worldLitter)
        {
            Object.Destroy(litter.gameObject);
        }

        _worldLitter.Clear();
    }
    public void AddLitter(Litter litter)
    {
        if (litter == null)
        {
            Debug.LogError("Tried to add null litter object to LitterManager.");
            return;
        }

        _worldLitter.Add(litter);
    }
    public void RemoveLitter(Litter litter)
    {
        if(_worldLitter.Contains(litter) == false)
        {
            Debug.LogError("Tried to remove unmanaged litter from LitterManager.");
            return;
        }

        _worldLitter.Remove(litter);
    }
}
