using RandomAnglePlugin;
using System;
using System.Windows.Media.Imaging;
using Vortice.Direct2D1;
using Vortice.DirectWrite;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace RandomAnglePlugin
{
    internal class RandomAngleEffectProcessor : IVideoEffectProcessor
    {
        readonly RandomAngleEffect item;
        ID2D1Image? input;

        public ID2D1Image Output => input ?? throw new NullReferenceException(nameof(input) + "is null");

        public RandomAngleEffectProcessor(RandomAngleEffect item)
        {
            this.item = item;
        }

        /// <summary>
        /// エフェクトを更新する
        /// </summary>
        /// <param name="effectDescription">エフェクトの描画に必要な各種情報</param>
        /// <returns>描画位置等の情報</returns>
        public DrawDescription Update(EffectDescription effectDescription)
        {
            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;
            var drawDesc = effectDescription.DrawDescription;
            var X軸 = item.X軸.GetValue(frame, length, fps);
            var Y軸 = item.Y軸.GetValue(frame, length, fps);
            var Z軸 = item.Z軸.GetValue(frame, length, fps);
            var 間隔 = item.間隔.GetValue(frame, length, fps);
            var seeds = frame + drawDesc.Draw.X + drawDesc.Draw.Y;
            var fps_frame = frame % fps;
            var fps間隔 = fps_frame % (fps / Math.Round(1 / 間隔));
            if (fps間隔 != 0)
            {
                Random random2 = new Random((int)seeds - (int)fps間隔);
                return
                    drawDesc with
                    {
                        Rotation = new(
                        drawDesc.Rotation.X + random2.Next(Math.Abs((int)X軸)) - ((int)X軸 / 2),
                        drawDesc.Rotation.Y + random2.Next(Math.Abs((int)Y軸)) - ((int)Y軸 / 2),
                        drawDesc.Rotation.Z + random2.Next(Math.Abs((int)Z軸)) - ((int)Z軸 / 2)
                        )
                    };
            }

            Random random = new Random((int)seeds);
            return
                drawDesc with
                {
                    Rotation = new (
                        drawDesc.Rotation.X + random.Next(Math.Abs((int)X軸)) - ((int)X軸 / 2),
                        drawDesc.Rotation.Y + random.Next(Math.Abs((int)Y軸)) - ((int)Y軸 / 2),
                        drawDesc.Rotation.Z + random.Next(Math.Abs((int)Z軸)) - ((int)Z軸 / 2)
                        )
                };
        }
        public void ClearInput()
        {
            input = null;
        }
        public void SetInput(ID2D1Image? input)
        {
            this.input = input;
        }

        public void Dispose()
        {

        }

    }
}