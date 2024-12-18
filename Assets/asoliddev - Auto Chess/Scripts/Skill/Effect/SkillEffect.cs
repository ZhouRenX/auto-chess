﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillEffect : MonoBehaviour
{
    protected Skill skill;
    //持续时间
    protected float duration;
    //已存在时间
    protected float curTime;

    protected Transform target;

    public float destroyDelay = 0;
    public List<ChampionController> hits;


    public GameObject hitParticleInstance;
    public GameObject emitParticleInstance;
    public virtual void Init(Skill _skill, Transform _target)
    {
        skill = _skill;
        target = _target;
        curTime = 0;
        duration = skill.skillData.duration;
        hits = new List<ChampionController>();
    }

    protected virtual void FixedUpdate()
    {
        curTime += Time.fixedDeltaTime;
    }

    public virtual void DestroySelf()
    {
        if (hitParticleInstance != null)
            Destroy(hitParticleInstance);
        if (emitParticleInstance != null)
            Destroy(emitParticleInstance);
        Destroy(gameObject, destroyDelay);
    }

    protected virtual void InstantiateEmitEffect()
    {
        emitParticleInstance = skill.InstantiateEmitInstance(transform.position, transform.rotation, 1.5f);
    }

    protected virtual void InstantiateHitEffect(Vector3 pos)
    {
        hitParticleInstance = skill.InstantiateHitInstance(pos, Quaternion.FromToRotation(Vector3.up, Vector3.zero), 1.5f);
    }

    protected virtual void PointedAtTarget(Vector3 targetPos)
    {
        Vector3 relativePos = targetPos - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        this.transform.rotation = rotation;
    }

    protected virtual void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "SkillEffectCol")
            return;
        if (hit.tag == "Shield")
        {
            InterceptShieldEffect shieldEffect = hit.GetComponentInParent<InterceptShieldEffect>();
            if (shieldEffect.skill.owner.team != skill.owner.team)
            {
                OnCollideShieldBegin(hit);
                return;
            }
        }
        ChampionController c = hit.gameObject.GetComponentInParent<ChampionController>();
        if (c == null)
            return;

        if (skill.skillTargetType == SkillTargetType.Teammate)
        {
            if (c.team == skill.owner.team)
            {
                OnCollideChampionBegin(c, hit.bounds.ClosestPoint(transform.position));
            }
        }
        else if (skill.skillTargetType == SkillTargetType.Enemy)
        {
            if (c.team != skill.owner.team)
            {
                OnCollideChampionBegin(c, hit.bounds.ClosestPoint(transform.position));
            }
        }
    }

    protected virtual void OnTriggerExit(Collider hit)
    {
        ChampionController c = hit.gameObject.GetComponentInParent<ChampionController>();
        if (c == null)
            return;

        if (skill.skillTargetType == SkillTargetType.Teammate)
        {
            if (c.team == skill.owner.team)
            {
                OnCollideChampionEnd(c, hit.bounds.ClosestPoint(transform.position));
            }
        }
        else if (skill.skillTargetType == SkillTargetType.Enemy)
        {
            if (c.team != skill.owner.team)
            {
                OnCollideChampionEnd(c, hit.bounds.ClosestPoint(transform.position));
            }
        }
    }

    protected virtual void OnCollideShieldBegin(Collider hit)
    {
        InstantiateHitEffect(hit.bounds.ClosestPoint(transform.position));
        InterceptShieldEffect shieldEffect = hit.GetComponent<InterceptShieldEffect>();
        shieldEffect.OnGotHit(skill.owner, skill.skillData.damageData);
        Destroy(gameObject, destroyDelay);
    }

    protected virtual void OnCollideChampionBegin(ChampionController c, Vector3 colPos)
    {
        if (!hits.Contains(c))
        {
            hits.Add(c);
            InstantiateHitEffect(colPos);
        }

    }

    protected virtual void OnCollideChampionEnd(ChampionController c, Vector3 colPos)
    {
        if (hits.Contains(c))
        {
            hits.Remove(c);
        }
    }

    protected virtual List<ChampionController> GetTargetsInRange(ChampionController c)
    {
        return skill.targetsSelector.FindTargetByRange(c, skill.skillRangeSelectorType, skill.skillData.range, skill.owner.team).targets;
    }

    public string GetParam(string name)
    {
        foreach (var p in skill.skillData.paramValues)
        {
            if (p.name == name)
            {
                return p.value;
            }
        }
        return null;
    }
}
