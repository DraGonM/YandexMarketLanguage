using System;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    public class offerCustom : offer
    {
        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public offerCustom() {}

        public offerCustom(string id, decimal price, CurrencyEnum currencyId, int categoryId, string name, string ostatok, decimal price_opt) 
            : base(id, price, currencyId, categoryId, name)
        {
            this.ostatok = ostatok;
            this.price_opt = price_opt;
        }

        /// <summary>
        ///     Price of product ОПТ
        /// </summary>
        public decimal price_opt { get; set; }

        /// <summary>
        ///     Ostatok
        /// </summary>
        public string ostatok { get; set; }
    }
}
