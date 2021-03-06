#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using System.Reflection;
#endregion

namespace SOM.RevitTools.Sections
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            RibbonPanel p = GetRibbonPanel(a, "LACMA", "LACMA Section");

            string createStartClassName = "SOM.RevitTools.Sections.CommandCreateStart";
            string createEndClassName = "SOM.RevitTools.Sections.CommandCreateEnd";
            string createPerpendicularClassName = "SOM.RevitTools.Sections.CommandCreatePerpendicular";
            string updateClassName = "SOM.RevitTools.Sections.CommandUpdate";
            string imageUP = "SectionUP.png";
            string imageDN = "SectionDN.png";
            string imagePR = "SectionPR.png";
            string imageNW = "SectionNW.png";

            PushButtonData btn1 = pushButton_Setting(p, createStartClassName, imageUP, "Create From Start Section", "CREATE AT START");
            PushButtonData btn2 = pushButton_Setting(p, createEndClassName, imageDN, "Create From End Section", "CREATE AT END");
            PushButtonData btn3 = pushButton_Setting(p, createPerpendicularClassName, imagePR, "Create From Perpendicular Section", "CREATE AT PER");
            PushButtonData btn4 = pushButton_Setting(p, updateClassName, imageNW, "Update Section", "UPDATE SECTION");

            p.AddItem(btn1);
            p.AddSeparator();
            p.AddItem(btn2);
            p.AddSeparator();
            p.AddItem(btn3);
            p.AddSeparator();
            p.AddItem(btn4);

            a.ApplicationClosing += a_ApplicationClosing;

            //Set Application to Idling
            a.Idling += a_Idling;

            return Result.Succeeded;
        }

        private PushButtonData pushButton_Setting(RibbonPanel p, string className, string image, string panel, string name)
        {
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            ////Set globel directory
            var globePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName
                (System.Reflection.Assembly.GetExecutingAssembly().Location), image);

            //Large image 
            Uri uriImage = new Uri(globePath);
            BitmapImage NewBitmapImage = new BitmapImage(uriImage);

            PushButtonData pushButton = new PushButtonData
                (panel, name, thisAssemblyPath, className);
            pushButton.LargeImage = NewBitmapImage;
            pushButton.Image = NewBitmapImage;

            return pushButton;
        }

        public RibbonPanel GetRibbonPanel(UIControlledApplication a, string tab, string p)
        {
            RibbonPanel ribbonPanel = null;

            //Create add-in to the SOM tool ribbon tab
            try
            {
                a.CreateRibbonTab(tab);
            }
            catch (Exception)
            { }
            //Create Ribbon Panel 
            try
            {
                RibbonPanel alignViewsPanel = a.CreateRibbonPanel(tab, p);

            }

            catch (Exception)
            { }

            List<RibbonPanel> List_ribbonPanels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel panel in List_ribbonPanels)
            {
                if (panel.Name == p)
                {
                    ribbonPanel = panel;
                }
            }
            return ribbonPanel;
        }

        void a_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {

        }

        void a_ApplicationClosing(object sender, Autodesk.Revit.UI.Events.ApplicationClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
