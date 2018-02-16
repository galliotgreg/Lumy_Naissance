using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhySkeleton : MonoBehaviour
{
    [SerializeField]
    private GameObject bonePrefab;

    [SerializeField]
    private float IKIter = 1;
    [SerializeField]
    private GameObject IKTarget;
    [SerializeField]
    private GameObject IKAnkor;
    [SerializeField]
    private bool isIK = false;
    [SerializeField]
    private PhyBone rootBone;

    public bool IsIK
    {
        get
        {
            return isIK;
        }

        set
        {
            isIK = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        //BuildSkeleton();
        //rootBone.Origin = IKAnkor.transform.position;
        //ForwardKinematic(rootBone);
    }

    public void BuildSkeleton()
    {
        IList<GameObject> bones = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            bones.Add(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < bones.Count; i++)
        {
            Destroy(bones[i]);
        }

        GameObject head = gameObject.transform.parent.Find("Head").gameObject;
        GameObject tail = gameObject.transform.parent.Find("Tail").gameObject;
        GameObject lumy = gameObject.transform.parent.gameObject;

        //Build Head
        PhyJoin[] headJoins = BuildSubPart(head, lumy);
        PhyJoin[] tailJoins = BuildSubPart(tail, lumy);

        //Link head to tail
        GameObject bone = Instantiate(
            bonePrefab, lumy.transform.position, lumy.transform.rotation);
        bone.transform.parent = gameObject.transform;
        PhyBone phyBone = bone.GetComponent<PhyBone>();
        PhyJoin join1 = headJoins[headJoins.Length - 1];
        PhyJoin join2 = tailJoins[0];
        phyBone.BaseJoin = join1;
        phyBone.HeadJoin = join2;
        join1.DstBones[0] = phyBone;
        join2.SrcBone = phyBone;

        //Get First & last joins
        PhyJoin firstJoin = null;
        if (headJoins.Length > 0)
        {
            firstJoin = headJoins[0];
        } else
        {
            firstJoin = tailJoins[0];
        }
        PhyJoin lastJoin = null;
        if (tailJoins.Length > 0)
        {
            lastJoin = tailJoins[tailJoins.Length - 1];
        }
        else
        {
            lastJoin = headJoins[tailJoins.Length - 1];
        }


        ////Close tail
        lastJoin.DstBones = new PhyBone[0];

        //Set root Bone
        rootBone = firstJoin.DstBones[0];
        IKAnkor = firstJoin.gameObject;
        IKTarget = lastJoin.gameObject;
    }

    private PhyJoin[] BuildSubPart(GameObject tail, GameObject lumy)
    {
        PhyJoin[] joins = new PhyJoin[tail.transform.childCount];
        for (int i = 0; i < tail.transform.childCount; i++)
        {
            joins[i] = tail.transform.GetChild(i).gameObject.GetComponent<PhyJoin>();
        }
        //One Joint Case
        if (joins.Length < 2)
        {
            return joins;
        }

        //General case
        for (int i = 0; i < joins.Length - 1; i++)
        {
            //Instanciate bone
            GameObject bone = Instantiate(
                bonePrefab, lumy.transform.position, lumy.transform.rotation);
            bone.transform.parent = gameObject.transform;
            PhyBone phyBone = bone.GetComponent<PhyBone>();

            //Link Bone to joins
            PhyJoin join1 = joins[i];
            PhyJoin join2 = joins[i + 1];
            phyBone.BaseJoin = join1;
            phyBone.HeadJoin = join2;

            //Link Joins to bone
            join1.DstBones[0] = phyBone;
            join2.SrcBone = phyBone;
        }

        return joins;
    }

    // Update is called once per frame
    void Update()
    {
        if (rootBone == null)
        {
            BuildSkeleton();
            rootBone.Origin = IKAnkor.transform.position;
            ForwardKinematic(rootBone);
        }

        //rootBone.Origin = IKAnkor.transform.position;
        if (!IsIK)
        {
            rootBone.Origin = IKAnkor.transform.position;
            ForwardKinematic(rootBone);
        }
        else
        {
            for (int i = 0; i < IKIter; i++)
            {
                InvertKinematic();
            }
        }
    }

    private void InvertKinematic()
    {
        //COMPUTE POSITIONS FROM TARGET
        //Vector2 upTarget = IKTarget.transform.position;
        //Vector2 downTarget = IKAnkor.transform.position;
        Vector2 upTarget = new Vector2(
            IKTarget.transform.position.x, IKTarget.transform.position.z);
        Vector2 downTarget = new Vector2(
            IKAnkor.transform.position.x, IKAnkor.transform.position.z);

        List<PhyBone> bones = ExtractBones();
        PhyLimb rootLimb = ExtractLimbHierarchy(rootBone);

        //HierarchyIKForwardPass(rootLimb, upTarget);
        //HierarchyIKBackardPass(rootLimb, downTarget);
        IKForwardPass(upTarget, bones);

        //COMPUTE ANGLES FROM POINTS
        //Compute world teta
        //UpdateAngles(bones);
    }
    private void HierarchyIKForwardPass(PhyLimb curLimb, Vector2 upTarget)
    {
        if (curLimb.Childs.Count == 0)
        {
            return;
        }
        else
        {
            Vector2 saveRootPos = new Vector2(
                curLimb.Bones[0].Origin.x,
                curLimb.Bones[0].Origin.y);
            List<Vector2> rootsInstances = new List<Vector2>();
            foreach (PhyLimb child in curLimb.Childs)
            {
                //Reset start
                child.Bones[0].Origin.Set(saveRootPos.x, saveRootPos.y);
                IKForwardPass(upTarget, child.Bones);
                rootsInstances.Add(new Vector2(
                    child.Bones[0].Origin.x,
                    child.Bones[0].Origin.y));
            }

            //Reset Origin
            Vector2 centroid = new Vector2();
            foreach (Vector2 v in rootsInstances)
            {
                centroid += v;
            }
            centroid /= (float)rootsInstances.Count;
            curLimb.Childs[0].Bones[0].Origin.Set(centroid.x, centroid.y);

            IKForwardPass(centroid, curLimb.Bones);
        }

    }

    private void HierarchyIKBackardPass(PhyLimb rootLimb, Vector2 downTarget)
    {
        IKBackwardPass(downTarget, rootLimb.Bones);
    }

    private PhyLimb ExtractLimbHierarchy(PhyBone bone)
    {
        PhyLimb limb = new PhyLimb();
        PhyBone curBone = bone;
        while (true)
        {
            if (curBone == null)
            {
                break;
            }

            limb.Bones.Add(curBone);

            if (curBone.HeadJoin == null
                || curBone.HeadJoin.DstBones.Length == 0)
            {
                break;
            }

            if (curBone.HeadJoin.DstBones.Length == 1)
            {
                curBone = curBone.HeadJoin.DstBones[0];
            }
            else if (curBone.HeadJoin.DstBones.Length > 1)
            {
                foreach (PhyBone childBone in curBone.HeadJoin.DstBones)
                {
                    PhyLimb childLimb = ExtractLimbHierarchy(childBone);
                    childLimb.Bones.Insert(0, curBone);
                    limb.Childs.Add(childLimb);
                }
                break;
            }
        }

        return limb;
    }

    private static void UpdateAngles(List<PhyBone> bones)
    {
        Vector3 refVec = Vector3.right;
        for (int i = 0; i < bones.Count; i++)
        {
            PhyBone curBone = bones[i];
            Vector2 dir = (curBone.End - curBone.Origin).normalized;
            float worldTeta = Vector2.SignedAngle(refVec, dir);
            curBone.WorldTeta = worldTeta;
        }
        //Compute local teta
        bones[0].LocalTeta = bones[0].WorldTeta;
        for (int i = 1; i < bones.Count; i++)
        {
            PhyBone curBone = bones[i];
            PhyBone prevBone = bones[i - 1];
            curBone.LocalTeta = curBone.WorldTeta - prevBone.WorldTeta;
        }
    }

    private void IKBackwardPass(Vector2 downTarget, List<PhyBone> bones)
    {
        Vector2 curTrg = downTarget;
        for (int i = 0; i < bones.Count; i++)
        {
            PhyBone curBone = bones[i];
            curBone.Origin = curTrg;
            Vector2 dir = (curBone.End - curBone.Origin).normalized;
            curBone.End = curBone.Origin + curBone.Lenght * dir;
            curTrg = curBone.End;
        }
    }

    private void IKForwardPass(Vector2 upTarget, List<PhyBone> bones)
    {
        //Forward Pass
        Vector2 curTrg = upTarget;
        for (int i = 0; i < bones.Count; i++)
        {
            PhyBone curBone = bones[bones.Count - 1 - i];
            curBone.End = curTrg;
            Vector2 dir = (curBone.Origin - curBone.End).normalized;
            curBone.Origin = curBone.End + curBone.Lenght * dir;
            curTrg = curBone.Origin;
        }
    }

    private List<PhyBone> ExtractBones()
    {
        //Retrieve Skeleton Parts
        List<PhyBone> bones = new List<PhyBone>();
        PhyBone curBone = rootBone;

        //Rec
        while (curBone != null)
        {
            bones.Add(curBone);

            PhyBone nextBone = null;
            if (curBone.HeadJoin != null && curBone.HeadJoin.DstBones.Length > 0)
            {
                nextBone = curBone.HeadJoin.DstBones[0];
            }
            curBone = nextBone;
        }

        return bones;
    }

    private void ForwardKinematic(PhyBone curBone)
    {
        curBone.WorldTeta = curBone.LocalTeta;
        curBone.ComputeEnd();
        //Get neigbours
        PhyBone prevBone = null;
        if (curBone.BaseJoin != null)
        {
            prevBone = curBone.BaseJoin.SrcBone;
        }

        //Update Cur Bone
        if (prevBone != null)
        {
            curBone.WorldTeta = curBone.LocalTeta + prevBone.WorldTeta;
            curBone.Origin = prevBone.End;
            curBone.ComputeEnd();
        }

        //Update Next Bone on next step
        if (curBone.HeadJoin != null)
        {
            for (int i = 0; i < curBone.HeadJoin.DstBones.Length; i++)
            {
                ForwardKinematic(curBone.HeadJoin.DstBones[i]);
            }
        }
    }
}
