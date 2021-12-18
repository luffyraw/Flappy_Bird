using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Dead
}
public static class GameStateManager
{
    public static GameState GameState { get; set; }
    
}
