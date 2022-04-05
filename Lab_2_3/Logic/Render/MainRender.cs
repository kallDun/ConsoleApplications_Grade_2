using Lab_2_3.Logic.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab_2_3.Logic.Render
{
    class MainRender
    {
        public delegate (int, int) GetRenderSize();
        GetRenderSize renderSizeDelegate;

        public event Action RenderEvent;

        Image image;
        RenderService renderService;
        public double PixelsPerDip => 96;
        public int FramesPerSecond => 50;
        bool render_started;

        public MainRender(Image image, GetRenderSize renderSizeDelegate, RenderService renderService)
        {
            this.image = image;
            this.renderService = renderService;
            this.renderSizeDelegate = renderSizeDelegate;
        }

        public async void Start()
        {
            render_started = true;
            while (render_started)
            {
                await Task.Delay(1000 / FramesPerSecond);
                RenderFrame();
                RenderEvent?.Invoke();
            }
        }
        public void Stop() => render_started = false;
        public void RenderFrame() => image.Source = GetRender();
        private BitmapSource GetRender()
        {
            var size = renderSizeDelegate();
            var bitmap = new RenderTargetBitmap(
                size.Item1, size.Item2,
                PixelsPerDip, PixelsPerDip, PixelFormats.Pbgra32);

            var drawingvisual = new DrawingVisual();

            using (DrawingContext dc = drawingvisual.RenderOpen())
            {
                Render(dc);
            }

            bitmap.Render(drawingvisual);
            return bitmap;
        }
        private void Render(DrawingContext dc)
        {
            int camera_position = renderService.GetCameraPosition();
            foreach (var item in renderService.backgrounds)
            {
                item.Render(dc, camera_position);
            }
            foreach (var horse in renderService.horsesService.Horses.Reverse())
            {
                horse.Render(dc, camera_position);
            }
            foreach (var item in renderService.foregrounds)
            {
                item.Render(dc, camera_position);
            }
        }
    }
}