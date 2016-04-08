using System;
using System.Text.RegularExpressions;
using mshtml;
using ToscaScrapper.InternalTasks;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaScrapper.InternalObjects
{
    class Link
    {
        public void SetupAttributes(Tuple<XModule, XModuleAttribute, bool> modulePack, IHTMLElement link)
        {

            var checkValidLink = link.getAttribute("href");
            if (checkValidLink.Contains("javascript()") || link.innerText==null || checkValidLink=="") return;

            var rgx = new Regex("\n");
            var innerText = rgx.Replace(link.innerText, "").Trim();

            if (innerText == "") return;


            var attribute = modulePack.Item3 ? modulePack.Item1.CreateModuleAttribute() : modulePack.Item2.CreateModuleAttribute();

            attribute.DefaultActionMode = XTestStepActionMode.Input;
            attribute.Name = innerText;

            attribute.BusinessType = "Link";
            AddBusinessParam(attribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(attribute.CreateConfigurationParam(), "BusinessAssociation", "Descendants");

            foreach (var technical in Scrapper.DecideForTechnicals(link))
            {
                AddBusinessParam(attribute.CreateTechnicalIDParam(), technical.Key, technical.Value);
            }

            AddBusinessParam(attribute.CreateTechnicalIDParam(), "InnerText", innerText);
        }


        public void AddBusinessParam(XParam param, string name, string value)
        {
            param.Name = name;
            param.Value = value;
        }
    }



}
