// Project:  MicroFluidCompiler  
// Author: Wenjie Fan (s210310)
// Department: Applied Mathematics and Computer Science
// DTU(Technical University of Denmark)


using System.Xml.Serialization;

public class ConfigReader
{
    public static BioSystemConfig GetConfig(string pathToXml)
    {
        // Read and write purchase orders.
        ConfigReader t = new ConfigReader();
        // "C:\\Users\\t-wenjiefan\\Desktop\\VisionBasedCompiler\\Config.xml"
        return t.ReadBioSystemConfig(pathToXml);
    }

    protected BioSystemConfig ReadBioSystemConfig(string filename)
    {
        // Create an instance of the XmlSerializer class;
        // specify the type of object to be deserialized.
        XmlSerializer serializer = new XmlSerializer(typeof(BioSystemConfig));
        /* If the XML document has been altered with unknown
        nodes or attributes, handle them with the
        UnknownNode and UnknownAttribute events.*/
        serializer.UnknownNode += new
        XmlNodeEventHandler(serializer_UnknownNode);
        serializer.UnknownAttribute += new
        XmlAttributeEventHandler(serializer_UnknownAttribute);

        // A FileStream is needed to read the XML document.
        FileStream fs = new FileStream(filename, FileMode.Open);
        // Declare an object variable of the type to be deserialized.
        BioSystemConfig bioSystemConfig;
        /* Use the Deserialize method to restore the object's state with
        data from the XML document. */
        bioSystemConfig = (BioSystemConfig)serializer.Deserialize(fs)!;
        // Read the order date.
        return bioSystemConfig;
    }

    private void serializer_UnknownNode
    (object sender, XmlNodeEventArgs e)
    {
        Console.WriteLine("Unknown Node:" + e.Name + "\t" + e.Text);
    }

    private void serializer_UnknownAttribute
    (object sender, XmlAttributeEventArgs e)
    {
        System.Xml.XmlAttribute attr = e.Attr;
        Console.WriteLine("Unknown attribute " +
        attr.Name + "='" + attr.Value + "'");
    }
}


/* The XmlRootAttribute allows you to set an alternate name
(PurchaseOrder) of the XML element, the element namespace; by
default, the XmlSerializer uses the class name. The attribute
also allows you to set the XML namespace for the element.  Lastly,
the attribute sets the IsNullable property, which specifies whether
the xsi:null attribute appears if the class instance is set to
a null reference. */
[XmlRootAttribute("BioSystemConfig", IsNullable = false)]
public class BioSystemConfig
{
    [XmlElementAttribute("BioCompilerConfig", IsNullable = false)]
    public BioCompilerConfig bioCompilerConfig;
    [XmlElementAttribute("BioExecutorConfig", IsNullable = false)]
    public BioExecutorConfig bioExecutorConfig;
}

public class BioCompilerConfig
{
    [XmlElementAttribute("column", IsNullable = false)]
    public int column;
    [XmlElementAttribute("row", IsNullable = false)]
    public int row;
}


public class BioExecutorConfig
{
    [XmlElementAttribute("column", IsNullable = false)]
    public int column;
    [XmlElementAttribute("row", IsNullable = false)]
    public int row;
    [XmlElementAttribute(IsNullable = false)]
    public string router;
    [XmlElementAttribute("json_template", IsNullable = false)]
    public string jsonTemplate;
    [XmlElementAttribute("basm_template", IsNullable = false)]
    public string basmTemplate;
    [XmlElementAttribute("json_output", IsNullable = false)]
    public string jsonOutput;
    [XmlElementAttribute("basm_output", IsNullable = false)]
    public string basmOutput;
}



