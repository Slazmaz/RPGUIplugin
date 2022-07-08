using ImGuiNET;
using ImGuiScene;
using System;
using System.Numerics;

namespace RPGUIplugin
{
    // It is good to have this be disposable in general, in case you ever need it
    // to do any cleanup
    class Parameters : IDisposable
    {
        private TextureWrap paramBorder;
        private TextureWrap paramGradient;
        private TextureWrap paramHealth;
        private TextureWrap paramMana;
        private TextureWrap paramLimit;
        private TextureWrap paramBackground;

        // passing in the image here just for simplicity
        public Parameters(TextureWrap paramBorder, TextureWrap paramGradient, TextureWrap paramHealth, TextureWrap paramMana, TextureWrap paramLimit, TextureWrap paramBackground)
        {
            this.paramBorder = paramBorder;
            this.paramGradient = paramGradient;
            this.paramHealth = paramHealth;
            this.paramMana = paramMana;
            this.paramLimit = paramLimit;
            this.paramBackground = paramBackground;
        }

        // this extra bool exists for ImGui, since you can't ref a property
        private bool visible = false;
        public bool Visible
        {
            get { return this.visible; }
            set { this.visible = value; }
        }

        public void Dispose()
        {
            this.paramBorder.Dispose();
            this.paramGradient.Dispose();
            this.paramHealth.Dispose();
            this.paramMana.Dispose();
            this.paramLimit.Dispose();
            this.paramBackground.Dispose();
        }

        public void Draw()
        {
            DrawMainWindow();
        }

        public void DrawMainWindow()
        {
            ImGuiWindowFlags window_flags = 0;
            window_flags |= ImGuiWindowFlags.NoTitleBar;
            window_flags |= ImGuiWindowFlags.NoScrollbar;
            if (false)
            {
                window_flags |= ImGuiWindowFlags.NoMove;
                window_flags |= ImGuiWindowFlags.NoMouseInputs;
                window_flags |= ImGuiWindowFlags.NoNav;
            }
            window_flags |= ImGuiWindowFlags.AlwaysAutoResize;
            window_flags |= ImGuiWindowFlags.NoBackground;
            
            var size = new Vector2(320, 320);
            ImGui.SetNextWindowSize(size, ImGuiCond.FirstUseEver);
            if (ImGui.Begin("Parameters", ref visible, window_flags))
            {
                var drawList = ImGui.GetWindowDrawList();
                var position = new Vector2(100, 100);
                drawList.AddImage(this.background.ImGuiHandle, position, 
                    position + new Vector2(this.background.Width, this.background.Height), 
                    new Vector2(0, 0), new Vector2(1, 1), ImGui.GetColorU32(new Vector4(1f, 1f, 1f, 1f)));
            }
            ImGui.End();
        }
    }
}