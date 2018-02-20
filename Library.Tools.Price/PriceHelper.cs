using Library.Tools.Attributes;
using Library.Tools.Extensions;
using Library.Tools.Tasks;
using Library.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Tools.Price
{
    public class PriceHelper : ProgressableCancellable
    {
        #region Public CONSTRUCTORS

        /// <summary>
        /// Permet les conversions de devise via récupération du taux sur internet
        /// </summary>
        public PriceHelper(ProgressCancelNotifier iNotifier, bool iWithSafeNetwork)
            : base(iNotifier)
        {
            WithSafeNetwork = iWithSafeNetwork;
        }

        #endregion

        #region Public METHODS

        /// <summary>
        /// Retourne le prix avec convertion de la tva pour une TVA sur CA
        /// </summary>
        /// <param name="iPrice"></param>
        /// <param name="iFromVATRate"></param>
        /// <param name="iToVATRate"></param>
        /// <returns></returns>
        public static decimal ConvertVATRateOnRevenu(decimal iPrice, decimal iFromVATRate, decimal iToVATRate)
        {
            if (iFromVATRate != iToVATRate)
            {
                //enleve la taxe déjà appliqué
                iPrice = Math.Round(iPrice / (1 + iFromVATRate / 100), 2);

                //rajout la nouvelle taxe
                iPrice = Math.Round(iPrice * (1 + iToVATRate / 100), 2);
            }
            return iPrice.Round2();
        }

        /// <summary>
        /// Retourne le montant de TVA seul depuis un prix HT pour une TVA sur CA
        /// </summary>
        /// <param name="iPriceHt"></param>
        /// <param name="iVATRate"></param>
        /// <returns></returns>
        public static decimal GetVATPriceOnRevenu(decimal iPriceHt, decimal iVATRate)
        {
            if (iVATRate <= 0)
                throw new ArgumentNullException();

            return decimal.Multiply(iPriceHt, decimal.Divide(iVATRate, 100)).Round2();
        }

        /// <summary>
        /// Retourne le montant de TVA seul depuis un prix TTC pour une TVA sur CA
        /// </summary>
        /// <param name="iPriceHt"></param>
        /// <param name="iVATRate"></param>
        /// <returns></returns>
        public static decimal GetVATPriceOnRevenuWithSellingPriceTTC(decimal iPriceTTC, decimal iVATRate)
        {
            if (iVATRate <= 0)
                throw new ArgumentNullException();
            var priceHT = decimal.Divide(iPriceTTC, 1 + decimal.Divide(iVATRate, 100)).Round2();

            return (iPriceTTC - priceHT).Round2();
        }

        /// <summary>
        /// Retourne le montant de TVA seul depuis un prix d'achat et prix de vente HT sur une TVA sur profit
        /// </summary>
        /// <param name="iBuyingPriceHt"></param>
        /// <param name="iSellingPriceHt"></param>
        /// <param name="iVATRate"></param>
        /// <returns></returns>
        public static decimal GetVATPriceOnProfit(decimal iBuyingPriceHt, decimal iSellingPriceHt, decimal iVATRate)
        {
            if (iSellingPriceHt <= iBuyingPriceHt)
                throw new ArgumentException("Le prix de vente ne peut pas être être inférieur ou égal au prix d'achat");

            if (iVATRate <= 0) throw new ArgumentException("Le taux de TVA doit être supérieur à zéro");

            return decimal.Multiply(iSellingPriceHt - iBuyingPriceHt, decimal.Divide(iVATRate, 100)).Round2();
        }

        /// <summary>
        /// Retourne le montant de TVA seul depuis un prix d'achat et prix de vente TTC sur une TVA sur profit
        /// </summary>
        /// <param name="iBuyingPriceHt"></param>
        /// <param name="iSellingPriceTTC"></param>
        /// <param name="iVATRate"></param>
        /// <returns></returns>
        public static decimal GetVATPriceOnProfitWithSellingPriceTTC(decimal iBuyingPriceHt, decimal iSellingPriceTTC, decimal iVATRate)
        {
            if (iSellingPriceTTC <= iBuyingPriceHt)
                throw new ArgumentException("Le prix de vente ne peut pas être être inférieur ou égal au prix d'achat");

            if (iVATRate <= 0) throw new ArgumentException("Le taux de TVA doit être supérieur à zéro");

            return decimal.Divide(decimal.Multiply((iSellingPriceTTC - iBuyingPriceHt), decimal.Divide(iVATRate, 100)), (1 + decimal.Divide(iVATRate, 100))).Round2();
        }

        /// <summary>
        /// retourne le montant HT pour une tva sur le CA
        /// </summary>
        public static decimal GetPriceHTFromPriceTTCOnRevenu(decimal iTTCAmount, decimal iVATRate)
        {
            return (iTTCAmount / (1 + (iVATRate / 100))).Round2();
        }

        /// <summary>
        /// retourne le montant TTC pour une tva sur le CA
        /// </summary>
        public static decimal GetPriceTTCFromPriceHTOnRevenu(decimal iHTAmount, decimal iVATRate)
        {
            return (iHTAmount * (1 + iVATRate / 100)).Round2();
        }

        /// <summary>
        /// retourne le montant HT pour une tva sur le bénéfice
        /// </summary>
        public static decimal GetPriceHTFromPriceTTCOnProfit(decimal iTTCAmount, decimal iBuyingAmount, decimal iVATRate)
        {
            return ((iTTCAmount + iBuyingAmount * (iVATRate / 100)) / (1 + iVATRate / 100)).Round2();
        }

        /// <summary>
        /// retourne le montant TTC pour une tva sur le bénéfice
        /// </summary>
        public static decimal GetPriceTTCFromPriceHTOnProfit(decimal iHTAmount, decimal iBuyingAmount, decimal iVATRate)
        {
            return (iHTAmount + (iHTAmount - iBuyingAmount) * (iVATRate / 100)).Round2();
        }

        /// <summary>
        /// Retourne le prix avec convertion de la tva pour une TVA sur marge
        /// </summary>
        /// <param name="iPrice"></param>
        /// <param name="iFromVATRate"></param>
        /// <param name="iToVATRate"></param>
        /// <returns></returns>
        public static decimal ConvertVATRateOnProfit(decimal iSellingPrice, decimal iBuyingPrice, decimal iFromVATRate, decimal iToVATRate)
        {
            if (iBuyingPrice == 0) throw new ArgumentNullException("Le prix d'achat ne peut pas être null ou égal à zéro");
            if (iSellingPrice <= iBuyingPrice) throw new ArgumentNullException("Le prix de vente ne peut pas être inférieur ou égal au prix d'achat");

            if (iFromVATRate != iToVATRate)
            {
                decimal sellingPrice = iSellingPrice;
                decimal profitPrice = iSellingPrice - iBuyingPrice;

                //enleve la taxe déjà appliqué
                profitPrice = Math.Round(profitPrice / (1 + iFromVATRate / 100), 2);

                //rajout la nouvelle taxe
                profitPrice = Math.Round(profitPrice * (1 + iToVATRate / 100), 2);
                return (iBuyingPrice + profitPrice).Round2();
            }
            return (iSellingPrice).Round2();
        }

        /// <summary>
        /// Applique un taux de marque, Le Profit represente le pourcentage par rapport au prix retourné.
        /// </summary>
        public static decimal ApplyProfit(decimal iAmount, decimal iPercentage)
        {
            if (iPercentage < 0) throw new Exception("Le pourcentage ne peut pas être inférieur à 0");

            return decimal.Divide(iAmount, (1 - (iPercentage / 100))).Round2();
        }

        /// <summary>
        /// Retourne un prix remisé
        /// </summary>
        /// <param name="iAmount"></param>
        /// <param name="iPercentage"></param>
        /// <returns></returns>
        public static decimal ApplyDiscount(decimal iAmount, decimal iPercentage)
        {
            if (iPercentage < 0) throw new Exception("Le pourcentage ne peut pas être inférieur à 0");
            return (iAmount * (1 - (iPercentage / 100))).Round2();
        }

        /// <summary>
        /// Retourne le coût de revient
        /// </summary>
        public static decimal GetCost(decimal iBuyingPriceHT, decimal iSellingPriceTTC, decimal iShippingAmountTTC, decimal iVariableFeePercentage, decimal iFixedFeeAmount)
        {
            return (iBuyingPriceHT + GetFeeAmount(iSellingPriceTTC, iShippingAmountTTC, iVariableFeePercentage, iFixedFeeAmount)).Round2();
        }

        /// <summary>
        /// Retourne le montant des frais
        /// </summary>
        public static decimal GetFeeAmount(decimal iSellingAmountTTC, decimal iShippingAmountTTC, decimal iVariableFeePercentage, decimal iFixedFeeAmount)
        {
            return ((iSellingAmountTTC + iShippingAmountTTC) * (iVariableFeePercentage / 100) + iFixedFeeAmount).Round2();
        }

        /// <summary>
        /// Retourne le pourcentage des frais
        /// </summary>
        public static decimal GetFeePercentage(decimal iAmountTTC, decimal iFeeAmount)
        {
            return ((iFeeAmount / iAmountTTC) * 100).Round2();
        }

        /// <summary>
        /// le taux de marque permet de calculer la marge directement depuis le prix de vente HT. Pvht * Tmq = la marge
        /// </summary>
        public static decimal GetSellingAmount(VATEnum iResultTypePrice, decimal iNetMarkRatePercentage, decimal iBuyingAmount, decimal iShippingAmountTTC, decimal iVariableFeePercentage, decimal iFixedFeeAmount, decimal iVATRate, ConditionEnum iCondition)
        {
            //TVA sur CA
            if (iCondition == ConditionEnum.UNUSED)
            {
                decimal sellingAmountHT = (iBuyingAmount + (iShippingAmountTTC * iVariableFeePercentage / 100) + iFixedFeeAmount) / (1 - iNetMarkRatePercentage / 100 - (1 + iVATRate / 100) * iVariableFeePercentage / 100);

                //retourne le TTC
                if (iResultTypePrice == VATEnum.TTC)
                    return (sellingAmountHT * (1 + (iVATRate / 100))).Round2();
                //Retourne le HT
                else if (iResultTypePrice == VATEnum.HT)
                    return sellingAmountHT.Round2();
                else
                    throw new NotSupportedException(iResultTypePrice.ToStringWithEnumName());

                //Prht = Paht + Fv + Ff  avec Prht = pvht(1-tmq)
                //Pvht = (Paht + Lvttc*Tfv +FF)/(1 - tmq - (1+TVA)(Tfv))   avec Pvttc = pvht *(1+TVA)
            }

                //Tva sur marge
            else if (iCondition == ConditionEnum.USED)
            {
                decimal sellingPriceHT = (iBuyingAmount + (iShippingAmountTTC - iBuyingAmount * (iVATRate / 100)) * (iVariableFeePercentage / 100) + iFixedFeeAmount) / (1 - iNetMarkRatePercentage / 100 - (1 + iVATRate / 100) * iVariableFeePercentage / 100);

                //retourne le TTC
                if (iResultTypePrice == VATEnum.TTC)
                    return (sellingPriceHT + (sellingPriceHT - iBuyingAmount) * (iVATRate / 100)).Round2();
                //Retourne le HT
                else if (iResultTypePrice == VATEnum.HT)
                    return (sellingPriceHT).Round2();
                else
                    throw new NotSupportedException(iResultTypePrice.ToStringWithEnumName());

                //Prht = Paht + Fv + Ff   avec Pvttc = Pvht + (pvht - paht)*TVA  avec Prht = pvht(1-tmq)
                //pvht = Paht + (Lvttc - Paht*TVA)Tfv + Ff)/(1-Tmq - (1+tva)Tfv)
            }
            else
            {
                throw new NotSupportedException(iCondition.ToStringWithEnumName());
            }
        }

        /// <summary>
        /// Retourne le bénéfice
        /// </summary>
        public static decimal GetProfitAmount(decimal iBuyingAmount, decimal iFeeAmount, decimal iSellingAmountHT)
        {
            return (iSellingAmountHT - iBuyingAmount - iFeeAmount).Round2();
        }

        /// <summary>
        /// Retourne le bénéfice
        /// </summary>
        public static decimal GetNetMarkRatePercentage(decimal iBuyingAmount, decimal iFeeAmount, decimal iSellingAmountHT)
        {
            return ((1 - (iBuyingAmount + iFeeAmount) / iSellingAmountHT) * 100).Round2();
        }

        /// <summary>
        /// Conversion tva sur CA + devise
        /// </summary>
        /// <param name="iPrice"></param>
        /// <param name="iFromVATRate"></param>
        /// <param name="iToVATRate"></param>
        /// <returns></returns>
        public async Task<decimal> ConvertVATRateOnRevenuCurrencyAsync(decimal iPrice, decimal iFromVATRate, decimal iToVATRate, string iFromCurrency, string iToCurrency)
        {
            if (iFromCurrency.IsNotNullAndNotEmpty() || iToCurrency.IsNotNullAndNotEmpty()) throw new Exception();

            var task = await ConvertCurrencyAsync(ConvertVATRateOnRevenu(iPrice, iFromVATRate, iToVATRate), iFromCurrency, iToCurrency);
            return task.Round2();
        }

        /// <summary>
        /// Conversion tva sur marge + devise
        /// </summary>
        /// <param name="iPrice"></param>
        /// <param name="iFromVATRate"></param>
        /// <param name="iToVATRate"></param>
        /// <returns></returns>
        public async Task<decimal> ConvertVATRateOnProfitCurrencyAsync(decimal iPrice, decimal iBuyingPrice, decimal iFromVATRate, decimal iToVATRate, string iFromCurrency, string iToCurrency)
        {
            if (iFromCurrency.IsNotNullAndNotEmpty() || iToCurrency.IsNotNullAndNotEmpty()) throw new Exception();

            var task = await ConvertCurrencyAsync(ConvertVATRateOnProfit(iPrice, iBuyingPrice, iFromVATRate, iToVATRate), iFromCurrency, iToCurrency);
            return task.Round2();
        }

        /// <summary>
        /// Convertion de la devise
        /// </summary>
        /// <param name="iAmount"></param>
        /// <param name="iFromCurrency"></param>
        /// <param name="iToCurrency"></param>
        /// <returns></returns>
        public async Task<decimal> ConvertCurrencyAsync(decimal iAmount, string iFromCurrency, string iToCurrency)
        {
            if (iFromCurrency == null ||
                iToCurrency == null) throw new ArgumentNullException();

            if (iFromCurrency != iToCurrency)
                return (iAmount * await GetExchangeRateAsync(iFromCurrency, iToCurrency)).Round2();

            return iAmount;
        }

        #endregion

        #region Public ENUMS

        public enum ConditionEnum
        {
            [Name("FR", "Neuf")]
            UNUSED = 1,

            [Name("FR", "Occasion")]
            USED = 2
        };

        #endregion

        #region Private FIELDS

        private const string CONVERTURL = "https://www.google.com/finance/converter?a=1&from={0}&to={1}";
        static private Dictionary<string, decimal> ExchangeRateDic;
        static private DateTime UpdateDate = DateTime.Now;
        private bool WithSafeNetwork;

        #endregion

        #region Private METHODS

        private async Task<decimal> GetExchangeRateAsync(string iFromCurrency, string iToCurrency)
        {
            if (UpdateDate.Date != DateTime.Now.Date || ExchangeRateDic == null)
                ExchangeRateDic = new Dictionary<string, decimal>();

            string key = iFromCurrency + iToCurrency;

            if (ExchangeRateDic.ContainsKey(key) == false)
                ExchangeRateDic.Add(key, await GetExchangeRateFromWebAsync(iFromCurrency, iToCurrency));

            return ExchangeRateDic[key];
        }

        private async Task<decimal> GetExchangeRateFromWebAsync(string iFromCurrency, string iToCurrency)
        {
            Uri url = new Uri(CONVERTURL.FormatString(iFromCurrency, iToCurrency));
            var weQuery = new WebQuery(Notifier, false, WithSafeNetwork);

            string htmlResponse = await weQuery.GetSourceAsync(url);

            HtmlAgilityPack.HtmlDocument theDocument = new HtmlAgilityPack.HtmlDocument();
            theDocument.LoadHtml(htmlResponse);

            return Convert.ToDecimal(theDocument.DocumentNode.SelectSingleNode(".//span[@class='bld']").InnerText.Split(' ').First().Replace(".", ","));
        }

        #endregion
    }
}