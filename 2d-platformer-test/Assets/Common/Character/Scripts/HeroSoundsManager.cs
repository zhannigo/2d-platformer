using UnityEngine;

namespace Common.Character.Scripts
{
  public class HeroSoundsManager : MonoBehaviour
  {
    [SerializeField] private AudioSource _attackSound;
    [SerializeField] private AudioSource _stepsSound;

    public void PlayAttack() => 
      _attackSound.Play();
    
    public void PlaySteps(float velocity)
    {
      if (Mathf.Abs(velocity) > 0.05)
      {
        if (!_stepsSound.isPlaying)
        {
          _stepsSound.Play();
        }
      }
      else
      {
        _stepsSound.Stop();
      }
    }
  }
}