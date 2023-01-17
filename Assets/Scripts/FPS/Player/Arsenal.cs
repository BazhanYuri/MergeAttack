using UnityEngine;
using DG.Tweening;


namespace FPS
{
    public enum WeaponType
    {
        Weapon,
        Explosinable,
        None
    }
    public class Arsenal : MonoBehaviour
    {
        [SerializeField] private MergeInfoContainer _mergeInfoContainer;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private Transform _explosibleRoot;


        private WeaponType _currentWeaponType = WeaponType.Weapon;

        public WeaponType CurrentWeaponType { get => _currentWeaponType;}



        private void Start()
        {
            GameManager.Instance.GameplayStarted += ChooseWeapon;
        }
        private void OnDisable()
        {
            GameManager.Instance.GameplayStarted -= ChooseWeapon;
        }


        public void ChooseGranade()
        {
            _currentWeaponType = WeaponType.None;
            ShowArcenal(_explosibleRoot, _weaponRoot, WeaponType.Explosinable);
            _explosibleRoot.GetComponentInChildren<Explosinable>().SetUpExplo(_mergeInfoContainer.ChoosedExplosivnesIndex);
        }
        public void ChooseWeapon()
        {
            _currentWeaponType = WeaponType.None;
            ShowArcenal(_weaponRoot, _explosibleRoot, WeaponType.Weapon);
        }
        private void ShowArcenal(Transform showed, Transform hidden, WeaponType choosedType)
        {
            showed.gameObject.SetActive(true);
            showed.localPosition = new Vector3(0, -1, 0);
            hidden.localPosition = new Vector3(0, 0, 0);
            Sequence sequence = DOTween.Sequence();

            sequence.Append(hidden.DOLocalMoveY(-1, 0.3f).OnComplete(() => hidden.gameObject.SetActive(true))); ;
            sequence.Append(showed.DOLocalMoveY(0, 0.3f).OnComplete(() => _currentWeaponType = choosedType));
        }
    }
}
