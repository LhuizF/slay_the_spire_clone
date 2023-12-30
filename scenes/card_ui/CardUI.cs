using Godot;
using System;
using System.Collections.Generic;

public partial class CardUI : Control
{
  public ColorRect color = null;
  public Label state = null;
  public Area2D DropPointDetector = null;

  public CardStateMachine cardStateMachine = null;
  public List<Node> targetNodes = new List<Node>();
  public override void _Ready()
  {
    color = GetNode<ColorRect>("Color");
    state = GetNode<Label>("State");
    cardStateMachine = GetNode<CardStateMachine>("CardStateMachine");
    DropPointDetector = GetNode<Area2D>("DropPointDetector");

    cardStateMachine.Init(this);
  }

  [Signal]
  public delegate void ReparentRequestedEventHandler(CardUI whichCardUI);

  public override void _Input(InputEvent inputEvent)
  {
    cardStateMachine.OnInput(inputEvent);
  }

  public override void _GuiInput(InputEvent inputEvent)
  {
    cardStateMachine.OnGuiInput(inputEvent);
  }

  public void OnMouseEntered()
  {
    cardStateMachine.OnMouseEntered();
  }

  public void OnMouseExited()
  {
    cardStateMachine.OnMouseExited();
  }

  public void OnDropPointDetectorMouseEntered(Area2D area)
  {
    if (!targetNodes.Contains(area))
    {
      targetNodes.Add(area);
    }
  }

  public void OnDropPointDetectorMouseExited(Area2D area)
  {
    if (targetNodes.Contains(area))
    {
      targetNodes.Remove(area);
    }
  }
}
