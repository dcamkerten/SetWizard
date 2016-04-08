using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ToscaScrapper.InternalObjects;
using Tricentis.TCAPIObjects.Objects;
using mshtml;

namespace ToscaScrapper.InternalTasks
{
    public class Scrapper
    {
       public static Dictionary<string, dynamic> DecideForTechnicals(IHTMLElement element)
        {
            var technicalList = new Dictionary<string, dynamic>
            {
                { "Id", element.id},
                { "Name",  element.getAttribute("name")},
                { "Tag", element.tagName},
                { "Title",  element.getAttribute("title")},
                { "Type",  element.getAttribute("type")},
                { "ClassName",  element.className},
                { "Value",  element.getAttribute("value")}
            };

            foreach (var elem in technicalList.ToList())
            {
                if (elem.Value != null)
                {
                   if (Convert.ToString(elem.Value).Trim().Equals("")) technicalList.Remove(elem.Key);
                }
                else if (elem.Value == null) technicalList.Remove(elem.Key);
            }

            if (technicalList.Count == 0) technicalList.Add("outerHTML", Scrapper.MakeHtmlToscaConform(element.outerHTML, element.getAttribute("type")));
            return technicalList;
        }


        public static string MakeHtmlToscaConform(string unformatted, string type)
        {
            unformatted = unformatted.Replace(type, type.ToLower());
            return "\"" + Regex.Replace(unformatted, @"(?<name>\S+)\s*=\s*(""(?<value>[^""]*)""|(?<value>[\S-[/>]]+))", @"${name}=""""${value}""""") + "\"";
        }

        public XModule SetupXModule(string windowTitle, TCFolder folder)
        {
            var newXModule = folder.CreateXModule();

            if (windowTitle == "") windowTitle = "HtmlDocument";

            newXModule.Name = windowTitle;

            newXModule.BusinessType = "HtmlDocument";
            newXModule.InterfaceType = XModule_InterfaceTypeE.GUI;

            var engine = newXModule.CreateConfigurationParam();
            engine.Name = "Engine";
            engine.Value = "Html";

            var title = newXModule.CreateTechnicalIDParam();

            title.Name = "Title";
            title.Value = windowTitle;

            return newXModule;
        }


        public void CreateAttributes(Tuple<XModule, XModuleAttribute, bool> modulePack, IHTMLElement node)
        {
           
            switch (node.tagName)
            {
                case "INPUT":
                    string type = node.getAttribute("type", 0);
                    switch (type)
                    {
                        case "text":
                        case "password":
                            var textfield = new Textfield();
                            textfield.SetupAttributes(modulePack, node);
                            break;

                        case "checkbox":
                            var checkbox = new Checkbox();
                            checkbox.SetupAttributes(modulePack, node);
                            break;

                        case "hidden":
                            var hidden = new HiddenInput();
                            hidden.SetupAttributes(modulePack, node);
                            break;

                        case "submit":
                        case "reset":
                        case "button":
                            var newButton = new InternalObjects.Button();
                            newButton.SetupAttributes(modulePack, node);
                            break;

                    }
                    break;

                case "select":
                    var combobox = new Combobox();
                    combobox.SetupAttributes(modulePack, node);
                    break;
                case "A":
                    var newLink = new Link();
                    newLink.SetupAttributes(modulePack, node);
                    break;

                case "table":
                    var newTable = new Table();
                    newTable.SetupAttributes(modulePack, node);

                    /*var subAttributeRow = mainModule.CreateModuleAttribute();
                    newTable.SetupRowAttributes(subAttributeRow, node);

                    var subAttributeCellforRow = subAttributeRow.CreateModuleAttribute();
                    newTable.SetupCellAttributes(subAttributeCellforRow, node);

                    var subAttributeCol = mainModule.CreateModuleAttribute();
                    newTable.SetupColAttributes(subAttributeCol, node);

                    var subAttributeCellforCol = subAttributeCol.CreateModuleAttribute();
                    newTable.SetupCellAttributes(subAttributeCellforCol, node);*/
                    break;
            }
        }
    }
}
