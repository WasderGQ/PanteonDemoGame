

using UnityEngine;

namespace Third_Party_Packages.Helpers.WasderGQ {

   
    public class Assets : MonoBehaviour {

        // Internal instance reference
        private static Assets _i; 

        // Instance reference
        public static Assets i {
            get {
                if (_i == null) _i = Instantiate(Resources.Load<Assets>("WasderGQAssets")); 
                return _i; 
            }
        }


        // All references
        
        public Sprite s_White;
        public Sprite s_Circle;

        public Material m_White;

    }

}
