using Godot;
using System;


public enum State {
  BASE,
  CLICKED,
  DRAGGING,
  AIMING,
  RELEASED,
}

[GlobalClass]
public partial class CardState : Node
{
  public CardUI cardUI = null;
  [Export]
  public State state = State.BASE;

  [Signal]
  public delegate void TransitionRequestedEventHandler(CardState from, State to);
  public virtual void Enter(){}
  public virtual void Exit(){}
  public virtual void OnInput(InputEvent inputEvent){}
  public virtual void OnGuiInput(InputEvent inputEvent){}
  public virtual void OnMouseEntered(){}
  public virtual void OnMouseExited(){}
}

