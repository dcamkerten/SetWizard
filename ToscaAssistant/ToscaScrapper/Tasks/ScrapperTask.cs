using System;
using System.Collections.ObjectModel;
using ToscaScrapper.InternalTasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Forms;
using mshtml;

namespace ToscaScrapper.Tasks
{

    internal class ScrapperTask : TCAddOnTask
    {
        public override Type ApplicableType => typeof (TCFolder);

        public override string Name => "New Project";

        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {

            //var currentSetting=Config.ReadConfig();

            var folder = objectToExecuteOn as TCFolder;
            var allHtmlDocuments = new Collection<Tuple<string, string>>();
            var scrapper = new Scrapper();

            var newWindowThread = new Thread(() =>
            {
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(
                        Dispatcher.CurrentDispatcher));

                var preferencesWindow = new WpfApplication1.MainWindow();

                preferencesWindow.Closed += (s, e) =>
                    Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);

                preferencesWindow.Show();
                Dispatcher.Run();
            });

            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();

            const int currentPercent = 0;

            taskContext.ShowProgressInfo(100, currentPercent + 5, "Enter Domain");

            //var baseUrl = new Uri(taskContext.GetStringValue("Please enter a domain adress", false));
            //var mainData = GetHtml(baseUrl);

            GetHtml(new Uri("http://www.toscatrial.com/sampleapp/VehicleData.html"), folder);

            taskContext.ShowProgressInfo(100, 100, "Finishing");
            return null;
        }

        private static void GetHtml(Uri baseUrl, TCFolder folder)
        {
            string[] definedElements = {"a","input"};

            using (var browser = new Browser())
            {
                browser.Await(
                    wb => wb.Navigate(baseUrl),
                    wb =>
                    {
                       var html = (IHTMLDocument3)wb.Document.DomDocument;
                        
                        var scrapper = new Scrapper();
                        var mainModule = scrapper.SetupXModule(wb.Document.Title, folder);
                        var modulePack = new Tuple<XModule, XModuleAttribute,bool>(mainModule,null,true);

                        foreach (var element in definedElements)
                        {
                            var iElements = html.getElementsByTagName(element);

                            foreach (IHTMLElement elem in iElements)
                            {
                                scrapper.CreateAttributes(modulePack,elem);
                            }
                        }

                        foreach (HtmlWindow frame in wb.Document.Window.Frames)
                        {
                            try
                            {
                                html = frame.Document.DomDocument as IHTMLDocument3;
                            }
                            catch (UnauthorizedAccessException)
                            {
                                continue;
                            }

                            bool firstIframe = true;

                            foreach (var element in definedElements)
                            {
                                var iElements = html.getElementsByTagName(element);

                                foreach (IHTMLElement elem in iElements)
                                {
                                    if (firstIframe)
                                    {
                                        var iFrame = new InternalObjects.Iframe();

                                        var htmlDocumentAttr = iFrame.SetupAttributes(mainModule, frame.WindowFrameElement);

                                        modulePack = new Tuple<XModule, XModuleAttribute, bool>(mainModule, htmlDocumentAttr, false);
                                        firstIframe = false;
                                    }

                                    scrapper.CreateAttributes(modulePack, elem);
                                }
                            }
                        }


                    });
            }
        }
    }
}