using UnityEngine;





namespace FPS
{
    public class ExplosinableSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _rgd;
        [SerializeField] private AudioSource _f1;
        [SerializeField] private AudioSource _dynamit;




        public void PlayExplosionableSound(ExpoType expoType)
        {
            print("sound played");

            switch (expoType)
            {
                case ExpoType.RGD:
                    _rgd.Play();
                    break;
                case ExpoType.F1:
                    _f1.Play();
                    break;
                case ExpoType.Dynamit:
                    _dynamit.Play();
                    break;
                default:
                    break;
            }
        }
    }
}

