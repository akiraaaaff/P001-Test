using Lockstep.Collision2D;
using Lockstep.Math;
using UnityEngine;
using Debug = Lockstep.Logging.Debug;

namespace Lockstep.Logic {
    public class InputManager : UnityEngine.MonoBehaviour {
        [HideInInspector] public Player player => GameManager.player;
        [HideInInspector] public int floorMask;
        public float camRayLength = 100;

        public static bool IsReplay = false;

        void Start(){
            floorMask = LayerMask.GetMask("Floor");
        }

        public void Update(){
        }

        public static bool hasHitFloor;
        public static LVector2 mousePos;
        public static LVector2 inputUV;
        public static bool isInputFire;
        public static int skillId;
        public static bool isSpeedUp;
    }


}