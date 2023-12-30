using Godot;
using System;

public partial class Hand : HBoxContainer
{
  public override void _Ready()
  {
    foreach (CardUI cardUI in GetChildren())
    {
      cardUI.Connect(CardUI.SignalName.ReparentRequested, Callable.From((CardUI currentCardUI) =>
      {
        OnCardUiReparentRequested(currentCardUI);
      }));
    }
  }

  public void OnCardUiReparentRequested(CardUI cardUI)
  {
    cardUI.Reparent(this);
  }
}
