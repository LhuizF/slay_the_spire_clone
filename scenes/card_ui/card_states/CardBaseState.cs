using Godot;
using System;

[GlobalClass]
public partial class CardBaseState : CardState
{
  public override async void Enter()
  {

    if (!cardUI.IsNodeReady())
    {
      await cardUI.ToSignal(cardUI, "ready");
    }

    cardUI.EmitSignal(CardUI.SignalName.ReparentRequested, cardUI);
    cardUI.color.Color = Colors.WebGreen;
    cardUI.state.Text = "BASE";
    cardUI.PivotOffset = new Vector2(0, 0);
  }

  public override void OnGuiInput(InputEvent inputEvent)
  {
    if (inputEvent.IsActionPressed("left_click"))
    {
      cardUI.PivotOffset = cardUI.GetGlobalMousePosition() - cardUI.GlobalPosition;
      int clickedState = (int)State.CLICKED;
      EmitSignal(CardState.SignalName.TransitionRequested, this, clickedState);
    }
  }
}
