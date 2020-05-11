using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MailRuTests2
{
    public abstract class BaseEmailTest
    {
        protected static IEnumerable EmailParameters
        {
            get
            {
                return GetEmailParametersFromXml();
            }
        }

 /*       private static IEnumerable GetEmailParametersFromXml()
        {
            var xmlDocument = XDocument.Load(@"C:\Users\77611\source\repos\MailRuTests\MailRuTests\testdata.xml");

            var result = xmlDocument.Descendants("vars").Select(element => new object[]
            {
                element.Attribute("addressTo")?.Value,
                element.Attribute("subject")?.Value,
                element.Attribute("body")?.Value
            });

            return result;
        }
*/
        private static IEnumerable GetEmailParametersFromXml()
        {
            // Получение расположения исполняемой сборки (dll-ки).
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var xmlDocumentPath = Path.Combine(assemblyDirectory, "testdata.xml");

            var xmlDocument = XDocument.Load(xmlDocumentPath);

            var result = xmlDocument.Descendants("vars").Select(element => new object[]
            {
                element.Attribute("addressTo")?.Value,
                element.Attribute("subject")?.Value,
                element.Attribute("body")?.Value
            });

            return result;
        }
    }
}