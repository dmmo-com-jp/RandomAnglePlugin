using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomAnglePlugin;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Controls;
using YukkuriMovieMaker.Exo;
using YukkuriMovieMaker.Player.Video;
using YukkuriMovieMaker.Plugin;
using YukkuriMovieMaker.Plugin.Effects;

namespace RandomAnglePlugin
{
    /// <summary>
    /// 映像エフェクト
    /// 映像エフェクトには必ず[VideoEffect]属性を設定してください。
    /// </summary>
    [VideoEffect("ランダム角度", ["アニメーション"], [])]
    internal class RandomAngleEffect : VideoEffectBase
    {
        /// <summary>
        /// エフェクトの名前
        /// </summary>
        public override string Label => "ランダム角度";

        /// <summary>
        /// アイテム編集エリアに表示するエフェクトの設定項目。
        /// [Display]と[AnimationSlider]等のアイテム編集コントロール属性の2つを設定する必要があります。
        /// [AnimationSlider]以外のアイテム編集コントロール属性の一覧はSamplePropertyEditorsプロジェクトを参照してください。
        /// </summary>
        [Display(Name = "X軸", Description = "X軸")]
        [AnimationSlider("F0", "度", 0, 360)]
        public Animation X軸 { get; } = new Animation(0, -10000, 10000);
        [Display(Name = "Y軸", Description = "Y軸")]
        [AnimationSlider("F0", "度", 0, 360)]
        public Animation Y軸 { get; } = new Animation(0, -10000, 10000);
        [Display(Name = "Z軸", Description = "Z軸")]
        [AnimationSlider("F0", "度", 0, 360)]
        public Animation Z軸 { get; } = new Animation(0, -10000, 10000);
        [Display(Name = "間隔", Description = "間隔")]
        [AnimationSlider("F2", "s", 0, 1)]
        public Animation 間隔 { get; } = new Animation(0, -10000, 10000);
        /// <summary>
        /// Exoフィルタを作成する。
        /// </summary>
        /// <param name="keyFrameIndex">キーフレーム番号</param>
        /// <param name="exoOutputDescription">exo出力に必要な各種情報</param>
        /// <returns></returns>
        public override IEnumerable<string> CreateExoVideoFilters(int keyFrameIndex, ExoOutputDescription exoOutputDescription)
        {
            //サンプルはSampleD2DVideoEffectを参照
            return [];
        }

        /// <summary>
        /// 映像エフェクトを作成する
        /// </summary>
        /// <param name="devices">デバイス</param>
        /// <returns>映像エフェクト</returns>
        public override IVideoEffectProcessor CreateVideoEffect(IGraphicsDevicesAndContext devices)
        {
            return new RandomAngleEffectProcessor(this);
        }

        /// <summary>
        /// クラス内のIAnimatableを列挙する。
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<IAnimatable> GetAnimatables() => [Z軸];
        public PluginDetailsAttribute Details => new()
        {
            //制作者
            AuthorName = "メタロロ",
            //作品ID
            ContentId = "",
        };
    }

}