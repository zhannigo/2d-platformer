using Common.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      if (Application.isEditor)
      {
        Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
      }
      else
      {
        Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
      }
    }
  }
}
