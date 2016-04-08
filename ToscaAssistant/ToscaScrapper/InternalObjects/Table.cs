using System;
using System.Windows.Forms;
using mshtml;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaScrapper.InternalObjects
{
    class Table
    {
        public void SetupAttributes(Tuple<XModule, XModuleAttribute, bool> modulePack, IHTMLElement table)
        {
            var attribute = modulePack.Item3 ? modulePack.Item1.CreateModuleAttribute() : modulePack.Item2.CreateModuleAttribute();

            var tableName = "TABLE";

            var takeOuterHtml = table.id != "";

            var idType = "OuterHtml";
            var tableTechId = table.outerHTML;

            if (takeOuterHtml)
            {
                tableName = table.id;
                idType = "Id";
            }

            attribute.Name = tableName;
            attribute.BusinessType = "Table";
            attribute.DefaultActionMode = XTestStepActionMode.Select;

            AddBusinessParam(attribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(attribute.CreateTechnicalIDParam(), idType, tableTechId);
            AddBusinessParam(attribute.CreateTechnicalIDParam(), "Tag", "TABLE");
        }

        public void AddBusinessParam(XParam param, string name, string value)
        {
            param.Name = name;
            param.Value = value;
        }

        public void SetupRowAttributes(XModuleAttribute attribute, HtmlElement table)
        {
            attribute.Name = "<Row>";
            attribute.DefaultDataType = ModuleAttributeDataType.String;
            attribute.Cardinality = "0-N";
            attribute.BusinessType = "Row";
            attribute.DefaultActionMode = XTestStepActionMode.Select;

            AddBusinessParam(attribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(attribute.CreateConfigurationParam(), "BusinessAssociation", "Rows");
            AddBusinessParam(attribute.CreateConfigurationParam(), "ExplicitName", "#1;#<n>;#last");
        }

        public void SetupColAttributes(XModuleAttribute attribute, HtmlElement table)
        {
            attribute.Name = "<Column>";
            attribute.DefaultDataType = ModuleAttributeDataType.String;
            attribute.Cardinality = "0-N";
            attribute.BusinessType = "Column";
            attribute.DefaultActionMode = XTestStepActionMode.Select;

            AddBusinessParam(attribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(attribute.CreateConfigurationParam(), "BusinessAssociation", "Column");
            AddBusinessParam(attribute.CreateConfigurationParam(), "ExplicitName", "#1;#<n>;#last");
        }

        public void SetupCellAttributes(XModuleAttribute attribute, HtmlElement table)
        {
            attribute.Name = "<Cell>";
            attribute.DefaultDataType = ModuleAttributeDataType.String;
            attribute.Cardinality = "0-N";
            attribute.BusinessType = "Cell";
            attribute.DefaultActionMode = XTestStepActionMode.Verify;

            AddBusinessParam(attribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(attribute.CreateConfigurationParam(), "BusinessAssociation", "Cells");
            AddBusinessParam(attribute.CreateConfigurationParam(), "ExplicitName", "#1;#<n>;#last");
        }

    }



}
