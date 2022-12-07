using Common.Character.Scripts;
using Common.Enemy.Scripts;
using Common.Infrastructure.Factory;
using Common.Infrastructure.Services;
using Common.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
  public class LocationInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.BindInterfacesTo<LocationInstaller>().FromInstance(this).AsSingle();

      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();

      Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
      Container.BindFactory<EnemyController, EnemyController.Factory>();
      Container.Bind<SpawnService>().AsSingle().NonLazy();
      Container.Bind<UnitService>().AsSingle();
    }
  }
}