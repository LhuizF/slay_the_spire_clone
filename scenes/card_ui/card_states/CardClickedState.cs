using Godot;
using System;

public partial class CardClickedState : CardState
{
  public override void Enter()
  {
    cardUI.color.Color = Colors.Orange;
    cardUI.state.Text = "CLICKED";
    cardUI.DropPointDetector.Monitorable = true;
  }

  public override void OnInput(InputEvent inputEvent){
    if(inputEvent is InputEventMouseMotion){
      int draggingState = (int) State.DRAGGING;
      EmitSignal(CardState.SignalName.TransitionRequested, this, draggingState);
    }
  }
}
