using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class TargetHit : MonoBehaviour
    {
        private static int targetScore = 100;
        private static int snowballScore = 5;
        private AudioSource pickup;
        private Rigidbody playerObject;

        // Use this for initialization
        void Start()
        {
            pickup = GameObject.Find("AudioSourceSnowball").GetComponent<AudioSource>();
            playerObject = GameObject.Find("RigidBodyFPSController").GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public static void hitTarget()
        {
            //Debug.Log("Target Hit! Add score!");
            int currscore = DataDictionary.getDD().getlevel1Score();
            //Debug.Log("currscore= " + currscore);
            DataDictionary.getDD().setlevel1Score(currscore + targetScore);
            //System.Threading.Thread.Sleep(500);
        }

        public static void hitSnowball()
        {
            int currscore = DataDictionary.getDD().getlevel1Score();
            DataDictionary.getDD().setlevel1Score(currscore + snowballScore);
            
        }

        private void OnTriggerEnter(Collider other)
        {

            // Level over
            if (other.gameObject.name == "FinalTarget")
            {
                // Add level1score and timeBonus to totalscore
                DataDictionary.getDD().setisPlaying(false);
                int currscore = DataDictionary.getDD().getlevel1Score();
                currscore += DataDictionary.getDD().gettimeBonus();
                int tscore = DataDictionary.getDD().gettotalScore();
                DataDictionary.getDD().settotalScore(tscore + currscore);

                playerObject.constraints = RigidbodyConstraints.FreezePosition;
                return;
            }

            if (other.gameObject.name.StartsWith("Snowball"))
            {
                hitSnowball();
                Destroy(other.gameObject);
                pickup.Play();
                return;
            }

            if (!DataDictionary.getDD().hitListContains(other.gameObject.name))
            {
                hitTarget();
                DataDictionary.getDD().addhitList(other.gameObject.name);
            }
            
        }
        
    }
}
