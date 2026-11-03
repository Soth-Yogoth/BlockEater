using System.Collections.Generic;
using UnityEngine;

public class LevelStateHistory
{
    private Stack<LevelState> history = new Stack<LevelState>();

    public void Add(LevelState state)
    {
        Debug.Log("Add snapshot into history: " + history.Count);
        
        LevelState currentState = state.MakeCopy();
        history.Push(currentState);
    }
    
    public bool TryGet(out LevelState state)
    {
        Debug.Log("Trying to get state from history. In history: " + history.Count);
        
        if (history.Count > 1)
        {
            Debug.Log("Poped: " + (history.Count - 1));
            history.Pop();
            
            Debug.Log("Peeked: " + (history.Count - 1));
            state = history.Peek();
            
            return true;
        }

        else
        {
            state = null;
            return false;
        }
    }
}

