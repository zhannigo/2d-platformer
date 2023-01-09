using Common.Enemies.Scripts;
using Common.Infrastructure.Data;
using Common.Infrastructure.Factory;
using Common.Infrastructure.Services;
using Common.Infrastructure.UI.Windows;
using Zenject;

namespace Common.Infrastructure
{
  public class LocationInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<LocationInstaller>().FromInstance(this).AsSingle();
      
      Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle().NonLazy();
      Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();

      Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
      Container.Bind<LoadLevelService>().AsSingle().NonLazy();
      Container.Bind<UnitService>().AsSingle();
      Container.Bind<ITimeCounter>().To<TimeCounter>().AsSingle();
    }
  }
}