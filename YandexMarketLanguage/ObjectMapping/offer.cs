using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace YandexMarketLanguage.ObjectMapping
{
    //TODO more tests?
    //TODO url required?
    /// <summary>
    ///     Product offer
    /// </summary>
    [XmlInclude(typeof(offerCustom))]
    [Serializable]
    public class offer
    {
        private bool? _available;
        private bool? _delivery;
        private string _description;
        private string _dimensions;
        private bool? _downloadable;
        private string _expiry;
        private string _id;
        private bool? _manufacturerWarranty;
        private string _model;
        private string _name;
        private decimal? _oldprice;
        private bool? _pickup;
        private string _picture;
        private string _rec;
        private string _salesNotes;
        private bool? _store;
        private string _typePrefix;
        private string _url;
        private string _weight;

        /// <summary>
        ///     DO NOT USE, need only for XmlSerializer
        /// </summary>
        [Obsolete]
        public offer() {}

        private offer(string id, decimal price, CurrencyEnum currencyId, int categoryId)
        {
            this.id = id;
            this.price = price;
            this.currencyId = currencyId;
            this.categoryId = categoryId;
        }

        /// <summary>
        ///     Product simple offer
        /// </summary>
        public offer(string id, decimal price, CurrencyEnum currencyId, int categoryId, string name)
            : this(id, price, currencyId, categoryId)
        {
            this.name = name;
        }

        /// <summary>
        ///     Product vendor offer
        /// </summary>
        [SuppressMessage("ReSharper", "ArrangeThisQualifier")]
        public offer(string id, decimal price, CurrencyEnum currencyId, int categoryId, string typePrefix, string vendor, string model)
            : this(id, price, currencyId, categoryId)
        {
            this.type = "vendor.model";

            this.typePrefix = typePrefix;
            this.vendor = vendor;
            this.model = model;
        }

        /// <summary>
        ///     ID offer, attribute
        /// </summary>
        [XmlAttribute]
        public string id
        {
            get { return _id; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("id must not be empty");

                _id = value;
            }
        }

        /// <summary>
        ///     Type of offer (simple or vendor), attribute
        /// </summary>
        [XmlAttribute]
        public string type { get; set; }

        /// <summary>
        ///     Term of delivery of the goods at the point of pickup, bool,
        ///     true - within two working days after ordering,
        ///     false - in a period of three working days to two months
        /// </summary>
        [XmlAttribute]
        public string available
        {
            get { return _available.HasValue ? _available.ToString() : null; }
            set { _available = bool.Parse(value); }
        }

        /// <summary>
        ///     Link on product (max length 512)
        /// </summary>
        public string url
        {
            get { return _url; }
            set
            {
                if (value.Length > 512)
                    throw new ArgumentException("max URL length 512");

                _url = url;
            }
        }

        /// <summary>
        ///     Price of product
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        ///     Old price of product (discount calculation)
        /// </summary>
        public string oldprice
        {
            get { return _oldprice.HasValue ? _oldprice.ToString() : null; }
            set { _oldprice = decimal.Parse(value); }
        }

        /// <summary>
        ///     Currency identifier product (RUR, USD, UAH, KZT)
        /// </summary>
        public CurrencyEnum currencyId { get; set; }

        /// <summary>
        ///     Category identifier product
        /// </summary>
        public int categoryId { get; set; }

        /// <summary>
        ///     Path to category of product
        ///     https://download.cdn.yandex.net/market/market_categories.xls
        /// </summary>
        public string market_category { get; set; }

        /// <summary>
        ///     Picture link on product (max length 512)
        /// </summary>
        public string picture
        {
            get { return _picture; }
            set
            {
                if (value.Length > 512)
                    throw new ArgumentException("max URL length 512");

                _picture = value;
            }
        }

        /// <summary>
        ///     can be purchased in real store, bool
        /// </summary>
        public string store
        {
            get { return _store.HasValue ? _store.ToString() : null; }
            set { _store = bool.Parse(value); }
        }

        /// <summary>
        ///     can be reserved (pickup), bool
        /// </summary>
        public string pickup
        {
            get { return _pickup.HasValue ? _pickup.ToString() : null; }
            set { _pickup = bool.Parse(value); }
        }

        /// <summary>
        ///     can be delivered, bool
        /// </summary>
        public string delivery
        {
            get { return _delivery.HasValue ? _delivery.ToString() : null; }
            set { _delivery = bool.Parse(value); }
        }

        /// <summary>
        ///     array of delivery options
        /// </summary>
        [XmlArray("delivery-options")]
        public delivery_option[] delivery_options { get; set; }

        /// <summary>
        ///     Name of product
        ///     <para>ONLY for simple offer</para>
        /// </summary>
        public string name
        {
            get { return _name; }
            set
            {
                if (typePrefix != null)
                    throw new ArgumentException("'name' must be used ONLY for simple offer");

                _name = value;
            }
        }

        /// <summary>
        ///     Type / category of product
        ///     <para>ONLY for vendor offer</para>
        /// </summary>
        public string typePrefix
        {
            get { return _typePrefix; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'typePrefix' must be used ONLY for vendor offer");

                _typePrefix = value;
            }
        }

        /// <summary>
        ///     Vendor of product
        /// </summary>
        public string vendor { get; set; }

        /// <summary>
        ///     Vendor code of product
        /// </summary>
        public string vendorCode { get; set; }

        /// <summary>
        ///     Model of product
        ///     <para>ONLY for vendor offer</para>
        /// </summary>
        public string model
        {
            get { return _model; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'model' must be used ONLY for vendor offer");

                _model = value;
            }
        }

        /// <summary>
        ///     description of product (max length 175)
        /// </summary>
        public string description
        {
            get { return _description; }
            set
            {
                if (value.Length > 175)
                    throw new ArgumentException("max description length 175");

                _description = value;
            }
        }

        /// <summary>
        ///     the minimum order amount, minimum consignment, prepayment required (element required) (max length 50)
        /// </summary>
        public string sales_notes
        {
            get { return _salesNotes; }
            set
            {
                if (value.Length > 50)
                    throw new ArgumentException("max sales notes length 50");

                _salesNotes = value;
            }
        }

        /// <summary>
        ///     has official manufacturer warranty, bool
        /// </summary>
        public string manufacturer_warranty
        {
            get { return _manufacturerWarranty.HasValue ? _manufacturerWarranty.ToString() : null; }
            set { _manufacturerWarranty = bool.Parse(value); }
        }

        /// <summary>
        ///     country of manufacturer
        ///     https://yandex.st/market-export/97.0516af5f/partner/help/Countries.pdf
        /// </summary>
        public string country_of_origin { get; set; }

        /// <summary>
        ///     Downloadable product, bool
        ///     <para>ONLY for vendor offer</para>
        /// </summary>
        public string downloadable
        {
            get { return _downloadable.HasValue ? _downloadable.ToString() : null; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'downloadable' must be used ONLY for vendor offer");

                _downloadable = bool.Parse(value);
            }
        }

        /// <summary>
        ///     sex related product
        /// </summary>
        public string adult { get; set; }

        /// <summary>
        ///     age category of goods
        /// </summary>
        public age age { get; set; }

        /// <summary>
        ///     array of manufacturer barcodes
        /// </summary>
        [XmlElement]
        public string[] barcode { get; set; }

        /// <summary>
        ///     can be purchased in yandex market (1 - true / 0 - false)
        /// </summary>
        public string cpa { get; set; }

        /// <summary>
        ///     recommended products for purchase with this
        ///     <para>ONLY for vendor offer</para>
        /// </summary>
        public string rec
        {
            get { return _rec; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'rec' must be used ONLY for vendor offer");

                _rec = value;
            }
        }

        /// <summary>
        ///     element indicates the expiration date / service life, ISO8601
        ///     <para>ONLY for vendor offer</para>
        /// </summary>
        public string expiry
        {
            get { return _expiry; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'expiry' must be used ONLY for vendor offer");

                _expiry = value;
            }
        }

        /// <summary>
        ///     weight of product with packaging (kilograms, ex: 2.07)
        ///     <para>ONLY for vendor offer</para>
        /// </summary>
        public string weight
        {
            get { return _weight; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'weight' must be used ONLY for vendor offer");

                _weight = value;
            }
        }

        /// <summary>
        ///     dimensions of product with packaging (centimeters, ex: 100/25.45/11.112)
        ///     <para>ONLY for vendor offer</para>
        /// </summary>
        public string dimensions
        {
            get { return _dimensions; }
            set
            {
                if (name != null)
                    throw new ArgumentException("'dimensions' must be used ONLY for vendor offer");

                _dimensions = value;
            }
        }

        /// <summary>
        ///     array of product specifications
        /// </summary>
        [XmlElement]
        public param[] param { get; set; }
    }
}