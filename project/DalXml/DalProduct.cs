
namespace Dal;

using Dal.DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
internal class DalProduct :IProduct
{

    public int Add(DO.Product pro)
    {
        XElement? rootConfig = XDocument.Load(@"..\xml\config.xml").Root;
        XElement? id = rootConfig?.Element("productId");
        int pId = Convert.ToInt32(id?.Value);
        pro.ID = pId;
        pId++;
        id.Value = pId.ToString();
        rootConfig?.Save("../xml/config.xml");




        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        products?.Add(pro);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        ser.Serialize(writer, products);
        writer.Close();
        return pro.ID;
    }

    public void Delete(int id)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        Product pro = products.Where(p => p.ID==id).FirstOrDefault();
        products.Remove(pro);
        ser.Serialize(writer, products);
        writer.Close();
    }

    public Product Get(Func<Product, bool> func)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        return (func == null ? products : products.Where(func).ToList()).FirstOrDefault();
    }

   
    public IEnumerable<Product> GetAll(Func<Product, bool> func = null)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        return products;
        throw new NotImplementedException();
    }

   

    public void Update(Product product)
    {

        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        Product pro = products.Where(p => p.ID==product.ID).FirstOrDefault();
        products.Remove(pro);
        products.Add(product);
        ser.Serialize(writer, products);
        writer.Close();
    }

    
    public void updateAmount(int id, int amount)
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "Products";
        xRoot.IsNullable = true;
        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);
        StreamReader reader = new StreamReader("..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);
        reader.Close();
        StreamWriter writer = new StreamWriter("..\\xml\\Product.xml");
        Product pro = products.Where(p => p.ID==id).FirstOrDefault();
        Product prod = pro;
        prod.InStock = amount;
        products.Remove(pro);
        products.Add(prod);
        ser.Serialize(writer, products);
        writer.Close();
    }
}

