using Godot;
using System;

public partial class CardReleasedState : CardState
{
  bool played;
  public override void Enter()
  {
    cardUI.color.Color = Colors.DarkViolet;
    cardUI.state.Text = "RELEASED";

    played = false;
    if (cardUI.targetNodes.Count > 0)
    {
      played = true;
    }
  }

  public override void OnInput(InputEvent inputEvent)
  {
    if (played) return;

    int state = (int)State.BASE;
    EmitSignal(CardState.SignalName.TransitionRequested, this, state);
  }
}
