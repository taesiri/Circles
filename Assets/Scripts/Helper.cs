using System;
namespace Assets.Scripts
{
		public class Helper
		{
			public Helper () {

			}
			public static float TruncateAngle(float angle) {
						if (angle < 0) {
								angle += 360;
						} else if (angle > 360) {
								angle -= 360;
						}

						return angle;
			}
	}
}