using Godot;
using System;
using System.Reflection.Metadata;

public partial class CardDraggingState : CardState
{
  const float dragMinimumThreshold = 0.05f;
  bool minimumDragTimeElapsed = false;


  public override void Enter()
  {
    Node uiLayer = GetTree().GetFirstNodeInGroup("ui_layer");

    if (uiLayer != null)
    {
      cardUI.Reparent(uiLayer);
    }

    cardUI.color.Color = Colors.NavyBlue;
    cardUI.state.Text = "DRAGGING";

    minimumDragTimeElapsed = false;
    var timer = GetTree().CreateTimer(dragMinimumThreshold, false);
    timer.Connect("timeout", Callable.From(() => minimumDragTimeElapsed = true));
  }

  public override void OnInput(InputEvent inputEvent)
  {
    bool mouseMotion = inputEvent is InputEventMouseMotion;
    bool cancel = inputEvent.IsActionPressed("right_mouse");
    bool confirm = inputEvent.IsActionReleased("left_mouse") || inputEvent.IsActionPressed("left_mouse");

    if (mouseMotion)
    {
      cardUI.GlobalPosition = cardUI.GetGlobalMousePosition() - cardUI.PivotOffset;
    }

    if (cancel)
    {
      int baseState = (int)State.BASE;
      EmitSignal(CardState.SignalName.TransitionRequested, this, baseState);
    }
    else if (confirm && minimumDragTimeElapsed)
    {
      GetViewport().SetInputAsHandled();
      int releasedState = (int)State.RELEASED;
      EmitSignal(CardState.SignalName.TransitionRequested, this, releasedState);
    }
  }
}
