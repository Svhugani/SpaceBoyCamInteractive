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
        [SerializeField] private float defaultEffectDuration = 1.5f;
        [SerializeField] private float defaultDistortionIntensity = 10;

        private LensDistortion _lensDistortion;
        private DepthOfField _depthOfField;
        private Coroutine _distortionEffect;
        public LensDistortion LensDistortion { get { if (_lensDistortion == null) volumeProfile.TryGet(out _lensDistortion); return _lensDistortion; } }
        public DepthOfField DepthOfField { get { if (_distortionEffect == null) volumeProfile.TryGet(out _depthOfField); return _depthOfField; } }

        public override async Task Prepare()
        {
            SetLensCenter(new Vector2(.5f, .5f));
            await Task.Yield();
        }
        public void SetLensDistortion(float distortion)
        {
            LensDistortion.intensity.Override(distortion);
        }

        public void SetLensCenter(Vector2 center)
        {
            LensDistortion.center.Override(center);
        }

        public void SetDoFFocusDist(float focusDist)
        {
            DepthOfField.focusDistance.Override(focusDist);
        }    

        public void DistortionEffect(float maxLensDistortion)
        {
            if (LensDistortion == null) return;
            if (_distortionEffect != null) StopCoroutine(_distortionEffect);
            _distortionEffect = StartCoroutine(DistortionRoutine(maxLensDistortion));
        }

        public void DistortionEffect()
        {
            if (LensDistortion == null) return;
            if (_distortionEffect != null) StopCoroutine(_distortionEffect);
            _distortionEffect = StartCoroutine(DistortionRoutine(defaultDistortionIntensity));
        }

        private IEnumerator DistortionRoutine(float maxLensDistortion)
        {
            float timer = 0;
            float ratio;

            while (timer < defaultEffectDuration)
            {
                ratio = timer / defaultEffectDuration;
                SetLensDistortion( maxLensDistortion * responseCurve.Evaluate(ratio));
                timer += Time.deltaTime;
                yield return null;
            }

            SetLensDistortion(0);
        }
    }
}
