
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
        //XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DO.Product>));
        //StreamReader reader = new StreamReader("../../../../Product.xml");

        //List<Product>? products = (List<DO.Product>?)xmlSerializer.Deserialize(reader);
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "ArrayOfProduct";
        // xRoot.Namespace = "http://www.cpandl.com";
        xRoot.IsNullable = true;

        XmlSerializer ser = new XmlSerializer(typeof(List<Product>), xRoot);


        //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
        StreamReader reader = new("..\\..\\..\\..\\xml\\Product.xml");
        List<DO.Product> products = (List<DO.Product>)ser.Deserialize(reader);

        XElement? rootConfig = XDocument.Load(@"..\..\..\..\xml\dal-config.xml").Root;
        XElement? id = rootConfig.Element("ids").Element("productId");
        int productId = Convert.ToInt32(id?.Value);
        productId++;
        id.Value = productId.ToString();
        products?.Add(pro);
        reader.Close();
        StreamWriter writer = new StreamWriter("../../../../../Product.xml");
        ser.Serialize(writer, products);
        return pro.ID;



        //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Order>));
        //StreamReader r = new("..\\..\\..\\..\\xml\\order.xml");
        //List<DO.Order>? lst = (List<DO.Order>?)ser.Deserialize(r);
        //lst?.Add(obj);
        //r.Close();
        //StreamWriter w = new StreamWriter("..\\..\\..\\..\\xml\\order.xml");
        //ser.Serialize(w, lst);
        //w.Close();

        //return obj.ID;

    }

    public void Delete(int id)
    {
        StreamReader reader = new StreamReader("../../Product.xml");
        StreamWriter writer = new StreamWriter("../../Product.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));
        List<Product>? products = (List<Product>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Product pro = products.Where(p => p.ID==id).FirstOrDefault();
        products.Remove(pro);
        xmlSerializer.Serialize(writer, products);
        writer.Close();
        throw new NotImplementedException();
    }

    public Product Get(Func<Product, bool> func)
    {
        StreamReader reader = new StreamReader("../../Product.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));
        List<Product>? products = (List<Product>?)xmlSerializer.Deserialize(reader);
        return (func == null ? products : products.Where(func).ToList()).FirstOrDefault();
    }

   
    public IEnumerable<Product> GetAll(Func<Product, bool> func = null)
    {
        StreamReader reader = new StreamReader("../../Product.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));
        List<Product>? products = (List<Product>?)xmlSerializer.Deserialize(reader);
        return products;
        throw new NotImplementedException();
    }

   

    public void Update(Product product)
    {
        StreamReader reader = new StreamReader("../../Product.xml");
        StreamWriter writer = new StreamWriter("../../Product.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));


        List<Product>? products = (List<Product>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Product pro = products.Where(p => p.ID==product.ID).FirstOrDefault();
        products.Remove(pro);
        products.Add(product);
        xmlSerializer.Serialize(writer, products);
        writer.Close();
        throw new NotImplementedException();
    }

    
    public void updateAmount(int id, int amount)
    {
        StreamReader reader = new StreamReader("../../Product.xml");
        StreamWriter writer = new StreamWriter("../../Product.xml");
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));


        List<Product>? products = (List<Product>?)xmlSerializer.Deserialize(reader);
        reader.Close();
        Product pro = products.Where(p => p.ID==id).FirstOrDefault();
        Product prod = pro;
        prod.InStock = amount;
        products.Remove(pro);
        products.Add(prod);
        xmlSerializer.Serialize(writer, products);
        writer.Close();
        throw new NotImplementedException();
    }
}

