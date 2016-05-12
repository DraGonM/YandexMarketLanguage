using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using YandexMarketLanguage;
using YandexMarketLanguage.ObjectMapping;
// ReSharper disable RedundantArgumentNameForLiteralExpression
// ReSharper disable RedundantArgumentName

namespace YandexMarketLanguageTests.IntegrationTests
{
    [TestFixture]
    public class IntegrationTests
    {
        private yml_catalog _ymlCatalog;

        [SetUp]
        public void SetUp()
        {
            var shop = new shop("BestShop",
                "Best online seller Inc.",
                "http://best.seller.ru/",
                new[]
                {
                    new currency(CurrencyEnum.RUR, rate: 1),
                    new currency(CurrencyEnum.EUR, RateEnum.CBRF),
                },
                new[]
                {
                    new category(id: 1, name: "Книги"),
                    new category(id: 2, name: "Детективы", parentId: 1),
                },
                new[]
                {
                    new delivery_option(cost: 300, workDays: 1),
                    new delivery_option(cost: 0, workDaysFrom: 5, workDaysTo: 7, orderBefore: 14),
                },
                new[]
                {
                    new offer(id: "12346", price: 600, currencyId: CurrencyEnum.EUR, categoryId: 1, name: "Наручные часы Casio A1234567B"),
                    new offerCustom(id: "777", price: 600, currencyId: CurrencyEnum.EUR, categoryId: 1, name: "TEST", ostatok: "12", price_opt: 504),
                    new offer(id: "12341", price: 16800, currencyId: CurrencyEnum.RUR, categoryId: 2, typePrefix: "Принтер", vendor: "HP", model: "Deskjet D2663")
                    {
                        vendorCode = "CH366C",
                        param = new[]
                        {
                            new param("Максимальный формат", "A4"),
                            new param("Максимальное разрешение для ч/б печати", "600x600", "dpi"), 
                            new param("Плотность бумаги", "75-280", "г/м2"), 
                        },
                    },
                })
            {
                platform = "CMS",
                version = "2.3",
                agency = "Agency",
                email = "CMS@CMS.ru",
                cpa = "0",
            };

            _ymlCatalog = new yml_catalog(new DateTime(2010, 04, 01, 17, 05, 00), shop);
        }

        [Test]
        public void YmlSerializer_ToXmlString_Test()
        {
            var xmlStringFromObject = new YmlSerializer().ToXmlString(_ymlCatalog);

            xmlStringFromObject.Should().NotBeNullOrWhiteSpace();

            var xmlStringStandart = ReadXmlFromAssembly();

            xmlStringStandart.Should().NotBeNullOrWhiteSpace();

            xmlStringFromObject.ShouldBeEquivalentTo(xmlStringStandart);
        }

        [Test]
        public void YmlSerializer_FromXmlString_Test()
        {
            var xmlStringStandart = ReadXmlFromAssembly();

            var ymlCatalog = new YmlSerializer().FromXmlString<yml_catalog>(xmlStringStandart);

            ymlCatalog.ShouldBeEquivalentTo(_ymlCatalog);
        }
        
        private static string ReadXmlFromAssembly()
        {
            string xmlStringStandart;

            // ReSharper disable once AssignNullToNotNullAttribute
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("YandexMarketLanguageTests.IntegrationTests.standart.xml"))
            {
                using (var reader = new StreamReader(stream))
                {
                    xmlStringStandart = reader.ReadToEnd();
                }
            }

            return xmlStringStandart;
        }
    }
}