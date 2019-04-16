using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace SmallManagerSpace.Resources
{
    public class PropertyManageCls : CollectionBase, ICustomTypeDescriptor
    {
        public void Add(Property value)
        {
            int flag = -1;
            if (value != null)
            {
                if (base.List.Count > 0)
                {
                    IList<Property> mList = new List<Property>();
                    for (int i = 0; i < base.List.Count; i++)
                    {
                        Property p = base.List[i] as Property;
                        if (value.Name == p.Name)
                        {
                            flag = i;
                        }
                        mList.Add(p);
                    }
                    if (flag == -1)
                    {
                        mList.Add(value);
                    }
                    base.List.Clear();
                    foreach (Property p in mList)
                    {
                        base.List.Add(p);
                    }
                }
                else
                {
                    base.List.Add(value);
                }
            }
        }
        public void Remove(Property value)
        {
            if (value != null && base.List.Count > 0)
                base.List.Remove(value);
        }
        public Property this[int index]
        {
            get
            {
                return (Property)base.List[index];
            }
            set
            {
                base.List[index] = (Property)value;
            }
        }
        #region ICustomTypeDescriptor 成员
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptor[] newProps = new PropertyDescriptor[this.Count];
            for (int i = 0; i < this.Count; i++)
            {
                Property prop = (Property)this[i];
                newProps[i] = new CustomPropertyDescriptor(ref prop, attributes);
            }
            return new PropertyDescriptorCollection(newProps);
        }
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion
    }
    //属性类
    public class Property
    {
        private string _name = string.Empty;
        private object _value = null;
        private bool _readonly = false;
        private bool _visible = true;
        private string _category = string.Empty;
        TypeConverter _converter = null;
        object _editor = null;
        private string _displayname = string.Empty;
        public Property(string sName, object sValue)
        {
            this._name = sName;
            this._value = sValue;
        }
        public Property(string sName, object sValue, bool sReadonly, bool sVisible)
        {
            this._name = sName;
            this._value = sValue;
            this._readonly = sReadonly;
            this._visible = sVisible;
        }
        public string Name  //获得属性名
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string DisplayName   //属性显示名称
        {
            get
            {
                return _displayname;
            }
            set
            {
                _displayname = value;
            }
        }
        public TypeConverter Converter  //类型转换器，我们在制作下拉列表时需要用到
        {
            get
            {
                return _converter;
            }
            set
            {
                _converter = value;
            }
        }
        public string Category  //属性所属类别
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }
        public object Value  //属性值
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        public bool ReadOnly  //是否为只读属性
        {
            get
            {
                return _readonly;
            }
            set
            {
                _readonly = value;
            }
        }
        public bool Visible  //是否可见
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
            }
        }
        public virtual object Editor   //属性编辑器
        {
            get
            {
                return _editor;
            }
            set
            {
                _editor = value;
            }
        }
    }
    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        Property m_Property;
        public CustomPropertyDescriptor(ref Property myProperty, Attribute[] attrs)
            : base(myProperty.Name, attrs)
        {
            m_Property = myProperty;
        }
        #region PropertyDescriptor 重写方法
        public override bool CanResetValue(object component)
        {
            return false;
        }
        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }
        public override object GetValue(object component)
        {
            return m_Property.Value;
        }
        public override string Description
        {
            get
            {
                return m_Property.Name;
            }
        }
        public override string Category
        {
            get
            {
                return m_Property.Category;
            }
        }
        public override string DisplayName
        {
            get
            {
                return m_Property.DisplayName != "" ? m_Property.DisplayName : m_Property.Name;
            }
        }
        public override bool IsReadOnly
        {
            get
            {
                return m_Property.ReadOnly;
            }
        }
        public override void ResetValue(object component)
        {
            //Have to implement
        }
        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        public override void SetValue(object component, object value)
        {
            m_Property.Value = value;
        }
        public override TypeConverter Converter
        {
            get
            {
                return m_Property.Converter;
            }
        }
        public override Type PropertyType
        {
            get { return m_Property.Value.GetType(); }
        }
        public override object GetEditor(Type editorBaseType)
        {
            return m_Property.Editor == null ? base.GetEditor(editorBaseType) : m_Property.Editor;
        }
        #endregion
    }
    //下拉框类型转换器
    public class DropDownListConverter : StringConverter
    {
        object[] m_Objects;
        public DropDownListConverter(object[] objects)
        {
            m_Objects = objects;
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;//true下拉框不可编辑
        }
        public override
        System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(m_Objects);//我们可以直接在内部定义一个数组，但并不建议这样做，这样对于下拉框的灵活             //性有很大影响
        }
    }


    [TypeConverterAttribute(typeof(SpellingOptions))]
    public class SpellingOptions : ExpandableObjectConverter
    {
        private bool spellCheckWhileTyping = true;
        private bool spellCheckCAPS = false;
        private bool suggestCorrections = true;
        [DefaultValueAttribute(true)]
        public bool SpellCheckWhileTyping
        {
            get { return spellCheckWhileTyping; }
            set { spellCheckWhileTyping = value; }
        }
        [DefaultValueAttribute(false)]
        public bool SpellCheckCAPS
        {
            get { return spellCheckCAPS; }
            set { spellCheckCAPS = value; }
        }
        [DefaultValueAttribute(true)]
        public bool SuggestCorrections
        {
            get { return suggestCorrections; }
            set { suggestCorrections = value; }
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
        {
            if (destinationType == typeof(SpellingOptions))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is SpellingOptions)
            {
                SpellingOptions so = (SpellingOptions)value;
                return "在键入时检查:" + so.SpellCheckWhileTyping +
                       "，检查大小写: " + so.SpellCheckCAPS +
                       "，建议更正: " + so.SuggestCorrections;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;
                    int colon = s.IndexOf(':');
                    int comma = s.IndexOf(',');
                    if (colon != -1 && comma != -1)
                    {
                        string checkWhileTyping = s.Substring(colon + 1, (comma - colon - 1));
                        colon = s.IndexOf(':', comma + 1);
                        comma = s.IndexOf(',', comma + 1);
                        string checkCaps = s.Substring(colon + 1, (comma - colon - 1));
                        colon = s.IndexOf(':', comma + 1);
                        string suggCorr = s.Substring(colon + 1);
                        SpellingOptions so = new SpellingOptions();
                        so.SpellCheckWhileTyping = Boolean.Parse(checkWhileTyping);
                        so.SpellCheckCAPS = Boolean.Parse(checkCaps);
                        so.SuggestCorrections = Boolean.Parse(suggCorr);
                        return so;
                    }
                }
                catch
                {
                    throw new ArgumentException("无法将“" + (string)value + "”转换为 SpellingOptions 类型");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
    public class FileNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new string[] { "Apple", "Orange", "Banana" });
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
    [DefaultPropertyAttribute("SaveOnClose")]
    public class AppSettings
    {
        private bool saveOnClose = true;
        private string greetingText = "欢迎使用应用程序！";
        private int maxRepeatRate = 10;
        private int itemsInMRU = 4;
        private bool settingsChanged = false;
        private string appVersion = "1.0";
        public FileNameConverter defaultFileName;
        // private SpellingOptions spellingOptionsObj;
        // 应用到 DefaultFileName 属性的 TypeConverter 特性。
        //[TypeConverter(typeof(FileNameConverter)),
        //CategoryAttribute("文档设置")]
        //public string DefaultFileName
        //{
        //    get { return defaultFileName; }
        //    set { defaultFileName = value; }
        //}
        //[TypeConverter(typeof(ExpandableObjectConverter))]
        //public SpellingOptions SpellingOptionsObj
        //{
        //    get { return spellingOptionsObj; }
        //    set { spellingOptionsObj = value; }
        //}
        [CategoryAttribute("文档设置"),
        DefaultValueAttribute(true)]
        public bool SaveOnClose
        {
            get { return saveOnClose; }
            set { saveOnClose = value; }
        }
        [CategoryAttribute("全局设置"),
        ReadOnlyAttribute(true),
        DefaultValueAttribute("欢迎使用应用程序！")]
        public string GreetingText
        {
            get { return greetingText; }
            set { greetingText = value; }
        }
        [CategoryAttribute("全局设置"),
        DefaultValueAttribute(4)]
        public int ItemsInMRUList
        {
            get { return itemsInMRU; }
            set { itemsInMRU = value; }
        }
        [DescriptionAttribute("以毫秒表示的文本重复率。"),
        CategoryAttribute("全局设置"),
        DefaultValueAttribute(10)]
        public int MaxRepeatRate
        {
            get { return maxRepeatRate; }
            set { maxRepeatRate = value; }
        }
        [BrowsableAttribute(true),
        DefaultValueAttribute(false)]
        public bool SettingsChanged
        {
            get { return settingsChanged; }
            set { settingsChanged = value; }
        }
        [CategoryAttribute("版本"),
        DefaultValueAttribute("1.0"),
        ReadOnlyAttribute(true)]
        public string AppVersion
        {
            get { return appVersion; }
            set { appVersion = value; }
        }
    }

    //文件路径选择 
    public class PropertyGridFileItem : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        { return UITypeEditorEditStyle.Modal; }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService)); if (edSvc != null)
            {                 // 可以打开任何特定的对话框                
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.AddExtension = false;
                if (dialog.ShowDialog().Equals(DialogResult.OK))
                {
                    return dialog.FileName;
                }
            }
            return value;
        }
    }





}
