using Godot;
using System;
using System.Collections.Generic;
public partial class CardStateMachine : Node
{
  [Export]
  private CardState initialState = null;

  private CardState currentState = null;

  private Dictionary<State, CardState> states = new Dictionary<State, CardState>();

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    states = new Dictionary<State, CardState>();
  }

  public void Init(CardUI card)
  {
    foreach (CardState child in GetChildren())
    {
      if (child is CardState)
      {
        states.Add(child.state, child);

        child.Connect(CardState.SignalName.TransitionRequested, Callable.From((CardState from, State to) => {
          OnTransitionRequested(from, to);
        }));
        child.cardUI = card;
      }
    }

    if (initialState != null)
    {
      initialState.Enter();
      currentState = initialState;
    }

  }

  public void OnInput(InputEvent inputEvent)
  {
    currentState?.OnInput(inputEvent);
  }

  public void OnGuiInput(InputEvent inputEvent)
  {
    currentState?.OnGuiInput(inputEvent);
  }

  public void OnMouseEntered()
  {
    currentState?.OnMouseEntered();
  }

  public void OnMouseExited()
  {
    currentState?.OnMouseExited();
  }

  private void OnTransitionRequested(CardState from, State to)
  {
    if (from != currentState)
    {
      return;
    }

    CardState newState = states[to];

    if (newState == null)
    {
      return;
    }

    currentState?.Exit();

    newState.Enter();
    currentState = newState;

  }
}
