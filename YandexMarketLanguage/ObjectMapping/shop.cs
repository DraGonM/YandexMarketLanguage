using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    [Serializable]
    [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
    public class shop
    {
        private string _name;
        private string _company;
        private string _url;
        private currency[] _currencies;
        private category[] _categories;
        private delivery_option[] _deliveryOptions;
        private offer[] _offers;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public shop() {}

        public shop(string name, string company, string url, currency[] currencies, category[] categories, delivery_option[] deliveryOptions, offer[] offers)
        {
            this.name = name;
            this.company = company;
            this.url = url;
            this.currencies = currencies;
            this.categories = categories;
            this.delivery_options = deliveryOptions;
            this.offers = offers;
        }

        public string name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("name must not be empty");

                if (value.Length > 20)
                    throw new ArgumentException("name length must be <= 20");

                _name = value;
            }
        }

        public string company
        {
            get { return _company; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("company must not be empty"); 
                
                _company = value;
            }
        }

        public string url
        {
            get { return _url; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("url must not be empty"); 
                
                _url = value;
            }
        }

        public string platform { get; set; }

        public string version { get; set; }

        public string agency { get; set; }

        public string email { get; set; }

        public currency[] currencies
        {
            get { return _currencies; }
            set
            {
                if (value == null)
                    throw new ArgumentException("currencies must not be null");

                _currencies = value;
            }
        }

        public category[] categories
        {
            get { return _categories; }
            set
            {
                if (value == null)
                    throw new ArgumentException("categories must not be null");

                _categories = value;
            }
        }

        [XmlArray("delivery-options")]
        public delivery_option[] delivery_options
        {
            get { return _deliveryOptions; }
            set
            {
//                if (value == null)
//                    throw new ArgumentException("delivery_options must not be null");

                _deliveryOptions = value;
            }
        }

        public string cpa { get; set; }

        public offer[] offers
        {
            get { return _offers; }
            set
            {
                if (value == null)
                    throw new ArgumentException("offers must not be null");

                _offers = value;
            }
        }
    }
}