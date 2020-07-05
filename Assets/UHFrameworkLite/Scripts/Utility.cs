namespace UHFrameworkLite {
    public static class Utility
    {
        /// <summary>
        /// Converts Unity Vector3 to UltraHaptics Vector3. Flips z and y axis.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Ultrahaptics.Vector3 ToUH(this UnityEngine.Vector3 v)
        {
            // Note: y and z axis are switched
            return new Ultrahaptics.Vector3(v.x, v.z, v.y);
        }

        /// <summary>
        /// Converts UltraHaptics Vector3 to Unity Vector3. Flips z and y axis.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static UnityEngine.Vector3 ToUnity(this Ultrahaptics.Vector3 v)
        {
            // Note: y and z axis are switched
            return new UnityEngine.Vector3(v.x, v.z, v.y);
        }
    }
}
