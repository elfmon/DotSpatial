﻿using System.Drawing;
using System.Windows.Forms;
using DotSpatial.Controls;
using DotSpatial.Controls.Docking;
using DotSpatial.Plugins.SimpleLegend.Properties;

namespace DotSpatial.Plugins.SimpleLegend
{
    public class SimpleLegendPlugin : Extension
    {
        public override void Activate()
        {
            ShowLegend();
            base.Activate();
        }

        public override void Deactivate()
        {
            App.HeaderControl.RemoveAll();
            App.DockManager.Remove("kLegend");
            base.Deactivate();
        }

        private void ShowLegend()
        {
            var legend1 = new Legend
                          {
                              BackColor = Color.White,
                              ControlRectangle = new Rectangle(0, 0, 176, 128),
                              DocumentRectangle = new Rectangle(0, 0, 34, 114),
                              HorizontalScrollEnabled = true,
                              Indentation = 30,
                              IsInitialized = false,
                              Location = new Point(217, 12),
                              MinimumSize = new Size(5, 5),
                              Name = "legend1",
                              ResetOnResize = false,
                              SelectionFontColor = Color.Black,
                              SelectionHighlight = Color.FromArgb(215, 238, 252),
                              Text = Resources.Legend,
                              VerticalScrollEnabled = true,
                              Size = new Size(150, 200)
                          };

            if (App.Map != null)
            {
                App.Map.Legend = legend1;
            }
            App.Legend = legend1;
            App.DockManager.Add(new DockablePanel("kLegend", Resources.Legend, legend1, DockStyle.Left) { SmallImage = Resources.legend_16x16 });
        }
    }
}