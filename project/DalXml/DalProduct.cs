using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal.DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class DalProduct : IProduct
{
    public int Add(Product pro)
    {
        XElement? rootConfig = XDocument.Load(@"..\..\xml\dal-config.xml").Root;
        XElement? id = rootConfig?.Element("ID");
        int productId = Convert.ToInt32(id?.Value);
        productId++;
        id.Value = productId.ToString();
        rootConfig?.Save("../../xml/dal-config.xml");
        XElement p = new("order",
                        new XElement("ID", productId),
                        new XElement("Name", pro.Name),
                        new XElement("Category", pro.Category),
                        new XElement("Price", pro.Price),
                        new XElement("InStock", pro.InStock));
        XElement? root = XDocument.Load("../../xml/Product.xml").Root;
        root?.Add(p);
        root?.Save("../../xml/Order.xml");
        return pro.ID;
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

