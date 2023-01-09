using Common.Infrastructure.UI.Elements;

namespace Common.Character.Scripts
{
  public class HeroUI
  {
    private HpBar _hpBar;

    public HeroUI(HpBar hpBar) => 
      _hpBar = hpBar;
    
    public void UpdateBar(float current, float max) => 
      _hpBar.SetValue(current, max);
    
  }
}