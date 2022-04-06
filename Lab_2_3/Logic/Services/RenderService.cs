using Lab_2_3.Logic.Models;
using System.Collections.Generic;

namespace Lab_2_3.Logic.Services
{    
    class RenderService
    {
        public delegate int GetPosition();
        public delegate (int, int) GetSize();
        public RenderService(HorsesService horsesService, List<BackgroundObject> backgrounds, List<BackgroundObject> foregrounds, GetSize renderSizeDelegate)
        {
            this.horsesService = horsesService;
            this.backgrounds = backgrounds;
            this.foregrounds = foregrounds;
            GetRenderSize = renderSizeDelegate;
        }
        public HorsesService horsesService { get; }
        public List<BackgroundObject> backgrounds { get; }
        public List<BackgroundObject> foregrounds { get; }
        public GetSize GetRenderSize { get; private set; }
        public GetPosition GetCameraPosition { get; private set; }
        public Horse ObservableHorse { get; private set; }
        public Horse GetObservableHorse() => ObservableHorse;
        public void ChangeObserver(Horse horse)
        {
            if (horse is null) return;
            ObservableHorse = horse;
            GetCameraPosition = () => horse.Position + 60 - GetRenderSize().Item1 / 2;
        }
    }
}