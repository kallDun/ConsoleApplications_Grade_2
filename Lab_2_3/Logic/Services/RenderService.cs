using Lab_2_3.Logic.Models;

namespace Lab_2_3.Logic.Services
{
    class RenderService
    {
        public RenderService(HorsesService horsesService, BackgroundObject background, BackgroundObject foreground)
        {
            this.horsesService = horsesService;
            this.background = background;
            this.foreground = foreground;
        }

        public int CameraPosition { get; private set; }
        public HorsesService horsesService { get; }
        public BackgroundObject background { get; }
        public BackgroundObject foreground { get; }
    }
}