using System;
using mshtml;
using ToscaScrapper.InternalTasks;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaScrapper.InternalObjects
{
    internal class Textfield
    {

        public void SetupAttributes(Tuple<XModule, XModuleAttribute, bool> modulePack, IHTMLElement textfield)
        {
            var attribute = modulePack.Item3 ? modulePack.Item1.CreateModuleAttribute() : modulePack.Item2.CreateModuleAttribute();


            var attrName = textfield.getAttribute("value") ?? textfield.getAttribute("name");

            attribute.Name = attrName;
            attribute.Cardinality = "0-1";
            attribute.BusinessType = "TextBox";
            attribute.DefaultActionMode = XTestStepActionMode.Input;

            AddBusinessParam(attribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(attribute.CreateConfigurationParam(), "BusinessAssociation", "Descendants");

            foreach (var technical in Scrapper.DecideForTechnicals(textfield))
            {
                AddBusinessParam(attribute.CreateTechnicalIDParam(), technical.Key, technical.Value);
            }

        }


        public void AddBusinessParam(XParam param, string name, string value)
        {
            param.Name = name;
            param.Value = value;
        }
    }
}

