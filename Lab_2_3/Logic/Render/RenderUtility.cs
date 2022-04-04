using Lab_2_3.Logic.Services;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Lab_2_3.Logic.Render
{
    class RenderUtility
    {
        Image image;
        RenderService renderService;
        public double PixelsPerDip => 96;
        public int FramesPerSecond => 50;
        bool render_started;

        public RenderUtility(Image image, RenderService renderService)
        {
            this.image = image;
            this.renderService = renderService;
        }

        public async void Start()
        {
            render_started = true;
            while (render_started)
            {
                await Task.Delay(1000 / FramesPerSecond);
                image.Source = GetRender();
            }
        }
        public void Stop() => render_started = false;
        private BitmapSource GetRender()
        {
            var bitmap = new RenderTargetBitmap(
                (int)image.Width, (int)image.Height,
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
            renderService.background.Render(dc, renderService.CameraPosition);
            foreach (var horse in renderService.horsesService.Horses)
            {
                horse.Render(dc, renderService.CameraPosition);
            }
            renderService.foreground.Render(dc, renderService.CameraPosition);
        }
    }
}