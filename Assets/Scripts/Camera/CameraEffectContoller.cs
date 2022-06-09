using UnityEngine;
using WaterRippleForScreens;
using QFSW.RetroFXUltimate;

public class CameraEffectContoller : MonoBehaviour {

	private RetroFX retro;

	private float defResPer = 1f;
	private float defConBoost = 0f;
	private float defBrigBoost = 0f;

	#region Mushroom
	public Effect mushroomEffect;

	private float MminResPer = 0.4f;
	private float MmaxResPer = 1f;
	private float MdeltaResPer = 0.3f;

	private float MminConBoost = 0.5f;
	private float MmaxConBoost = 1.3f;
	private float MdeltaConBoost = 0.3f;
	private bool MonCon = false;

	private float MminBrigBoost = 0f;
	private float MmaxBrigBoost = 0.4f;
	private float MdeltaBrigBoost = 0.2f;
	private bool MonBrig = false;

	private RippleGenerator ripple;
	private float minTimeRipple = 0.1f;
	private float maxTimeRipple = 2f;
	private float deltaRippleTime;

	private float MpeakEffect;

	private bool onMushroom = false;

	public void TakeMushroomEffect () {
		onMushroom = true;

		MpeakEffect = mushroomEffect.effectTime * 0.6f;
		deltaRippleTime = (maxTimeRipple - minTimeRipple) / MpeakEffect;

		MdeltaResPer = 0.3f;
		MdeltaConBoost = 0.3f;
		MdeltaBrigBoost = 0.2f;

		ripple.randomGeneration = true;
	}

	public void RevealMushroomEffect () {
		ripple.randomGeneration = false;
		ripple.timeBetweenRippleMedian = maxTimeRipple;

		retro.ResolutionPercentage = defResPer;
		retro.ContrastBoost = defConBoost;
		retro.BrightnessBoost = defBrigBoost;

		MonCon = false;
		MonBrig = false;

		onMushroom = false;
	}
	#endregion

	private void Awake () {
		GetReferences ();
	}

	private void FixedUpdate () {
		if (onMushroom) {
			if (!MonBrig && retro.BrightnessBoost >= MmaxBrigBoost) {
				MonBrig = true;
			}

			if (MonBrig && (retro.BrightnessBoost >= MmaxBrigBoost || retro.BrightnessBoost <= MminBrigBoost)) {
				MdeltaBrigBoost = -MdeltaBrigBoost;
			}

			if (!MonCon && retro.ContrastBoost >= MmaxConBoost) {
				MonCon = true;
			}

			if (MonCon && (retro.ContrastBoost >= MmaxConBoost || retro.ContrastBoost <= MminConBoost)) {
				MdeltaConBoost = -MdeltaConBoost;
			}

			if (retro.ResolutionPercentage >= MmaxResPer || retro.ResolutionPercentage <= MminResPer) {
				MdeltaResPer = -MdeltaResPer;
			}

			retro.BrightnessBoost += MdeltaBrigBoost * Time.deltaTime;
			retro.ContrastBoost += MdeltaConBoost * Time.deltaTime;
			retro.ResolutionPercentage -= MdeltaResPer * Time.deltaTime;

			if (ripple.timeBetweenRippleMedian <= minTimeRipple) {
				deltaRippleTime = -(deltaRippleTime * 1.3f);
			}

			ripple.timeBetweenRippleMedian -= deltaRippleTime * Time.deltaTime;

		}
	}

	private void GetReferences () {
		ripple = GetComponent<RippleGenerator> ();
		retro = GetComponent<RetroFX> ();
	}
}
