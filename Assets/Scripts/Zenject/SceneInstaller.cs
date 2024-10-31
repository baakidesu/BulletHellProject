using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
  public override void InstallBindings()
  {
      Container.Bind<PlayerMovement>().FromComponentInHierarchy().AsSingle();
      Container.Bind<MapController>().FromComponentInHierarchy().AsSingle();
  }
  
}
