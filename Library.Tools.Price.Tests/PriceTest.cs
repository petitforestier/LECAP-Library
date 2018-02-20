using Library.Tools.Price;
using Library.Tools.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Library.Tools.Extensions;

namespace Library.Tests
{
    [TestClass]
    public class PriceTest
    {
        #region Public METHODS

        [TestMethod]
        public void Convert()
        {
            var notifier = new ProgressCancelNotifier();

            decimal vatRate = 20;

            //TVA sur CA
            decimal amountTTC = 10;
            decimal amountHT = 8.33m;

            Assert.AreEqual(PriceHelper.ConvertVATRateOnRevenu(amountTTC, vatRate, 0), amountHT);
            Assert.AreEqual(PriceHelper.ConvertVATRateOnRevenu(amountHT, 0, vatRate) , amountTTC);

            //TVA sur marge
            decimal sellingPriceTTC = 20;
            decimal sellingPriceHT = 18;
            decimal buyingPriceHT = 8;

             Assert.AreEqual(PriceHelper.ConvertVATRateOnProfit(sellingPriceTTC, buyingPriceHT, vatRate, 0) , sellingPriceHT);
             Assert.AreEqual(PriceHelper.ConvertVATRateOnProfit(sellingPriceHT, buyingPriceHT, 0, vatRate) , sellingPriceTTC);
             Assert.AreEqual(PriceHelper.ConvertVATRateOnProfit(sellingPriceTTC, buyingPriceHT, vatRate, vatRate) ,sellingPriceTTC);
        }

        [TestMethod]
        public void GetVatPrice()
        {
            var notifier = new ProgressCancelNotifier();

            decimal vatRate = 20;
            decimal buyingPriceHt = 100;
            decimal sellingPriceHt = 200;
            decimal vatPriceOnRevenue = 40;
            decimal sellingPriceTTCOnRevenue = 240;
            decimal vatPriceOnProfit = 20;
            decimal sellingPriceTTCOnProfit = sellingPriceHt + vatPriceOnProfit; 

            if (PriceHelper.GetVATPriceOnProfit(buyingPriceHt, sellingPriceHt, vatRate) != vatPriceOnProfit) throw new Exception();
            if (PriceHelper.GetVATPriceOnRevenu(sellingPriceHt, vatRate) != vatPriceOnRevenue) throw new Exception();

            Assert.AreEqual(vatPriceOnRevenue, PriceHelper.GetVATPriceOnRevenuWithSellingPriceTTC(sellingPriceTTCOnRevenue, vatRate));

            if (PriceHelper.GetVATPriceOnProfitWithSellingPriceTTC(buyingPriceHt, sellingPriceTTCOnProfit, vatRate) != vatPriceOnProfit) throw new Exception();
        }

        [TestMethod]
        public void Discount()
        {
            decimal beforeDiscount = 250;
            decimal afterDiscount = 230;
            decimal discountRate = 8;
            if (PriceHelper.ApplyDiscount(beforeDiscount, discountRate) != afterDiscount) throw new Exception();
        }

        [TestMethod]
        public void Profit()
        {
            decimal buyingPrice = 100;
            decimal sellingPrice = 125;
            decimal profitRate = 20;
            if (PriceHelper.ApplyProfit(buyingPrice, profitRate) != sellingPrice) throw new Exception();
        }

        [TestMethod]
        public void AllTest()
        {
            //input
            decimal buyingAmount = 100;
            decimal shippingAmountTTC = 7;
            decimal variableFeePercentage = 9;
            decimal fixedFeeAmount = 0.25m;
            decimal sellingAmountTTC = 200;
            decimal vatRate = 20;
            decimal markRate = 20;

            //result
            decimal feeAmountResult = 18.88m;
            decimal costResult = 118.88m;

            decimal sellingUnusedHtResult = 145.78m;
            decimal sellingUnusedTTCResult = 174.94m;
            decimal sellingUnusedBenefitResult = 29.16m;

            decimal sellingUsedHtResult = 143.18m;
            decimal sellingUsedTTCResult = 151.82m;
            decimal sellingUsedBenefitResult = 28.64m;


            if (PriceHelper.GetFeeAmount(sellingAmountTTC, shippingAmountTTC, variableFeePercentage, fixedFeeAmount) != feeAmountResult)
                throw new Exception();

            if (PriceHelper.GetCost(buyingAmount, sellingAmountTTC, shippingAmountTTC, variableFeePercentage, fixedFeeAmount) != costResult)
                throw new Exception();


            if (Math.Round(PriceHelper.GetSellingAmount(VATEnum.HT, markRate, buyingAmount, shippingAmountTTC, variableFeePercentage, fixedFeeAmount, vatRate,PriceHelper.ConditionEnum.UNUSED), 2) != sellingUnusedHtResult)
                throw new Exception();

            if (Math.Round(PriceHelper.GetSellingAmount(VATEnum.TTC, markRate, buyingAmount, shippingAmountTTC, variableFeePercentage, fixedFeeAmount, vatRate, PriceHelper.ConditionEnum.UNUSED), 2) != sellingUnusedTTCResult)
                throw new Exception();

            if (Math.Round(PriceHelper.GetSellingAmount(VATEnum.HT, markRate, buyingAmount, shippingAmountTTC, variableFeePercentage, fixedFeeAmount, vatRate, PriceHelper.ConditionEnum.USED), 2) != sellingUsedHtResult)
                throw new Exception();

            if (Math.Round(PriceHelper.GetSellingAmount(VATEnum.TTC, markRate, buyingAmount, shippingAmountTTC, variableFeePercentage, fixedFeeAmount, vatRate, PriceHelper.ConditionEnum.USED), 2) != sellingUsedTTCResult)
                throw new Exception();

            //SellingUsedBenefit
            if (Math.Round(PriceHelper.GetProfitAmount(buyingAmount, PriceHelper.GetFeeAmount(sellingUsedTTCResult, shippingAmountTTC, variableFeePercentage, fixedFeeAmount), sellingUsedHtResult), 2) != sellingUsedBenefitResult)
                throw new Exception();

            //SellingUnusedBenefit
            if (Math.Round(PriceHelper.GetProfitAmount(buyingAmount, PriceHelper.GetFeeAmount(sellingUnusedTTCResult, shippingAmountTTC, variableFeePercentage, fixedFeeAmount), sellingUnusedHtResult), 2) != sellingUnusedBenefitResult)
                throw new Exception();
        }

        [TestMethod]
        public void vatConverstion()
        {
            //input
            decimal buyingAmount = 100;
            decimal vatRate = 20;

            decimal CATTC = 200m;
            decimal CAHT = 166.67m;

            decimal BenefitTTC = 200m;
            decimal BenefitHT = 183.33m;

            if (Math.Round(PriceHelper.GetPriceHTFromPriceTTCOnRevenu(CATTC, vatRate), 2) != CAHT)
                throw new Exception();

            if (Math.Round(PriceHelper.GetPriceTTCFromPriceHTOnRevenu(CAHT, vatRate), 2) != CATTC)
                throw new Exception();


            if (Math.Round(PriceHelper.GetPriceHTFromPriceTTCOnProfit(BenefitTTC, buyingAmount, vatRate), 2) != BenefitHT)
                throw new Exception();

            if (Math.Round(PriceHelper.GetPriceTTCFromPriceHTOnProfit(BenefitHT, buyingAmount, vatRate), 2) != BenefitTTC)
                throw new Exception();


        }

        [TestMethod]
        public void GetNetMarkRatePercentage()
        {
            decimal buyingAmount = 100;
            decimal sellingHTAmount = 200m;
            decimal FeeAmount = 10;

            if (Math.Round(PriceHelper.GetNetMarkRatePercentage(buyingAmount, FeeAmount, sellingHTAmount), 2) != 45)
                throw new Exception();

        }

        [TestMethod]
        public void GetPercentageFee()
        {
            decimal amountTTC = 10;
            decimal feePercentage = 10;
            decimal feeAmount = 1;

            if (PriceHelper.GetFeePercentage(amountTTC, feeAmount).Round2() != feePercentage) throw new Exception();

        }

        #endregion
    }
}