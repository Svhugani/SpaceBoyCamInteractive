using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace HomeomorphicGames
{
    public class PostProcessManager : AbstractManager
    {
        [SerializeField] private VolumeProfile volumeProfile;
        [SerializeField] private AnimationCurve responseCurve = AnimationCurve.EaseInOut(0, 0, 1, 0);
        [SerializeField] private float escapeFieldAnimationDuration = 1.5f;

        private LensDistortion _lensDistortion;
        private Coroutine _escapeFieldRoutine;
        public LensDistortion lensDistortion { get { if (_lensDistortion == null) volumeProfile.TryGet(out _lensDistortion); return _lensDistortion; } }

        public override async Task Prepare()
        {
            SetLensCenter(new Vector2(.5f, .5f));
            await Task.Yield();
        }
        private void SetLensDistortion(float value)
        {
            lensDistortion.intensity.Override(value);
        }

        public void SetLensCenter(Vector2 center)
        {
            lensDistortion.center.Override(center);
        }

        public void EscapeFieldAnimation(float maxLensDistortion)
        {
            if (_escapeFieldRoutine != null) StopCoroutine(_escapeFieldRoutine);
            _escapeFieldRoutine = StartCoroutine(EscapeFieldRoutine(maxLensDistortion));
        }

        private IEnumerator EscapeFieldRoutine(float maxLensDistortion)
        {
            float timer = 0;
            float ratio;

            while (timer < escapeFieldAnimationDuration)
            {
                ratio = timer / escapeFieldAnimationDuration;
                SetLensDistortion( maxLensDistortion * responseCurve.Evaluate(ratio));
                timer += Time.deltaTime;
                yield return null;
            }

            SetLensDistortion(0);
        }
    }
}
