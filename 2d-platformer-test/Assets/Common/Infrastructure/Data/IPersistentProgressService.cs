namespace Common.Infrastructure.Data
{
  public interface IPersistentProgressService
  {
    PlayerProgress Progress { get; set; }
  }
}