using Dalamud.Game.ClientState;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using Dalamud.Plugin;
using DalamudPluginProjectTemplate.Attributes;
using System;
using System.IO;
using System.Reflection;

namespace RPGUIplugin
{
    public class Plugin : IDalamudPlugin
    {
        private readonly DalamudPluginInterface pluginInterface;
        private readonly ClientState clientState;

        private readonly Configuration config;

        private Parameters parameters;

        public string Name => "RPGUI";

        public Plugin(
            DalamudPluginInterface pi,
            ClientState cs)
        {
            this.pluginInterface = pi;
            this.clientState = cs;

            // you might normally want to embed resources and load them from the manifest stream
            var assemblyLocation = Path.GetDirectoryName(pluginInterface.AssemblyLocation.DirectoryName + "\\");
            var imagePath = Path.Combine(assemblyLocation, @"Textures/testtex.png");

            var paramBorder = this.pluginInterface.UiBuilder.LoadImage(Path.Combine(assemblyLocation, @"Textures/border.png"));
            var paramGradient = this.pluginInterface.UiBuilder.LoadImage(Path.Combine(assemblyLocation, @"Textures/gradient.png"));
            var paramHealth = this.pluginInterface.UiBuilder.LoadImage(Path.Combine(assemblyLocation, @"Textures/health.png"));
            var paramMana = this.pluginInterface.UiBuilder.LoadImage(Path.Combine(assemblyLocation, @"Textures/mana.png"));
            var paramLimit = this.pluginInterface.UiBuilder.LoadImage(Path.Combine(assemblyLocation, @"Textures/limit.png"));
            var paramBackground = this.pluginInterface.UiBuilder.LoadImage(Path.Combine(assemblyLocation, @"Textures/background.png"));
            this.parameters = new Parameters(paramBorder, paramGradient, paramHealth, paramMana, paramLimit, paramBackground);

            this.pluginInterface.UiBuilder.Draw += this.parameters.Draw;
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            this.pluginInterface.SavePluginConfig(this.config);

            this.pluginInterface.UiBuilder.Draw -= this.parameters.Draw;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
