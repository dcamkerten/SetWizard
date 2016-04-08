using System;
using mshtml;
using ToscaScrapper.InternalTasks;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaScrapper.InternalObjects
{
    class Combobox
    {
        public void SetupAttributes(Tuple<XModule, XModuleAttribute, bool> modulePack, IHTMLElement combobox)
        {
            var attribute = modulePack.Item3 ? modulePack.Item1.CreateModuleAttribute() : modulePack.Item2.CreateModuleAttribute();

            var attrName = combobox.getAttribute("value") ?? combobox.getAttribute("name");


            attribute.Name = attrName;

            attribute.Cardinality = "0-1";
            attribute.BusinessType = "ComboBox";
            attribute.DefaultActionMode = XTestStepActionMode.Input;

            AddBusinessParam(attribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(attribute.CreateConfigurationParam(), "BusinessAssociation", "Descendants");

            foreach (var technical in Scrapper.DecideForTechnicals(combobox))
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
