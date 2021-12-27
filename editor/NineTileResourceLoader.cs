using HxManager;

namespace editor
{
    public class NineTileResourceLoader : GenericResourceLoader<string, NineTile>
    {
        // ReSharper disable once HeapView.ObjectAllocation.Evident
        public static NineTileResourceLoader Instance { get; } = new NineTileResourceLoader();

        static NineTileResourceLoader()
        {

        }

        private NineTileResourceLoader()
        {

        }
        
        public override void LoadResources()
        {
            Add("frame", new NineTile(TextureContentLoader.Instance.Find("ui/frame"),6), true);
            Add("scrollbar", new NineTile(TextureContentLoader.Instance.Find("ui/frame"), 2));
            Add("test", new NineTile(TextureContentLoader.Instance.Find("ui/test"), 6));
        }

    }
}