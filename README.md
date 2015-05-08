# TextControl.PropertyBindingRichTextBox
http://www.textcontrol.com/en_US/blog/archive/20150105/

#RichTextBox compatible data binding with TX Text Control

A frequent need when composing UIs is to bind a property to an information source to separate presentation and content (and logic). There are several ways to accomplish this task including MVC, MVVM and MVP. A core concept to realize those architectural concepts in Windows Forms (and WPF) is property binding.

The .NET Framework System.Windows.Forms.RichTextBox provides an Rtf property that *gets or sets the text of the RichTextBox control, including all rich text format (RTF) codes*. This property accepts formatted text in the RTF format and can be used to bind the RichTextBox directly to data sources.

As TX Text Control supports many different file formats including DOC, DOCX, RTF and PDF, the file loading concept differs from the RichTextBox and is implemented using a central Load method to import various input streams.

But it is very easy to simulate the same property binding by subclassing TX Text Control and implementing your own Rtf property:

```c#
public class MyTextControl : TXTextControl.TextControl 
{ 
    public MyTextControl() 
    { 
        this.CreateControl(); 
    } 
 
    public string Rtf 
    { 
        get 
        { 
            string data; 
            this.Save(out data, TXTextControl.StringStreamType.RichTextFormat); 
            return data; 
        } 
        set 
        { 
            if (value != "") 
                this.Load(value, TXTextControl.StringStreamType.RichTextFormat); 
        } 
    } 
}
```
Now, the subclassed *MyTextControl* can be bound to a BindingSource in the same way like using the RichTextBox:
```c#
DataTable dt = new DataTable(); 
dt.Columns.Add("rtfText"); 
 
dt.Rows.Add(new string[] { @"{\rtf1 Test \par Test}" }); 
dt.Rows.Add(new string[] { @"{\rtf1 Test2 \par Test2}" }); 
dt.Rows.Add(new string[] { @"{\rtf1 Test3 \par Test3}" }); 
 
bindingSource1.DataSource = dt; 
 
myTextControl1.DataBindings.Add( 
    new Binding("Rtf", bindingSource1, "rtfText", true));
```
When using a *BindingNavigator* connected to the BindingSource, you can easily navigate through the contained RTF documents:

<img src="http://s2.www.textcontrol.com/en_US/blog/archive/20150105/assets/property_binding.png" />
    
