using System.Windows.Forms;
using Tricentis.TCAPIObjects.Objects;

namespace ToscaScrapper.InternalObjects
{
    class Iframe
    {
        public XModuleAttribute SetupAttributes(XModule mainModule, HtmlElement iframe)
        {


            XModuleAttribute iframeAttribute= mainModule.CreateModuleAttribute();

            iframeAttribute.Name = iframe.TagName;
            iframeAttribute.DefaultActionMode = XTestStepActionMode.Select;
            iframeAttribute.Cardinality = "0-1";
            iframeAttribute.BusinessType = "HtmlFrame";


            AddBusinessParam(iframeAttribute.CreateConfigurationParam(), "Engine", "Html");
            AddBusinessParam(iframeAttribute.CreateConfigurationParam(), "BusinessAssociation", "Descendants");
            AddBusinessParam(iframeAttribute.CreateTechnicalIDParam(),"Id", iframe.Id);
            AddBusinessParam(iframeAttribute.CreateTechnicalIDParam(), "Tag", iframe.TagName);

            var htmlDocumentAttr= iframeAttribute.CreateModuleAttribute();

            htmlDocumentAttr.Name = "HtmlDocument";
            htmlDocumentAttr.BusinessType = "HtmlDocument";
            htmlDocumentAttr.Cardinality = "0-1";
            htmlDocumentAttr.DefaultActionMode = XTestStepActionMode.Select;
            AddBusinessParam(htmlDocumentAttr.CreateConfigurationParam(), "BusinessAssociation", "Descendants");
            AddBusinessParam(htmlDocumentAttr.CreateConfigurationParam(), "Engine", "Html");

            return htmlDocumentAttr;
        }

        public void AddBusinessParam(XParam param, string name, string value)
        {
            param.Name = name;
            param.Value = value;
        }

    }
}
