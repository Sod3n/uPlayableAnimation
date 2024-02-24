using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UPlayable.AnimationMixer
{
    public class AnimationClipOutput : BaseAnimationOutput
    {
        public AnimationClip ToClip;
        private AnimationClipPlayable m_toPlayable;
        protected override Playable m_managerInput => m_toPlayable;

        protected override void ParseSettingToModel()
        {
            var speed = TransitionSetting.ClipSpeed;
            var clipLength = ToClip != null ? ToClip.length : -1;

            if (!TransitionSetting.UseClipDuration) speed *= clipLength / TransitionSetting.ClipDuration;

            m_model = new AnimationOutputModel
            {
                IsAnimatorPlayable = false,
                ClipLength = clipLength,
                OutputTargetWeight = TransitionSetting.OutputTargetWeight,
                FadeInTime = TransitionSetting.FadeInTime,
                ExitTime = TransitionSetting.ExitTime,
                FixedTimeOffset = TransitionSetting.FixedTimeOffset,
                RestartWhenPlay = TransitionSetting.RestartWhenPlay,
                Speed = speed,
            };
        }

        protected override void CreatePlayables()
        {
            m_toPlayable = AnimationClipPlayable.Create(m_manager.PlayableGraph, ToClip);
            m_toPlayable.SetTime(0f);
            m_Id = m_toPlayable.GetHashCode();
        }
    }

}
