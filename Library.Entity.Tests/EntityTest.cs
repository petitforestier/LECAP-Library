using System;
using System.Collections.Generic;
using System.Linq;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using Library.Tools.Network;
using Library.Tools.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;

namespace Library.Entity.Tests
{
    [TestClass]
    public class EntityTest
    {
        #region Public METHODS

        [TestMethod]
        public void Add()
        {
            try
            {

                using (var ds = new DataService())
                {
                    ds.ResetDatabase();

                    //classique
                    var newProduct = new T_E_Product();
                    newProduct.Description = "Product 1";
                    newProduct.Ranking = 1;
                    ds.Add(newProduct);

                    var productList = ds.GetList<T_E_Product>().ToList();
                    if (productList.Count != 1) throw new Exception();

                    T_E_Product getProduct = productList.Single();
                    if (newProduct.Description != getProduct.Description) throw new Exception();

                    //with error
                    var newProduct2 = new T_E_Product();
                    try
                    {
                        ds.Add(newProduct2);
                        throw new Exception();
                    }
                    catch { }

                    //classique
                    var newProduct3 = new T_E_Product();
                    newProduct3.Description = "Product 1";
                    ds.Add(newProduct3);

                    if (ds.GetList<T_E_Product>().Count() != 2) throw new Exception();
                    ds.ResetDatabase();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [TestMethod]
        public void Update()
        {
            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                //classique
                var newProduct = new T_E_Product();
                newProduct.Description = "Add Product 1";
                ds.Add(newProduct);

                var productList = ds.GetList<T_E_Product>().ToList();
                if (productList.Count != 1) throw new Exception();

                T_E_Product getProduct = productList.Single();
                if (newProduct.Description != getProduct.Description) throw new Exception();

                getProduct.Description = "Update product2";
                ds.Update(getProduct);

                T_E_Product getProduct2 = ds.GetSingle<T_E_Product>(x => x.ProductId == getProduct.ProductId);
                if (getProduct2.Description != getProduct.Description) throw new Exception();

                //with error
                try
                {
                    getProduct2.Description = null;
                    ds.Update(getProduct2);
                    throw new Exception();
                }
                catch { }

                //classique add
                getProduct2.Description = "Update product3";
                ds.Update(getProduct2);

                ds.ResetDatabase();
            }

        }

        [TestMethod]
        public void Delete()
        {

            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                //classique
                var newProduct = new T_E_Product();
                newProduct.Description = "Add Product 1";
                ds.Add(newProduct);

                var productList = ds.GetList<T_E_Product>().ToList();
                if (productList.Count != 1) throw new Exception();

                ds.Delete(newProduct);

                if (ds.GetList<T_E_Product>().Count() != 0) throw new Exception();

                ds.ResetDatabase();
            }

        }

        [TestMethod]
        public void AddList()
        {
            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                //classique
                var addList = new List<T_E_Product>();

                var newProduct1 = new T_E_Product();
                newProduct1.Description = "Product 1";
                addList.Add(newProduct1);

                var newProduct2 = new T_E_Product();
                newProduct2.Description = "Product 2";
                addList.Add(newProduct2);

                var newProduct3 = new T_E_Product();
                newProduct3.Description = "Product 3";
                addList.Add(newProduct3);

                ds.AddList(addList);

                var productList = ds.GetList<T_E_Product>().ToList();
                if (productList.Count != 3) throw new Exception();

                //avec erreur
                var addList2 = new List<T_E_Product>();
                var newProduct4 = new T_E_Product();
                newProduct4.Description = "newProduct4";
                addList2.Add(newProduct4);

                var newProduct5 = new T_E_Product();
                newProduct5.Description = null; // provoque l'erreur
                addList2.Add(newProduct5);

                try
                {
                    ds.AddList(addList2);
                    throw new Exception();
                }
                catch { }

                if (productList.Count != 3) throw new Exception();

                //classique
                var addList3 = new List<T_E_Product>();

                var newProduct6 = new T_E_Product();
                newProduct6.Description = "newProduct6";
                addList3.Add(newProduct6);

                var newProduct7 = new T_E_Product();
                newProduct7.Description = "Product 7";
                addList3.Add(newProduct7);

                ds.AddList(addList3);

                if (ds.GetList<T_E_Product>().Count() != 5) throw new Exception();
                ds.ResetDatabase();
            }
        }

        [TestMethod]
        public void DeleteList()
        {
            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                //classique
                var deleteList = new List<T_E_Product>();

                var newProduct1 = new T_E_Product();
                newProduct1.Description = "Product 1";
                deleteList.Add(newProduct1);

                var newProduct2 = new T_E_Product();
                newProduct2.Description = "Product 2";
                deleteList.Add(newProduct2);

                var newProduct3 = new T_E_Product();
                newProduct3.Description = "Product 3";
                deleteList.Add(newProduct3);

                ds.AddList(deleteList);

                var productList = ds.GetList<T_E_Product>().ToList();
                if (productList.Count != 3) throw new Exception();

                ds.DeleteList(deleteList);
                productList = null;
                productList = ds.GetList<T_E_Product>().ToList();
                if (productList.Count != 0) throw new Exception();

                ds.ResetDatabase();
            }
        }

        [TestMethod]
        public void UpdateList()
        {
            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                //classique
                var deleteList = new List<T_E_Product>();

                var newProduct1 = new T_E_Product();
                newProduct1.Description = "Product 1";
                deleteList.Add(newProduct1);

                var newProduct2 = new T_E_Product();
                newProduct2.Description = "Product 2";
                deleteList.Add(newProduct2);

                var newProduct3 = new T_E_Product();
                newProduct3.Description = "Product 3";
                deleteList.Add(newProduct3);

                ds.AddList(deleteList);

                var productList = ds.GetList<T_E_Product>().ToList();
                if (productList.Count != 3) throw new Exception();

                foreach (var item in productList)
                {
                    item.Description = "Update 4";
                }

                ds.UpdateList(productList);

                var getProductList = ds.GetList<T_E_Product>().ToList();

                //vérification de la modification
                foreach (var item in getProductList)
                {
                    if (item.Description != "Update 4")
                        throw new Exception();
                }

                //Passe avec une erreur
                getProductList.First().Description = null;

                try
                {
                    ds.UpdateList(getProductList);
                    throw new Exception();
                }
                catch { }

                getProductList.First().Description = "Update5";
                ds.UpdateList(getProductList);
                ds.ResetDatabase();
            }
        }

        [TestMethod]
        public void GetList()
        {
            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                for (int a = 1; a <= 100; a++)
                {
                    var newProduct1 = new T_E_Product();
                    newProduct1.Ranking = a;
                    newProduct1.Description = a + " Product";

                    newProduct1 = ds.Add(newProduct1);

                    var newPicture = new T_E_Picture();
                    newPicture.Name = "picture " + newProduct1.Description;
                    newPicture.ProductId = newProduct1.ProductId;

                    newPicture = ds.Add(newPicture);
                }

                //GetList<T>
                if (ds.GetList<T_E_Product>().Count() != 100) throw new Exception();
                if (ds.GetList<T_E_Product>(x => x.Description.Contains("Product")).Count() != 100) throw new Exception();

                var navPictureList = new List<System.Linq.Expressions.Expression<Func<T_E_Picture, object>>>();
                navPictureList.Add(x => x.T_E_Product);
                if (ds.GetList<T_E_Picture>(null, navPictureList).ToList().Where(x => x.T_E_Product != null).Count() != 100) throw new Exception();
                if (ds.GetList<T_E_Picture>().ToList().Enum().Where(x => x.T_E_Product != null).Count() != 0) throw new Exception();

                //GetList<T,Tkey>
                var iquery = ds.GetList<T_E_Product, int>(null, null, x => x.Ranking, System.ComponentModel.ListSortDirection.Descending);

                if (iquery.First().Ranking != 100) throw new Exception();
                if (iquery.Last().Ranking != 1) throw new Exception();

                iquery = ds.GetList<T_E_Product, int>(null, null, x => x.Ranking, System.ComponentModel.ListSortDirection.Descending, 10, 10);
                if (iquery.Count() != 10) throw new Exception();
                if (iquery.First().Ranking != 90) throw new Exception();
                if (iquery.Last().Ranking != 81) throw new Exception();

                ds.ResetDatabase();
            }
        }

        [TestMethod]
        public void GetListBatchLoading()
        {
            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                for (int a = 1; a <= 100; a++)
                {
                    var newProduct1 = new T_E_Product();
                    newProduct1.Ranking = a;
                    newProduct1.Description = a + " Product";

                    newProduct1 = ds.Add(newProduct1);

                    var newPicture = new T_E_Picture();
                    newPicture.Name = "picture " + newProduct1.Description;
                    newPicture.ProductId = newProduct1.ProductId;

                    newPicture = ds.Add(newPicture);
                }

                var navPictureList = new List<System.Linq.Expressions.Expression<Func<T_E_Picture, object>>>();
                navPictureList.Add(x => x.T_E_Product);
                if (ds.GetList<T_E_Picture>(null, navPictureList).ToList().Where(x => x.T_E_Product != null).Count() != 100) throw new Exception();
                if (ds.GetList<T_E_Picture>().ToList().Enum().Where(x => x.T_E_Product != null).Count() != 0) throw new Exception();

                var iquery = ds.GetListBatchLoading<T_E_Picture, long>(x => x.PictureId, System.ComponentModel.ListSortDirection.Descending, navPictureList, null, 10, 10);

                if (iquery.Where(x => x.T_E_Product == null).IsNotNullAndNotEmpty()) throw new Exception();

                ds.ResetDatabase();
            }
        }

        [TestMethod]
        public void GetSingle()
        {
            using (var ds = new DataService())
            {
                ds.ResetDatabase();

                var newProduct1 = new T_E_Product();
                newProduct1.Ranking = 1;
                newProduct1.Description = "Product";
                newProduct1 = ds.Add(newProduct1);

                var newPicture = new T_E_Picture();
                newPicture.ProductId = newProduct1.ProductId;
                newPicture.Name = "picture";
                newPicture = ds.Add(newPicture);

                var get1 = ds.GetSingle<T_E_Product>(x => x.Ranking == 1);
                if (get1.Ranking != 1) throw new Exception();
                if (get1.Description != "Product") throw new Exception();

                var navPictureList = new List<System.Linq.Expressions.Expression<Func<T_E_Picture, object>>>();
                navPictureList.Add(x => x.T_E_Product);

                var get2 = ds.GetSingle<T_E_Picture>(x => x.Name == "picture", navPictureList);
                if (get2.T_E_Product == null) throw new Exception();
                ds.ResetDatabase();
            }
        }

        [TestMethod]
        public void GetCurrentTime()
        {
            try
            {
                using (var ds = new DataService())
                {
                    var time = ds.GetCurrentDateTime();
                }
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        #endregion Public METHODS

    }
}