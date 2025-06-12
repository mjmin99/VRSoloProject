using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class GrabAndThrow : MonoBehaviour
    {
        public OVRHand hand;
        public OVRSkeleton skeleton;
        private GameObject heldObject;
        private Rigidbody heldRb;

        void Update()
        {
            bool isPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            if (isPinching && heldObject == null)
            {
                TryGrab();
            }
            else if (!isPinching && heldObject != null)
            {
                Release();
            }
        }

        void TryGrab()
        {
            Vector3 tipPos = skeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
            Collider[] hits = Physics.OverlapSphere(tipPos, 0.02f);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Note"))
                {
                    heldObject = hit.gameObject;
                    heldRb = heldObject.GetComponent<Rigidbody>();
                    heldRb.isKinematic = true;
                    heldObject.transform.SetParent(transform);
                    break;
                }
            }
        }

        void Release()
        {
            heldObject.transform.SetParent(null);
            heldRb.isKinematic = false;
            heldRb.velocity = hand.transform.forward * 3f; // 손의 forward 방향
            heldObject = null;
            heldRb = null;
        }
    }

